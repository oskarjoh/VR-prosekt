///
/// Attach this script to 'PlankHalf' under Plank
///

namespace VRTK.Examples
{
    using UnityEngine;

    //This script is applied to all planks that are supposed to become a part of the birdhouse.
    //The task of this script is to allow the planks to be picked up and placed on the house. 
    public class BirdhousePlank : VRTK_InteractableObject
    {
        [Tooltip("Drag the Birdhouse gameobject here.")]
        //A reference to the birdhouse gameobject
        public GameObject birdHouse;
        //Saves the color you chose for the 'TouchHighlightColor'
        private Color highlightColor;

        private void Start()
        {
            //Save the wanted color. We do this so that it can be reapplied once released from the birdhouse.
            highlightColor = this.GetComponent<BirdhousePlank>().touchHighlightColor;
        }

        //This function returns true if this plank has been placed inside a 'SnapDropZone'.
        //The nail gun uses this function to test if it can nail the plank or not.
        public override bool IsInSnapDropZone()
        {
            return base.IsInSnapDropZone();
        }

        //When the plank has been grabbed, this function will be called.
        public override void Grabbed(VRTK_InteractGrab currentGrabbingObject)
        {
            base.Grabbed(currentGrabbingObject);

            //To make this plank able to go through objects, and this collide with the birdhouse,
            //we make the collider to a type 'trigger'.
            this.GetComponent<BoxCollider>().isTrigger = true;
        }

        //Called when this plank has been dropped.
        public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject)
        {
            base.Ungrabbed(previousGrabbingObject);
            //The nailgun was dropped, so to prevent it from going through the ground we
            //re-enable the collider
            this.GetComponent<BoxCollider>().isTrigger = false;
        }

        //When this plank is nailed by the nail gun, this function is called. 
        //This function will update the necessary variables for the game to understand that this plank
        //is now a part of the birdhouse.
        public void SetAsHouse()
        {
            //To prevent unwanted behaviour, we make sure that this plank isn't already part of the birdhouse.
            //If the tag of this object isn't 'House', make it part of the birdhouse.
            if(this.transform.tag != "House")
            {
                //Update ammount of planks added
                HouseProgress.progress.PlankAdded();

                //Make this plank ungrabbable
                base.isGrabbable = false;

                //Set the parent of this object to be the birdhouse
                this.transform.SetParent(birdHouse.transform);
                //Update the tag of this gameobject
                this.transform.tag = "House";
                //Make the "Controller is inside me" highlighted color light to be black, aka invisible
                this.GetComponent<BirdhousePlank>().touchHighlightColor = new Color(0, 0, 0);
            }
        }

        //When the crowbar is breaking free this plank, this function is called.
        //This function will update the necessary variables for the game to understand that this plank
        //is to behave as it did before it was nailed to the birdhouse.
        public void SetAsPlank(Vector3 target)
        {
            //Debug.Log("Removed house piece");
            //To prevent unwanted behaviour due to lag or whatever, make sure this function isn't completed
            //if this plank already is a normal plank.
            if (this.transform.tag != "ReadyPlank")
            {
                //Decrease amount of planks on the birdhouse
                HouseProgress.progress.PlankRemoved();
                
                //Get a reference to the 'SnapDropZone' this plank currently is inside
                VRTK_SnapDropZone zone = GetStoredSnapDropZone();
                //Release this plank from that zone.
                this.ToggleSnapDropZone(zone, false);

                //Set the tag to be 'ReadyPlank' so that it yet again can be snapped to the house and nailed.
                this.transform.tag = "ReadyPlank";
                //Make it so that this object has no parents
                this.transform.parent = null;
                //Make this object  grabbable
                base.isGrabbable = true;
                //Make the highlighted color yellow again
                this.GetComponent<BirdhousePlank>().touchHighlightColor = highlightColor;

                //For fun we add a tiny explosion force where the crowbar is hitting the plank. This will move the plank a bit,
                //making it easier for the player to see that the plank no longer is part of the house.
                //Tweak the variables as you want!
                this.GetComponent<Rigidbody>().AddExplosionForce(2f, target, 1, 1, ForceMode.Impulse);
            }
        }
    }

}

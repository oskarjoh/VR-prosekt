///
/// Attach this script to Crowbar
///

namespace VRTK.Examples
{
    using UnityEngine;

    public class Crowbar : VRTK_InteractableObject
    {

        //Called when the crowbar is grabbed
        public override void Grabbed(VRTK_InteractGrab currentGrabbingObject)
        {
            base.Grabbed(currentGrabbingObject);

            //To make this gameobject able to go through objects, and collide with the birdhouse,
            //we make the collider to a type 'trigger'.
            this.GetComponent<MeshCollider>().isTrigger = true;
        }

        //Called when the crowbar is released
        public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject)
        {
            base.Ungrabbed(previousGrabbingObject);
            
            //The crowbar was dropped, so to prevent it from going through the ground we
            //make the collision solid again.
            this.GetComponent<MeshCollider>().isTrigger = false;
        }

        //Called when the trigger is pressed while this object is held.
        //Used to check if the plank was nailed to the birdhouse
        public override void StartUsing(VRTK_InteractUse currentUsingObject)
        {
            base.StartUsing(currentUsingObject);

            //Attempt to release a plank from the birdhouse. This task is performed by the CrowbarAttemptFire script.
            //We know that the first child of this gameobject holds the CrowbarAttemptFire script, so we say 
            //"My child number 0, go and do this thing".
            this.transform.GetChild(0).gameObject.GetComponent<CrowbarAttemptFire>().AttemptFire();
        }
    }
}

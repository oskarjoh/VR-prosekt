///
/// Attach this script to NailGun
///


namespace VRTK.Examples
{
    using UnityEngine;

    public class NailGun : VRTK_InteractableObject
    {
        //Called when the nail gun is grabbed
        public override void Grabbed(VRTK_InteractGrab currentGrabbingObject)
        {
            base.Grabbed(currentGrabbingObject);

            //To make this gameobject able to go through objects, and this collide with the birdhouse,
            //we make the collider to a type 'trigger'.
            this.GetComponent<BoxCollider>().isTrigger = true;
        }

        //Called when the nail gun is released
        public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject)
        {
            base.Ungrabbed(previousGrabbingObject);
            //The nail gun was dropped, so to prevent it from going through the ground we
            //re-enable the collider
            this.GetComponent<BoxCollider>().isTrigger = false;
        }

        //Called when the when the trigger is pressed while this object is held.
        //Used to check if the plank was nailed to the birdhouse
        public override void StartUsing(VRTK_InteractUse currentUsingObject)
        {
            base.StartUsing(currentUsingObject);

            //Attempt to merge the plank with the birdhouse. This task is performed by the NailgunHitboxChecker script.
            //We know that the first child of this gameobject holds the NailgunHitboxChecker script, so we say 
            //"My child number 0, go and do this thing".
            this.transform.GetChild(0).gameObject.GetComponent<NailgunHitboxChecker>().AttemptFire();
        }
    }


}

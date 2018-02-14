///
/// Attach this script to 'SawHandle' under Saw
///

namespace VRTK.Examples
{
    using UnityEngine;

    //This script is not put on the actual saw handle model, but on the gameobject named 'HandleRoot'. We move that object, 
    //and then LookAtSawHandle.cs on 'SawPivot' manipulates the saw rotation to face the object we now are holding.
    //The saw is the most complicated item in this entire project, but hopefully it's easy enough to understand how it works.
    public class SawHandle : VRTK_InteractableObject
    {
        [Tooltip("The position this gameobject starts in.")]
        //This stores the default position of the saw handle
        public Vector3 initPosition;

        [Tooltip("The rotation this gameobject starts in.")]
        //This stores the default rotation of the saw handle
        public Vector3 initRotation;

        [Tooltip("Drag the SawBlade gameobject here.")]
        //A reference to the sawblade
        public GameObject sawBlade;

        //This function is called when this gameobject is picked up. 
        public override void Grabbed(VRTK_InteractGrab usingObject = null)
        {
            base.Grabbed(usingObject);

            Debug.Log("Grabbed object");
            
            //When the object is grabbed, we make it invisible. This is because it's now a part of the controller, and you would see both it,
            //and the actual grip on the saw model. So until it is released, make it invisible. 
            //Try to comment out this line of code to see for yourself what we are talking about.
            this.GetComponent<MeshRenderer>().enabled = false;
            //Inform the sawblade that it should start rotatin
            sawBlade.GetComponent<RotateSawblade>().StartRotating();
            //Inform HitsPlank that the saw is moving, allowing "hitting plank" sawing noices.
            HitsPlank.hits.canRotate = true;
        }

        public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject = null)
        {
            base.Ungrabbed(previousGrabbingObject);

            Debug.Log("Ungrabbed object");

            this.transform.localPosition = initPosition;
            this.transform.localRotation = Quaternion.Euler(initRotation.x, initRotation.y, initRotation.z);

            //Make  visible again after release
            this.GetComponent<MeshRenderer>().enabled = true;
            sawBlade.GetComponent<RotateSawblade>().StopRotating();
            //Inform HitsPlank that the saw stopped, preventing unwanted audioloops.
            HitsPlank.hits.canRotate = false;
        }

        //When a controller touches this gameobject, this function is called.
        public override void StartTouching(VRTK_InteractTouch currentTouchingObject)
        {
            base.StartTouching(currentTouchingObject);
        }

        //When the controllers leaves this gameobject, this function is called
        public override void StopTouching(VRTK_InteractTouch previousTouchingObject)
        {
            base.StopTouching(previousTouchingObject);
        }
    }

}

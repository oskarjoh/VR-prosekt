namespace VRTK.Examples
{
    using UnityEngine;
    using System.Collections;

    public class Eksempel : VRTK_InteractableObject
    {
        // Use this for initialization
        void Start()
        {
            Debug.Log("Started");
        }

        void Update()
        {
            base.Update();

            Debug.Log("Updating");
        }

        //Objektet plukkes opp
        public override void Grabbed(VRTK_InteractGrab currentGrabbingObject)
        {
            base.Grabbed(currentGrabbingObject);

            Debug.Log("Object picked up");
        }

        //Objektet slippes
        public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject)
        {
            base.Ungrabbed(previousGrabbingObject);

            Debug.Log("Object dropped");
        }

        public override void StartUsing(VRTK_InteractUse currentUsingObject)
        {
            base.StartUsing(currentUsingObject);

            Debug.Log("Object was used");
        }

        public override void StopUsing(VRTK_InteractUse previousUsingObject)
        {
            base.StopUsing(previousUsingObject);

            Debug.Log("Object is no longer being used");
        }
    }
}



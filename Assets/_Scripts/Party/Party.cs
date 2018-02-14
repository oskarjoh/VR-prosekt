namespace VRTK.Examples
{
    using UnityEngine;

    public class Party : VRTK_InteractableObject
    {
        [Tooltip("Drag the Disco Ball gameobject into this variable")]
        //A reference to the discoball gameobject. 
        public GameObject discoBall;

        //Called when the player clicks on this gameobject
        public override void StartUsing(VRTK_InteractUse usingObject)
        {
            base.StartUsing(usingObject);

            //Set the discoball gameobject active. 
            discoBall.SetActive(true);
        }
    }
}

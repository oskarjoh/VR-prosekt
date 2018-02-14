using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ViewPartyButton : MonoBehaviour {

    [Tooltip("The position this object should move towards once spawned.")]
    //This variable holds the target position for this gameobject
    public Vector3 target;
	
	// Update is called once per frame
	void Update()
    {
        //This line of code moves this gameobject towards the position 'target' over a defined time. 
        //We use this to move the cylinder with the party button from beneath the floor to the target height. 
        this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, target, Time.deltaTime * 0.5f);
    }
}

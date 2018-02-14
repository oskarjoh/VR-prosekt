using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script will reset a object that has gone out of world bounds.
//Usable in cases where objects such as tools falls through walls or floors.
public class ResetObjectOutsideOfScene : MonoBehaviour
{
    //Holds information about this objects original position and rotation
    private Transform initTransform;

	// Use this for initialization
	void Start ()
    {
        //Update variable to contain information about this objects transformations
        initTransform = this.transform;	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //If this objects y-position is greater than 30 or lower than -30, it's outside the world.
        //Therefore, reset the object.
		if(this.transform.position.y > 30f || this.transform.position.y < -30)
        {
            //Set this objects position to the same as the original position
            this.transform.position = initTransform.position;
            //Set this objects rotation to the same as the original rotation
            this.transform.rotation = initTransform.rotation;
        }
	}
}

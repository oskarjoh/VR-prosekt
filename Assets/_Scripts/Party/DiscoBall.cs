using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoBall : MonoBehaviour
{
    [Tooltip("The position the disco ball should move towards once spawned.")]
    //The target position for the discoball
    public Vector3 target;
	
	// Update is called once per frame
	void Update ()
    {
        //Make the discoball rotate forever. this makes the lights that are children of this gameobject also rotate, 
        //which gives it a cool discoball effect.
        this.transform.localRotation = Quaternion.Euler(0, this.transform.eulerAngles.y + 2, 0);

        MoveDown();

    }

    private void MoveDown()
    {
        //This line of code moves this gameobject towards the position 'target' over a defined time.
        this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, target, Time.deltaTime * 0.5f);
    }
}

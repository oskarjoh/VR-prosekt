///
/// Attach this script to 'SawPivot' under Saw
///

using UnityEngine;

public class LookAtSawHandle : MonoBehaviour
{
    [Tooltip("Drag the gameobject 'SawHandle' here.")]
    //A reference to the gameobject the player actually is holding and moving around
    public GameObject objectToLookAt;

    [Tooltip("The rotation this gameobjects starts in.")]
    //The initial rotation of this gameobject
    public Vector3 initRotation;

    [Tooltip("Drag 'AudioSawing' here.")]
    //A reference to the audiosource that plays when a plank is sawn in half
    public AudioSource sawing;

    //The maximum rotation we will allow the saw to have
    private float maxXRotation = 0;

    //The lowest rotation we will allow the saw to have
    private float minXRotation = 330;

    // Update is called once per frame
    void Update ()
    {
        //Look towards the handle the player is holding
        LookAtThree();
    }

    //Simply forces this gameobject to face whatever object we've told it to face
    private void LookAtOne()
    {
        //Look towards the position of the objectToLookAt gameobject
        this.transform.LookAt(objectToLookAt.transform);
    }

    //Enable restrictions on the saw movement. We wan't it to only move in the x-axis, anything else is unrealistic
    private void LookAtTwo()
    {
        //Create a new temporary variable of type Transform (used for rotations and positions)
        //Set it to have the same transformation as this object, to get accurate result. 
        Transform newRot = this.transform;
        //Make the newRot variable face the handle the player pulls
        newRot.LookAt(objectToLookAt.transform);
        
        //Make the actual saw object gain the same rotation in the X-axis as the newRot variable, but set the y- and z-axis to have the default rotations
        this.transform.rotation = Quaternion.Euler(newRot.eulerAngles.x, initRotation.y, initRotation.z);
    }

    /*
     * Continue restricting the movement of the saw.
     * It's not realistic that it can turn around 360 degrees.
     * After newRot is transformed to look towards the handle the player is holding, we manipulate
     * the x-rotation value.
     */
    private void LookAtThree()
    {
        //Create a new temporary variable of type Transform (used for rotations and positions)
        //Set it to have the same transformation as this object, to get accurate result. 
        Transform newRot = this.transform;
        //Make the newRot variable face the handle the player pulls
        newRot.LookAt(objectToLookAt.transform);

        //Debug the newRot's current x rotation. Using this, and moving it manually in the scene while the game is running, we can get the
        //exact values for our restrictions.
        //Debug.Log("newRot rotaion: " + newRot.transform.eulerAngles.x);

        //To make it easier to work with, create a new variable that copies newRot's x-rotation value
        float xRotation = newRot.transform.localEulerAngles.x;

        //As seen in the inspector, the saws rotation goes to 0, and then starts increasing towards 180 again. 
        //When the rotation is greater than 0, we want the saw to snap to that rotation, but to prevent unwanted snapping we also saw that
        //we had to for "&& xRotation < 180". 
        //If this is true, the saw has been moved all the way down.
        if (xRotation > maxXRotation && xRotation < 180)
        {
            //Force stop the rotation
            xRotation = maxXRotation;

            //Attempt to saw a plank
            PerformSaw();
        }
        else if (xRotation < minXRotation && xRotation >= 180) //The saw is rotated to the maximum 'upwards' rotation
        {
            //Snap the rotation
            xRotation = minXRotation;
        }

        //Set the rotation of the saw
        this.transform.localRotation = Quaternion.Euler(xRotation, initRotation.y, initRotation.z);
    }

    //Called every frame when the saw has been rotated all the way down
    private void PerformSaw()
    {
        //If true, we know that the saw is hitting a plank that can be sawn in half
        if (HitsPlank.hits.currentPlank != null)
        {
            //HitsPlank holds the reference to the plank, so we use that reference to call the Release function to create new planks.
            HitsPlank.hits.currentPlank.GetComponent<ReleaseChildren>().Release();
            //Doing the line above hinders the OnTriggerExit to trigger, so do the reset functionality for that script
            HitsPlank.hits.currentPlank = null;

            //Play the "Sawn a plank" audioclip
            sawing.Play();

            //Inform HitsPlank that it isn't actually hitting a plank anymore, so it should stop making the sawing sounds
            HitsPlank.hits.StopAudio();
        }
    }
}

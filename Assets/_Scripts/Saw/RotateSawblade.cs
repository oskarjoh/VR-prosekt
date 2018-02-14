///
/// Attach this script to 'SawBlade' under Saw
///

using UnityEngine;

//This script rotates the sawblade on the saw.
//We could have a simple "rotate in a constant speed" functionality, but we made it more sexy
//by easing in and out the rotation speed. 
public class RotateSawblade : MonoBehaviour {

    //If true, the saw was started and we check to see if the sawblade should ease in
    private bool easeIn = false;
    //If true, the saw was released and we check to see if the sawblade should ease out
    private bool easeOut = false;

    [Tooltip("This variable manipulates how fast the saw blade starts and stops its rotation.")]
    //Modifies how fast the sawblade eases in and out
    public float easeModifier = 14f;

    //The current sawblade speed
    private float speed = 0f;

    [Tooltip("This is how fast we want the saw blade to rotate once it has reached max velocity.")]
    //The target sawblade speed
    public float speedTarget = 7f;

    [Tooltip("Drag the gameobject 'AudioStart' here.")]
    //Holds a reference to the sawStart AudioSource component
    public AudioSource sawStart;

    [Tooltip("Drag the gameobject 'AudioLoop' here.")]
    //Holds a reference to the sawLoop AudioSource component
    public AudioSource sawLoop;

    // Update is called once per frame
    void Update ()
    {
        //If easeIn is true, we apply more speed to the sawblade
        if (easeIn)
        {
            EaseInSaw();
        }
        else if(easeOut) //If easeout is true, we remove speed from the sawblade
        {
            EaseOutSaw();
        }

        //For every frame, rotate the sawblade in the y-direction with the current 'speed' value.
        //Since 'speed' is modified in EaseInSaw and EaseOutSaw this line of code can run every frame. 
        this.transform.Rotate(0, speed, 0);
    }

    //Increases speed of sawblade rotation
    private void EaseInSaw()
    { 
        //Increase speed
        speed += Time.deltaTime * easeModifier;
        //Increase volume of sawLoop
        sawLoop.volume = speed / speedTarget;

        //If speed is greater than or equal to the target speed, this function is done
        if (speed >= speedTarget)
        {
            //Set the volume to 1
            sawLoop.volume = 1;
            //Set speed to be speedtarget
            speed = speedTarget;
            //Stop repeating this function
            easeIn = false;
        }

    }

    //Decreases speed of sawblade rotation
    private void EaseOutSaw()
    {
        //Decrease speed
        speed -= Time.deltaTime * easeModifier;
        //Decrease volume of sawLoop
        sawLoop.volume = speed / speedTarget;

        //If the speed is lower than or equal to 0, this function is done
        if (speed <= 0)
        {
            //Set the volume to 0
            sawLoop.volume = 0;
            //Set speed to 0
            speed = 0;
            //Stop repeating this function
            easeOut = false;
        }
    }

    //Called when the saw handle is grabbed by the player
    public void StartRotating()
    {
        //Begin easing in
        easeIn = true;
        //Make sure the program stop easing out
        easeOut = false;

        //To prevent audio glitches, we first stop sawStart, then play it
        sawStart.Stop();
        sawStart.Play();
        //Stop sawLoop, set the volume to 0, then play it again
        sawLoop.Stop();
        sawLoop.volume = 0;
        sawLoop.Play();
    }

    //Called when the saw handle is released by the player
    public void StopRotating()
    {
        //Begin easing out the sawblade rotation speed
        easeOut = true;
        //Stop easing in the rotation speed
        easeIn = false;
    }


}

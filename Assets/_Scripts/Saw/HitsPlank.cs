///
/// Attach this script to 'SawBlade' under Saw
///

using UnityEngine;

//This script is on the MDL_SawBlade gameobject. 
//This script checks if the sawblade is hitting a plank, and stores a reference to that plank.
//It also plays an audioloop while hitting a plank, increasing the realism of this game.
public class HitsPlank : MonoBehaviour
{
    //Creates a reference to this script
    public static HitsPlank hits;

    //A reference to the currently hit plank
    [HideInInspector] public GameObject currentPlank = null;
    [Tooltip("Drag 'AudioHittingPlank' here.")]
    //A reference to the audiosource that plays when this is hitting a plank
    public AudioSource hittingPlank;

    //If true, the saw was started and we check to see if the sawblade should ease in
    private bool easeIn = false;
    //If true, the saw was released and we check to see if the sawblade should ease out
    private bool easeOut = false;
    //This is true while the player is holding the saw handle. This has to be true for the sawblade to react on hitting a plank
    [HideInInspector]public bool canRotate = false;

    [Tooltip("This variable manipulates how fast the 'hitting a plank' sound effect fades in. Higher number makes the fade faster.")]
    //This alter the speed of how fast the audio volume is increasing / decreasing
    public float easeModifier = 1.0f;
    //This is the current speed (volume) of the hittingPlank audiosource
    private float speed = 0f;
    //Target speed
    private float speedTarget = 1.0f;

    private void Awake()
    {
        //Create the reference to this script
        hits = this;
    }

    //Called whenever this gameobject collides with another gameobject
    private void OnTriggerEnter(Collider other)
    {
        //The tag of the other gameobject is 'Plank', and object we can saw
        if(other.transform.tag == "Plank")
        {
            Debug.Log("Hit plank");
            //Save a reference to the plank
            currentPlank = other.gameObject;
            //Begin playing the looping sawing audio
            StartAudio();
        }
    }

    //Called whenever this gameobject stops colliding with another gameobject
    private void OnTriggerExit(Collider other)
    {
        //The tag of the other gameobject was 'Plank'
        if (other.transform.tag == "Plank")
        {
            Debug.Log("No longer hitting plank");
            //Remove the reference to the plank
            currentPlank = null;
            //Stop the audio from looping
            StopAudio();
        }
    }

    private void Update()
    {
        //If true, the saw is currently being used
        if (canRotate)
        {
            if (easeIn) //If the audio hasn't reached max volume yet, ease it in
            {
                EaseInAudio();
            }
            else if (easeOut) //If the audio is going to stop, ease out the volume
            {
                EaseOutAudio();
            }
        }
        else //Player is not using the saw
        {
            if (hittingPlank.volume > 0) //The volume is greater than 0, so decrease it
            {
                if(easeIn == true) //If the easeIn variable has been set to false, perform the StopAudio function
                    StopAudio();
                EaseOutAudio(); //Ease out the audio
            }
        }
    }

    //We have to ease in  the audio
    private void EaseInAudio()
    {
        //Increase speed variable
        speed += Time.deltaTime * easeModifier;
        //Calculate audio volume
        hittingPlank.volume = speed / speedTarget;

        //The current speed has reached the target value
        if (speed >= speedTarget)
        {
            //Set the volume to 1
            hittingPlank.volume = 1;
            //Set the speed value to be equal to the target value
            speed = speedTarget;
            //Stop easing in the audio
            easeIn = false;
        }

    }

    //We have to ease out the audio
    private void EaseOutAudio()
    {
        //Decrease the speed variable
        speed -= Time.deltaTime * easeModifier;
        //Calculate audio volume
        hittingPlank.volume = speed / speedTarget;

        //If the speed value is lower than or equal to 0, we are done
        if (speed <= 0)
        {
            //Set the volume to 0
            hittingPlank.volume = 0;
            //Set the speed to 0
            speed = 0;
            //Stop easing out
            easeOut = false;
        }
    }

    //Called when OnTriggerEnter collides with a new plank
    private void StartAudio()
    {
        //Begin easing in
        easeIn = true;
        //Stop easing out, just in case it is doing it
        easeOut = false;

        //Restart the hittingPlank audioclip
        hittingPlank.volume = 0;
        hittingPlank.Stop();
        hittingPlank.Play();
    }

    //Called when the plank has been sawn in half, when the player stops using the saw and when the saw no longer collides with a plank
    public void StopAudio()
    {
        //Start easing out the audio
        easeOut = true;
        //Stop easing in, just in case it is doing it
        easeIn = false;
    }
}

///
/// Attach this script to 'BlinkingLight' under Lightswitch
///

using UnityEngine;

//This function makes the light around the lightswitch on the wall blink.
//It's a great way to inform the player where he/she should start.

public class LightBlink : MonoBehaviour
{
    //How long it takes for the light to loop between full and low intensity
    private float duration = 1f;
    //Current progress in the loop
    private float timer = 0f;
    //Used to check if the light is increasing or lowering in intensity
    private bool up = false;
    //A reference to the light component.
    private Light myLight;

	// Use this for initialization
	void Start () {
        //Set the reference to the light component.
        myLight = this.GetComponent<Light>(); 
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Light is increasing in intensity
		if(up)
        {
            IncreaseIntensity();
        }
        else //The intensity is lowering
        {
            LowerIntensity();
        }
	}

    //Called from Update when the intensity is increasing
    private void IncreaseIntensity()
    {
        //If the timer variable is equal to or greater than the duration variable, we know that the wanted intensity is met
        if (timer >= duration)
        {
            //Inform the code that the intensity is supposed to lower now
            up = false;
            //Reset the timer
            timer = 0;
            //Set the intensity of the light to be 1, just to be on the safe side.
            myLight.intensity = 1;
        }
        else //Required duration not yet met, so increase the intensity
        {
            //Increase the timer variable by 'Time.deltaTime'
            //Time.deltaTime is the time between this frame and the previous frame, and is the most commonly used way to calculate time. 
            timer += Time.deltaTime;
            //Calculate the intensity of the light. This is easily done by taking time / duration, and allows you to freely change 
            //the duration variable without breaking the logic of the code.
            myLight.intensity = timer / duration;
        }
    }

    //This function does exactly the same as the IncreaseIntensity, except how it calculates the light intensity
    private void LowerIntensity()
    {
        if (timer > duration)
        {
            //Inform the code that the intensity is supposed to increase now
            up = true;
            timer = 0;
            //Set the intensity of the light to be 0, just to be on the safe side.
            myLight.intensity = 0;
        }
        else
        {
            timer += Time.deltaTime;
            //timer / duration is closer to 1 the higher the timer variable is, so to calculate this in the negative direction we just take 1 - theTimeDifference.
            //This can also be done by setting timer = 1, and time -= Time.deltaTime. When timer is <= 0, this function is completed.
            myLight.intensity = 1 - (timer / duration);
        }
    }
}

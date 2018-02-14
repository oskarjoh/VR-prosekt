using UnityEngine;

//This script is applied to every lightsource that are a child of the discoball. 
//What's cool about this script is that it uses randomly generated numbers to set the intensity and speed of the lights,
//making the discoball appear more alive. 
public class DiscoLight : MonoBehaviour
{
    //How long it takes for the ball to go between max and 0 intensity
    private float duration;
    //Current position in the duration timer
    private float timer;
    //Sets the maximum intensity of the light
    private float intensity;

    //A boolean telling the script if the light is moving up or down
    private bool up = false;
    //A reference to this gameobjects lightcomponent
    private Light myLight;

    //The current rotation target for this light, also randomly generated
    private Vector3 targetRotation;

	// Use this for initialization
	void Awake () {
        //Get a reference to this gameobjects lightcomponent
        myLight = this.GetComponent<Light>();

        //Generate a random number for how fast the light intensity should move between 0 and 'intensity'
        duration = Random.Range(0.2f, 2.0f);

        //Do a random test to see if the light should start by moving up or down
        float random = Random.Range(0, 100);
        if (random <= 50)
            up = true;
        //Set a random max intensity value for the light
        intensity = Random.Range(1, 3);
        //Create a randomly generated target rotation in the x-axis
        targetRotation = new Vector3(Random.Range(0, 360), this.transform.eulerAngles.y, this.transform.eulerAngles.z);
	}

    // Update is called once per frame
    void Update()
    {
        //Move between intensities
        Intensity();
        //Move the light rotation
        MoveUpAndDown();
    }

    //Rotates the light
    private void MoveUpAndDown()
    {
        //We use a functionality called 'Lerp' to rotate this gameobject towards a wanted rotation. 
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(targetRotation), Time.deltaTime * 0.2f);

        //We calculate the difference between this gameobjects current rotation and the target rotation. We do this to check
        //if it time to generate a new rotation to move towards
        float dif = this.transform.rotation.x - Quaternion.Euler(targetRotation).x;
        if(Mathf.Abs(dif) <= 1) //Mathf.Abs(dif) does the actual calculation for us. The difference is less than 1, so update the rotation
        {
            //Generate a new random x-rotation target
            targetRotation = new Vector3(Random.Range(0, 360), this.transform.eulerAngles.y, this.transform.eulerAngles.z);
        }
    }

    //Changes the intensity of the light
    private void Intensity()
    {
        //Increasing intensity
        if (up)
        {
            IncreaseIntensity();
        }
        else //Decreasing intensity
        {
            LowerIntensity();
        }
    }

    //Gradually increases the intensity of the light
    private void IncreaseIntensity()
    {
        //The current intensity is greater than or equal to the target intensity
        if (myLight.intensity >= intensity)
        {
            //Start lowering the intensity
            up = false;
            //Reset timer
            timer = 0;
            //For safety we manually set the intensity as well.
            myLight.intensity = intensity;
        }
        else //Requiremens not met, increase more
        {
            //Increase timer
            timer += Time.deltaTime;
            //Calculate the light intensity
            myLight.intensity = timer / duration * intensity;
        }
    }

    //Gradually dencreases the intensity of the light
    private void LowerIntensity()
    {
        //The current intensity is lower than or equal to 0
        if (myLight.intensity <= 0)
        {
            //Start increasing intensity
            up = true;
            //Reset timer
            timer = 0;
            //For safety we manually set the intensity as well
            myLight.intensity = 0;
        }
        else //Requirements not met, decrease more
        {
            //Increase timer
            timer += Time.deltaTime;
            //Calculate intensity
            myLight.intensity = intensity - (timer / duration);
        }
    }
}

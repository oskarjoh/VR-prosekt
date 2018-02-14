///
/// Attach this script to 'Lightswitch'
///

namespace VRTK.Examples
{
    using UnityEngine;

    public class Lightswitch : VRTK_InteractableObject
    {
        [Tooltip("An array containing all ligts that are going to be affected by this lightswitch")]
        //An array containing all the lights this button is controlling
        public GameObject[] lights;
        [Tooltip("This bool controls if the lights are on or off by default. If true, the lights are on when the game starts. If false, the lights are off.")]
        //Boolean telling the script if the lights are on or off. By default this is false, as are the lights
        public bool lightsOn = false;
        //Reference to this gameobjects AudioSource. Used to play an audioclip when the button is pressed
        private AudioSource source;

        [Tooltip("This defines the light intensity when the lights are turned off. We should have some light, so that we don't leave the player in complete darkness.")]
        //How powerful the intensity of the lights are when turned off. Modified in the inspector!
        public float lighOffIntensity = 0.2f;
        [Tooltip("This defines the light intensity when the lights are turned on.")]
        //How powerful the intensity of the lights are when turned on. Modified in the inspector!
        public float lightOnIntensity = 1.0f;

        
        /*
         * This function comes from the plugin VRTK. When a controller interracts with the object
         * holding this script, this function is called. It functions as a "OnCollisionEnter if criteria is met"
         * handler.
         */
        public override void StartUsing(VRTK_InteractUse usingObject)
        {
            base.StartUsing(usingObject);

            //The lightswitch was clicked, so change the lights!
            ChangeLightning();
        }

        /*
         * This function is called at the start of the program. It creates the reference to the audiosource,
         * and sets the intensity for all lights to be that of "off"
         */ 
        public void Start()
        {
            //Create the reference to this gameobjects audiosource
            source = this.GetComponent<AudioSource>();

            //Make sure that we populated the array
            if (lights.Length > 0)
            {
                if(!lightsOn) //If this is false, we want to turn off the lights at launch
                {
                    //For every instance of objects in the 'lights' array, perform a task
                    for (int i = 0; i < lights.Length; i++)
                    {
                        //Set light number 'i' to have a light intensity of lighOffIntensity
                        lights[i].GetComponent<Light>().intensity = lighOffIntensity;
                    }
                }

            }
            else //We inform you via the Console that you forgot to populate the array
            {
                Debug.LogError("You forgot to populate the array with lightsources!");
            }
        }

        /*
         * This function is changing the intensity of all lightsources is the 'lights' array
         */
        private void ChangeLightning()
        {
            //Play an audioclip to give feedback to the player that the button was pressed
            source.Play();

            //Make sure that we populated the array
            if (lights.Length > 0)
            {
                //For every instance of objects in the 'lights' array, perform a task
                for (int i = 0; i < lights.Length; i++)
                {
                    //If the bool is false, the lights are currently off. Turn them on!
                    if (lightsOn == false)
                    {
                        //For the current item in the array, set the intensity of light equal to lightOnIntensity
                        lights[i].GetComponent<Light>().intensity = lightOnIntensity;
                    }
                    else //The bool is true, so the lights are turned on. Turn them off.
                    {
                        //For the current item in the array, set the intensity of light equal to lighOffIntensity
                        lights[i].GetComponent<Light>().intensity = lighOffIntensity;
                    }

                }

                //Set the bool to be the oposite of what it is now. This is easily done with this line of code.
                //lightsOn is equal to the oposite (!lightsOn) of what is is now. We do this to make sure that the
                //next time we click the button, the behaviour of this script is as intended. 
                lightsOn = !lightsOn;
            }
            else //We inform you via the Console that you forgot to populate the array
            {
                Debug.LogError("You forgot to populate the array with lightsources!");
            }
        }
    }
}

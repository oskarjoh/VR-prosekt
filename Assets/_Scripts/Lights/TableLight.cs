///
/// Attach this script to 'LightButton' under TableLight
///

namespace VRTK.Examples
{
    using UnityEngine;

    public class TableLight : VRTK_InteractableObject
    {
        [Tooltip("Drag the container for all 'light is on' objects here.")]
        //Reference to the container of objects that are to be visible when the light is turned on
        public GameObject lightsOn = null;
        [Tooltip("Drag the container for all 'light is off' objects here.")]
        //Reference to the container of objects that are to be visible when the light is turned off
        public GameObject lightsOff = null;

        //Boolean telling the script if the light is on or off. By default this is false, as is the light
        private bool on = false;
        //Boolean that is true of either of the variables above hasn't been declared.
        private bool error = false;

        //A reference to this gameobjects audiosource
        private AudioSource source = null;


        private void Start()
        {
            //Create a reference to this gameobjects audiosource
            source = this.GetComponent<AudioSource>();
            
            //If either of these gameobjects are null, the script will not work as intended. Warn the developer.
            if(lightsOn == null || lightsOff == null || source == null)
            {
                //We warn you, the developer, and also sets error to be true
                Debug.LogError("One or more of your variables have not been declared in TableLights.cs");
                error = true;

                //Stop this function from continuing.
                return;
            }

            //Based on what the on variable is, we either turn on or off the lights at start.
            if (!on)
            {
                lightsOff.SetActive(true);
                lightsOn.SetActive(false);
            }
            else
            {
                lightsOff.SetActive(false);
                lightsOn.SetActive(true);
            }

        }

        //Called whenever the player clicked on this gameobject with the controller. 
        public override void StartUsing(VRTK_InteractUse usingObject)
        {
            base.StartUsing(usingObject);

            //Make sure that all references has been made.
            if(error)
            {
                Debug.LogError("One or more of your variables have not been declared on TableLights.cs");
            }
            else
            {
                //The lightswitch was clicked, so change the lights!
                ChangeLightning();
            }
        }

        //Called when this gameobject was clicked on
        private void ChangeLightning()
        {
            //Play an audioclip to give feedback to the player that the button was pressed
            source.Play();

            //If the light is on, turn if off.
            if (on)
            {
                lightsOff.SetActive(true);
                lightsOn.SetActive(false);
            }
            else //The light is currently turned off, so turn if on.
            {
                lightsOff.SetActive(false);
                lightsOn.SetActive(true);
            }

            //Set the bool to be the oposite of what it currently is. This is an easy way to say
            //"Do the opposite the next time this function is called".
            on = !on;
        }
    }
}

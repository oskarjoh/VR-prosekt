///
/// Attach this script to 'MyColliders' under Crowbar
///

using UnityEngine;
using VRTK.Examples;

public class CrowbarAttemptFire : MonoBehaviour
{
    [Tooltip("Drag all children that are going to behave as colliders into this array")]
    //An array of all the objects used for hit detection when attempting to break free a plank from the birdhouse
    public GameObject[] myChildren;
    //A reference to this gameobjects audiosource.
    private AudioSource source;

    //Sometimes we want a variable to be private, yet accessible in the Inspector. 
    //This is done by writing [SerializeField] in the line above or next to the variable
    [Tooltip("This is the audioclip that plays when successfully removing a plank from the birdhouse")]
    [SerializeField] private AudioClip clipSuccess; //Audioclip played when freeing a plank
    [Tooltip("This is the audioclip that plays when none of the 'myChildren' objects hits a plank")]
    [SerializeField] private AudioClip clipFailed; //Audioclip played when not freeing a plank

    private void Awake()
    {
        //Create the reference to this gameobjects audiosource
        source = this.GetComponent<AudioSource>();
    }

    //Attempts to break free the plank from the birdhouse
    public void AttemptFire()
    {
        //We want to check if either of the children in 'myChildren' is hitting a plank, and to
        //do se we will use a foreach loop. In a foreach loop you go through every object in an
        //array, just as you could do with a regular for loop. You have less control when using 
        //a foreach loop, but for this case it's easier to use.
        foreach(GameObject go in myChildren)
        {
            //Returns a reference to the gameobject the current child is hitting. Will return 
            //null if not hitting a birdhouse plank, and the plank if actually hitting a plank.
            GameObject temp = go.GetComponent<CrowbarHitboxChecker>().HittingPlank();

            //The child is hitting a birdhouse plank, so we release it!
            if (temp != null)
            {
                //Play the success audioclip
                source.clip = clipSuccess;
                source.Play();

                //Tell the plank to release itself
                temp.GetComponent<BirdhousePlank>().SetAsPlank(go.transform.position);

                //Stop this entire function from continuing. If we continue, and multiple
                //children hit different planks, all planks will be released. This is not
                //wanted. 
                //A return; statement will end the function immediately.
                return;
            }
        }

        //None of the children hit a valid plank, so we play the failed audioclip.
        //We know this, because if a plank got hit the return statement would prevent
        //these lines from ever being called.
        source.clip = clipFailed;
        source.Play();
    }

}

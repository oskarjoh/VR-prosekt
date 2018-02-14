///
/// Attach this script to 'MyHitbox' under NailGun
///

using UnityEngine;
using VRTK.Examples;

public class NailgunHitboxChecker : MonoBehaviour {

    [Tooltip("Drag the nail prefab into this variable. This is the object that spawns when nailing a plank.")]
    //A reference to the nail prefab
    public GameObject nail;

    //A reference to the plank the nail gun is hitting
    private GameObject hitPlank = null;
    //Reference to this gameobjects audiosource
    private AudioSource source;

    //Sometimes we want a variable to be private, yet accessible in the Inspector. 
    //This is done by writing [SerializeField] in the line above or next to the variable
    [Tooltip("The audio clip that plays when successfully nails a plank to the birdhouse")]
    [SerializeField]
    private AudioClip clipSuccess;
    [Tooltip("The audio clip that plays when shooting into thin air")]
    [SerializeField] private AudioClip clipFailed;

    private void Awake()
    {
        //Creates a referende to this gameobjects audiosource
        source = this.GetComponent<AudioSource>();
    }

    public void AttemptFire()
    {
        //If hitPlank isn't null, we know we have saved a reference to a valid plank. Perform nail gun fire
        if (hitPlank != null)
        {
			SpawnNail();
            //Call the function on the plank that initiates the mergin
            hitPlank.GetComponent<BirdhousePlank>().SetAsHouse();
        }

        //Play the currently set audioclio
        source.Play();
    }

    //Checks to see if the nail gun is inside something
    private void OnTriggerEnter(Collider other)
    {
        //The nail gun is inside a gameobject with the tag "ReadyPlank"
        if (other.transform.tag == "ReadyPlank")
        {
            //True if the current plank we're hitting is snapped to the birdhouse
            //Only when it's snapped can we actually nail it to the birdhouse
            if (other.gameObject.GetComponent<BirdhousePlank>().IsInSnapDropZone() == true)
            {
                //Save a reference to the plank gameobject
                hitPlank = other.gameObject;

                //Set the planks Highlight object to be active
                if (other.transform.childCount > 0)
                {
                    //Set the planks Highlight object to be disabled
                    other.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                }
                //Update the audioclip to be the successfull one. 
                source.clip = clipSuccess;
            }
        }

        //We also need to check if the nail gun is hitting a plank already part of the birdhouse.
        if (other.transform.tag == "House")
        {
            //Save the reference to the current plank
            hitPlank = other.gameObject;
            //Update the audioclip to be the successful one
            source.clip = clipSuccess;

            //Set the planks Highlight object to be active
            if (other.transform.childCount > 0)
            {
                //Set the planks Highlight object to be disabled
                other.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
            //Debug.Log("Hit House");
        }
    }

    //Checks to see if the nail gun no longer is inside another object
    private void OnTriggerExit(Collider other)
    {
        //True if the object it lost its collision with was the same as we saved a reference to
        if (other.transform.gameObject == hitPlank)
        {
            //Remove the reference to the plank object
            hitPlank = null;

            if (other.transform.childCount > 0)
            {
                //Set the planks Highlight object to be disabled
                other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }

            //Update the audioclip to be the failed one
            source.clip = clipFailed;
        }
    }

    private void SpawnNail()
    {
        //We spawn a copy of the nail prefab. We set the position and rotation of the newly spawner object to be equal to this objects
        //position and rotation. 
        Instantiate(nail, this.transform.position, this.transform.rotation);
    }
}

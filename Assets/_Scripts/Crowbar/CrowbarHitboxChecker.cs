///
/// Attach this script to 'Collider1' and 'Collider2' under Crowbar
///

using UnityEngine;

public class CrowbarHitboxChecker : MonoBehaviour
{
    //A reference to the plank this gameobject is hitting
    private GameObject hitPlank = null;

    //Returns a reference to the plank this object is hitting. 
    //This function is used when attempting to break free a plank from the birdhouse.
    //If this gameobject is inside a valid plank, it will return a reference to that plank.
    //If not, it returns null.
    public GameObject HittingPlank()
    {
        //If this variable isn't null, we know this object is inside a valid plank.
        if (hitPlank != null)
        {
            //Return the reference to the plank
            return hitPlank;
        }
        else //This object is inside a valid plank
        {
            //Return null
            return null;
        }
    }

    //This function is called whenever this gameobject collides with a new gameobject.
    //We use this to check if the crowbar is hitting a birdhouse plank
    private void OnTriggerEnter(Collider other)
    {
        //The tag of the gameobject this object is hitting is 'House'
        if (other.transform.tag == "House")
        {
            //Update the reference to be the new plank
            hitPlank = other.gameObject;

            //For safety and stability, perform this check (ensure that number of children is greater than 0)
            if (other.transform.childCount > 0)
            {
                //Set the planks Highlight object to be enabled
                other.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    //This function is called whenever this gameobject leaves the collider of another gameobject.
    //We use this to check if this object no longer hits the hitPlank gameobject.
    private void OnTriggerExit(Collider other)
    {
        //True if this gameobject lost its collision with the hitPlank object
        if (other.transform.gameObject == hitPlank)
        {
            //Remove the reference to the plank object
            hitPlank = null;

            //For safety and stability, perform this check (ensure that number of children is greater than 0)
            if (other.transform.childCount > 0)
            {
                //Set the planks Highlight object to be disabled
                other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}

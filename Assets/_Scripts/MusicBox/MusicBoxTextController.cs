///
/// Attach this script to 'Text' under MusicBox
///

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// This script moves the text inside the Music Box object.
/// </summary>
public class MusicBoxTextController : MonoBehaviour
{
    //A reference to this gameobjects text component
    private Text myText;
    //A reference to this gameobjects rect transform component
    private RectTransform myRect;

    [Tooltip("This variable is used to manipulate the calculation of the start position of the textfield")]
    //This variables is used to manipulate the calculation of the "startPosition" variable
    public float startPositionModifier = 0.75f;

    [Tooltip("This variable is used to manipulate the calculation of the target position")]
    //This variable is used to manipulate the calculation of the "targetPosition" variable
    public float targetModified = 0.9f;

    [Tooltip("This variable defines the movement speed of the text")]
    //This variable controls how fast the text should move
    public float speed = 50f;
    //Stores the previous rect transform scale. Used to check if a new song started playing.
    //Used to check when we need to update the target and start position.
    private float previousRectScale;

    //Position we want the text to move to
    private Vector3 targetPosition;
    //Position the text should have when starting it's movement.
    private Vector3 startPosition;

	// Use this for initialization
	void Awake ()
    {
        //Gets the reference to this gameobjects text component
        myText = this.GetComponent<Text>();
        //Gets the reference to this gameobjects rect transform component
        myRect = this.GetComponent<RectTransform>();
        //Stores the current x scale of this rect transform
        previousRectScale = myRect.sizeDelta.x;
    }

    private void Update()
    {
        //If the current x scale is different from the stored data, a new song started playing
        if(previousRectScale != myRect.sizeDelta.x)
        {
            //Update the current x scale
            previousRectScale = myRect.sizeDelta.x;
            //Calculate new position to move towards
            targetPosition = new Vector3(-myRect.sizeDelta.x * targetModified, 0, 0);
            //Calculate the position the text should go to when it has reached the target position
            startPosition = new Vector3(Mathf.RoundToInt(myRect.sizeDelta.x) * startPositionModifier, this.transform.localPosition.y, this.transform.localPosition.z);
        }
    }

    //This is a corouting. A coroutine runs for a given amount of time.
    //In this case, the coroutine runs for as long as the while loop is true. 
    private IEnumerator MoveText()
    {
        //While the distance between this gameobject (the text) and the target position is greater than 
        //0.05, continue moving towards that point.
        while (Vector3.Distance(this.transform.localPosition, targetPosition) >= 0.05f)
        {
            //Move towards the target
            this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, targetPosition, speed * Time.deltaTime);

            //Stop running the code, check if the while loop is true or false
            yield return null;
        }

        //This line of code is only called once the while loop is no longer true.
        //That means that the distance between this gameobject and the target position is low enough
        //to reset the position of this gameobject
        ResetPosition();
    }

    //Reset the position of this gameobject
    private void ResetPosition()
    {
        //Stop the coroutine, just to make sure it really did end
        StopCoroutine("MoveText");

        //Set the position of this gameobject
        this.transform.localPosition = startPosition;

        //Start moving again
        StartCoroutine("MoveText");
    }

    //This function is called whenever the player changes song.
    //Here we update the displayed text
    public void NewText(string newText)
    {
        //Stop moving the text until it has changed
        StopCoroutine("MoveText");

        //Set the new text
        myText.text = newText;

        //Start moving again
        StartCoroutine("MoveText");
    }

    //Called whenever the player pauses the music. 
    public void Paused(string newText)
    {
        //Stop moving the text
        StopCoroutine("MoveText");

        //Display the new status
        myText.text = newText;

        //Set the position of the text to be centered
        this.transform.localPosition = new Vector3(0, 0, 0);
    }

    //Called whenever the player resumes a song (unpauses)
    public void Resumed(string newText)
    {
        //Display the name of the song currently playing
        myText.text = newText;

        //Reset position of song
        this.transform.localPosition = startPosition;

        //Start moving text
        StartCoroutine("MoveText");
    }
}

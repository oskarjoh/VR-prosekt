///
/// Attach this script to 'Birdhouse'
///

using UnityEngine;

public class HouseProgress : MonoBehaviour
{
    //Creates a public reference to this script. To access the functions and variables in this script,
    //other scripts can just type HouseProgress.progress.wantedPublicFunction();
    public static HouseProgress progress;

    [Tooltip("Click and drag the 'Party' gameobject under 'PartyObjects' into this field. This is an example of how to celebrate the end of the game.")]
    //A reference to the party gameobjects
    public GameObject partyButton;

    [Tooltip("How many planks must be nailed to the house for the game to end")]
    //This variable defines the required amount of planks for the birdhouse to be completed
    public int targetPlanks = 4;

    [Tooltip("How many nails that are currently nailed to the house")]
    //This variable holds the current amount of planks applied to the birdhouse
    public int planksPlaced = 1;

    //False until the first time the game has been completed
    private bool gameCompleted = false;

    private void Awake()
    {
        //Set the reference
        progress = this;
    }

    //Called when a plank is added to the birdhouse
    public void PlankAdded()
    {
        //Add a plank
        planksPlaced++;

        //If planksPlaced is the same as targetPlanks, the required amount has been added to the birdhouse!
        if(planksPlaced == targetPlanks)
        {
            Debug.Log("You just won the game!");

            if(!gameCompleted)
            {
                CompleteGame();
            }
        }
    }

    //Called when a plank is removed from the birdhouse
    public void PlankRemoved()
    {
        planksPlaced--;
    }

    private void CompleteGame()
    {
        //The game has now been completed
        gameCompleted = true;

        if (partyButton != null)
        {
            //Set the party objects to be active
            partyButton.SetActive(true);
        }
        else
        {
            Debug.LogError("You are missing a reference to the party gameobject!");
        }
    }
}

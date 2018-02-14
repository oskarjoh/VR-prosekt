///
/// Attach this script to 'MusicBox'
///

using UnityEngine;

public class MusicBoxController : MonoBehaviour
{
    //A reference to this gameobjects audiosource. Used when switching song
    private AudioSource source;

    [Tooltip("An array containing all the audioclips you want to be able to play ingame.")]
    //An array containing all the audiofiles you want to be able to play during runtime
    public AudioClip[] clips;

    [Tooltip("This number represents what song to start the loop at. The first song is 0")]
    //The position in 'clips' to start the loop
    public int currentClip = 0;

    [Tooltip("A reference to the textfield that displays the name of the song currently playing. It's a child of this gameobject.")]
    //A reference to the "name of the song" text controller
    public MusicBoxTextController musicText;

    //A variable used when switching songs. If the music box is playing a song at the time we switch, 
    //we want it to play the next song imediately. If it isn't playing, we don't start playing the new song.
    private bool playing = true;

    private void Start()
    {
        //Creates a reference to this gameobjects audiosource
        source = this.GetComponent<AudioSource>();
        //Set the clip to play to be the "currentClip" in clips array
        source.clip = clips[currentClip];
        //Start playing the song
        source.Play();

        //Display the name of the song currently playing
        musicText.NewText("Now playing  " + clips[currentClip].name);
    }

    //The player pressed on the play icon.
    //If the audiosource is playing, we are going to pause it.
    //If not, unpause it
    public void PlayAction()
    {
        //The audiosource is currently playing, so pause it
        if (source.isPlaying)
        {
            //Pause the audio
            source.Pause();
            //Display "Paused" in the textfield
            musicText.Paused("Paused");
        }
        else //Audiosource is not playing, so start playing
        {
            //Start playing
            source.Play();
            //Display the name of the song currently playing
            musicText.Resumed("Now playing " + source.clip.name);
        }

        //Set the value of 'playing' to be the oposite of whatever it is now
        playing = !playing;
    }

    //The player wants to play the next song in the array
    public void NextAction()
    {
        //Update the currentClip variable
        currentClip++;

        //If the number of currentClip is greater or equal to the size of clips, we will get an IndexOutOfRange exception.
        //Basically, you can't try to reach a number in an array that has not yet been defined. 
        //Therefore, we restart the loop.
        if (currentClip >= clips.Length)
            currentClip = 0;

        //Play the next song
        source.clip = clips[currentClip];

        //If the music box was playing a song at the time the player pressed this button, we want to instantly play the new song
        if (playing)
        {
            //Play the new song
            source.Play();
            //Update the display with the name of the currently playing song
            musicText.NewText("Now playing  " + source.clip.name);
        }
    }

    //The player wants to play the next song in the array
    public void PreviousAction()
    {
        //Update the currentClip variable
        currentClip--;

        //If the value of currentClip is lower than 0, we know we have reached the start of the loop. Therefore, we go to the end to perform the loop.
        //If we don't do this, we will attempt to reach index -1 in the array, resulting an IndexOutOfRange exception
        if(currentClip < 0)
            currentClip = clips.Length - 1;

        source.clip = clips[currentClip];

        //If the music box was playing a song at the time the player pressed this button, we want to instantly play the new song
        if (playing)
        {
            //Play the new song
            source.Play();
            //Update the display with the name of the currently playing song
            musicText.NewText("Now playing  " + source.clip.name);
        }
    }
}

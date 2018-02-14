///
/// Attach this script to 'PlayPause', 'Next' and 'Previous' under MusicBox
///

namespace VRTK.Examples
{
    using UnityEngine;
    /// <summary>
    /// This script is applied to all the buttons on the music box object.
    /// This script just checks what type the button is, and perform the 
    /// correct action in MusicBoxController
    /// </summary>
    public class MusicBoxButtons : VRTK_InteractableObject
    {
        //These variables are visible in the inspector. 
        //For every object, set the corresponding bool to true
        [Tooltip("Set to true if this is the 'play/pause' button")]
        public bool isPlayButton = false;
        [Tooltip("Set to true if this is the 'previous song' button")]
        public bool isPreviousButton = false;
        [Tooltip("Set to true if this is the 'next song' button")]
        public bool isNextButton = false;

        [Tooltip("Drag the gameobject containing the 'MusicBoxController' script into here.")]
        //Create a reference to the MusicBoxController script
        public MusicBoxController myParent = null;

        /// <summary>
        /// This script is called whenever the player presses on a button with this script attached to it.
        /// </summary>
        /// <param name="usingObject"></param>
        public override void StartUsing(VRTK_InteractUse usingObject)
        {
            base.StartUsing(usingObject);

            //We check what button the player pressed on. This check is based on the bools above, and 
            //what you defined in the inspector
            if (isPlayButton) //The player pressed on the "Play" button.
                myParent.PlayAction(); //Either play or pause the music, depending on the current status
            else if (isPreviousButton) //The player wants to play the previous song
                myParent.PreviousAction(); //Play the previous song
            else if (isNextButton) //The player wants to play the next song
                myParent.NextAction(); //Play the next song
        }
    }
}

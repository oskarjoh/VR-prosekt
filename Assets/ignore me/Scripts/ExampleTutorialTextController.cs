namespace VRTK.Examples
{
    using UnityEngine;
    using UnityEngine.UI;

    public class ExampleTutorialTextController : VRTK_InteractableObject
    {
        public string[] allText;

        public Text tutorialText;
        public Text pageText;

        private int currentText = 0;

        // Use this for initialization
        void Start()
        {
            tutorialText.text = allText[currentText];
            pageText.text = "Page " + (currentText + 1) + " / " + allText.Length;
        }

        /*
 * This function comes from the plugin VRTK. When a controller interracts with the object
 * holding this script, this function is called. It functions as a "OnCollisionEnter if criteria is met"
 * handler.
 */
        public override void StartUsing(VRTK_InteractUse usingObject)
        {
            base.StartUsing(usingObject);

            currentText++;

            if (currentText >= allText.Length)
                currentText = 0;

            pageText.text = "Page " + (currentText + 1) + " / " + allText.Length;

            tutorialText.text = allText[currentText];
        }
    }
}

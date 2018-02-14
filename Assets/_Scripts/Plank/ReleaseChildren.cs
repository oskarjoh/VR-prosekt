///
/// Attach this script to 'Plank'
///

using UnityEngine;

public class ReleaseChildren : MonoBehaviour
{
    [Header("Reference to the planks that are child of this gameobject")]
    //These are references to this gameobjects children
    public GameObject plank1;
    public GameObject plank2;

    //Called when HitsPlank saws a plank in half. This function enables the sawn planks, and disables the root plank
    public void Release()
    {
        //Make them children of nothing, so they are separate world objects
        plank1.transform.parent = null;
        plank2.transform.parent = null;

        //Make them active and visible
        plank1.SetActive(true);
        plank2.SetActive(true);

        //Remove this gameobject from the scene
        Destroy(this.gameObject);
    }
}

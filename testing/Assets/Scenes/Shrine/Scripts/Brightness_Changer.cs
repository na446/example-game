using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brightness_Changer : MonoBehaviour
{
    public Light lt;
    //public GameObject boundary;
    public GameObject headset;

    //The following two methods below is to adjust brightness. They are both click-once type buttons.
    public void add_Bright()
    {
        lt.intensity = lt.intensity + 0.1f; //adds 0.1 to the hallway directional light in the scene
    }

    public void subtract_Bright()
    {
        lt.intensity = lt.intensity - 0.1f; //subtracts 0.1 to the hallway directional light in the scene
    }

    //This is to set the buttons to the headset in Vive VR. 
    public void ChildToHeadset()
    {
        Vector3 currentPosition = transform.localPosition;
        Quaternion currentRotation = transform.localRotation;
        Vector3 currentScale = transform.localScale;
        Transform newParent = null;

        //switch (sdkObject)
        //{
        //    //case SDKObject.Boundary:
        //        newParent = VRTK_DeviceFinder.PlayAreaTransform();
        //        break;
        //    case headset:
        //        newParent = VRTK_DeviceFinder.HeadsetTransform();
        //        break;
        //}

        //transform.SetParent(newParent);
        //transform.localPosition = currentPosition;
        //transform.localRotation = currentRotation;
        //transform.localScale = currentScale;
    }

}

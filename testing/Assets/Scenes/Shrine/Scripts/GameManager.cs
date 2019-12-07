using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class GameManager : MonoBehaviour
    {

        void RecordData() //calls the acutal getting data function in the CSVWriter script
        {
            CSVWriter.instance.getData();
        }

        // Start is called before the first frame update
        void Start()
        { 
            CSVWriter.instance.shrineRecording();
            InvokeRepeating("RecordData", 1f, 0.05f);//at the 1st second (in-game time) it will record data every .05 second
        }

    }
}
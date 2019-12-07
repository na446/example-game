using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public InputField participantNum; // participantNum is being initialized to set them to values later
    /*
    participantNum has to be initialized on gameObject's script page in the inspector
    from there, you have to go to the button and change the on-click function 
    */
    public static StartGame instance = new StartGame();

    IEnumerator ExposureTime()
    {
        SceneManager.LoadScene("Shrine");
        yield return new WaitForSeconds(900);//15 mintues in real time
        Application.Quit();
    }

    // DO_FINAL encompasses all the functions for the onclick part of the button
    //stores data from list once input file box is filled out by user at the start menu

    public void Do_FINAL()
    {
        if (participantNum.text != "") 
        {
            PlayerPrefs.GetString("Participant", participantNum.text);
            if (SceneManager.GetActiveScene().name == "Input_Menu")
            {
                CSVWriter.instance.writeFilename(participantNum.text);
                try
                {
                    StartCoroutine(ExposureTime());

                }
                catch (Exception ex)
                {
                    throw new ApplicationException("#### Error in Do_FINAL ####", ex);
                }
            }
            else
            {
                CSVWriter.instance.writeFilename(participantNum.text);
                try
                {
                    SceneManager.LoadScene("Tutorial");

                }
                catch (Exception ex)
                {
                    throw new ApplicationException("#### Error in Do_FINAL ####", ex);
                }
            }
            
            
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

}

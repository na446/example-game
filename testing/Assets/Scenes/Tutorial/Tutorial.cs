using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

public class Tutorial : MonoBehaviour
{
    //script references
    public ButtonTransitionerer button;
    public ButtonTransitionerer buttonY;
    public ButtonTransitionerer buttonZ;
    public ButtonTransitionerer buttonB;

    public GameObject text1; //button step
    public GameObject text2; //teleporting step
    public GameObject text3; //walking step
    public GameObject text4; //last step
    public GameObject button1;//next button
    public GameObject button2;//continue button
    public GameObject button3;//done button
    public GameObject backButton;//back button
    public GameObject thePanel; //background color for text

    public void Start()
    {
        text1.SetActive(true);
        button1.SetActive(true);
        thePanel.SetActive(true);
        //StartCoroutine(WaitForText());
    }

    public void StartGame()
    {
        button3.SetActive(false);
        thePanel.SetActive(false);
        text4.SetActive(false);
        Application.Quit();
    }

    public void Update()
    {
        if (button.nextT())
        {
            text2.SetActive(true);
            button2.SetActive(true);
            button1.SetActive(false);
            text1.SetActive(false);

            button.next = false;
        }
        else if (buttonY.continueT())
        {
            text3.SetActive(true);
            button3.SetActive(true);
            backButton.SetActive(true);
            button2.SetActive(false);
            text2.SetActive(false);

            buttonY.continueButton = false;
        }
        else if (buttonZ.doneT())
        {
            text4.SetActive(true);
            button3.SetActive(false);
            backButton.SetActive(false);
            text3.SetActive(false);
            StartCoroutine(WaitForText());

            buttonZ.done = false;
        }
        else if (buttonB.backT())//this button goes back to the teleporting step
        {
            text2.SetActive(true);
            button2.SetActive(true);
            text3.SetActive(false);
            button3.SetActive(false);
            backButton.SetActive(false);
            button1.SetActive(false);
            text1.SetActive(false);

            buttonB.back = false;
        }
    }

    
    private IEnumerator WaitForText()
    {
        //if (text1.activeSelf == true)
        /*{
            yield return new WaitUntil(() => button.nextT());
            text2.SetActive(true);
            button2.SetActive(true);
            button1.SetActive(false);
            text1.SetActive(false);
            yield return new WaitUntil(() => buttonY.continueT());
            text3.SetActive(true);
            button3.SetActive(true);
            button2.SetActive(false);
            text2.SetActive(false);
            yield return new WaitUntil(() => buttonZ.doneT());
            text4.SetActive(true);
            button3.SetActive(false);
            text3.SetActive(false);*/
            yield return new WaitForSeconds(10);
            StartGame();
        //}
    }
}

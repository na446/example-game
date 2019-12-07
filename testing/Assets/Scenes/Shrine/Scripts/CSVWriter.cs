using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;
using System.Text;
using Tobii.XR;


struct dataForEye
{
    public string playerPos; //Position of where the player is in game
    public string playerRo;
    public string Lhand; //The player's left hand, in world coordinates 
    public string Rhand; //The player's right hand, in world coordinates 
    public string gazePoint; //Point of collision of an object in the game with left eye, in world coordinates
    public string rayDirection;
    public string nameOfObj;
    public float time; //Time spent in the shrine scene

    public dataForEye(Vector3 position, Quaternion rotation, Vector3 leftPosition, Vector3 rightPosition, Vector3 gaze, Vector3 r, string name, float t)
    {
        playerPos = position.x + "," + position.y + "," + position.z;
        playerRo = rotation.x + "," + rotation.y + "," + rotation.z;
        Lhand = leftPosition.x + "," + leftPosition.y + "," + leftPosition.z;
        Rhand = rightPosition.x + "," + rightPosition.y + "," + rightPosition.z;
        gazePoint = gaze.x + "," + gaze.y + "," + gaze.z;
        rayDirection = r.x + "," + r.y + "," + r.z;
        nameOfObj = name;
        time = t;
    }
}

public class CSVWriter : MonoBehaviour
{
    public static CSVWriter instance = new CSVWriter();

    private static string path = Application.persistentDataPath;//in C:\Users\nadaa\AppData\LocalLow\DefaultCompany
    private static string now;  // naming convention for file --> based on time
    private static string num;//naming convention for file pt. 2 --> file name will begin with partcipant number
    private static string file_unit;// tells the program which file to write to
       

    public GameObject leftHand;
    public GameObject rightHand;
    public Camera player;  
    private Vector3 EyeTarget;

    //The values/gameobjects to calculate eye tracking data.
    private Vector3 rayDir;
    private float convDistance;
    public Vector3 rayOrigin;

    private float _defaultDistance;
    private Camera _mainCamera;
    private Vector3 tempVector;
    string objectName;

    public GameObject CSVRecorder;
    
    public void shrineRecording() //finds all the gameobjects and scripts when the shrine scene starts 
    {
        player = CameraHelper.GetMainCamera();//GameObject.Find("Player");
        rightHand = GameObject.Find("LeftHand");
        leftHand = GameObject.Find("RightHand");
        CSVRecorder = GameObject.Find("CSVRecorder");
    }
 
    public  void writeFilename(string num)
    {
        now = DateTime.Now.ToString("_dddd_dd_MMMM_yyyy_HH_mm"); //takes current day, month, year, and hour and min. 
        file_unit = path + num + now + ".csv";  // tells the program which file to write to
        quickAdd("yeet");//adds the heading that you will see in row 1 of the excel sheet
    }

    static void structToCSV(dataForEye data, string filename)
    {
        try
        {
            using (StreamWriter file = new StreamWriter(filename, true))
            {
                //if (data.Lhand == "(0,0,0)")
                //    file.WriteLine("STOP" + "," + "STOP" + "," + "STOP" + "," + "STOP" + "," + "STOP");

                file.WriteLine(data.playerPos + "," + data.playerRo + "," + data.Lhand + "," + data.Rhand + "," + data.gazePoint + "," + data.rayDirection + "," + data.nameOfObj + "," + data.time);
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("error in list to CSV function in CSVWriter.cs file", ex);
        }
    }

    public void getData()
    {
        Vector3 leftCoor;
        Vector3 rightCoor;
        Vector3 targetEye;
        Vector3 playPos = Vector3.zero;

        try
        {
            if (player.transform.position.y >= -1.898146)
            {
                playPos = player.transform.position;//takes player's position if above floor 
            }

            Quaternion playerRot = player.transform.rotation;//take's the head/player's rotation

            float time = TobiiXR.EyeTrackingData.Timestamp;//returns the current time in-game

            var gazeray = TobiiXR.EyeTrackingData.GazeRay;

            if (TobiiXR.EyeTrackingData.GazeRay.IsValid && TobiiXR.EyeTrackingData.ConvergenceDistanceIsValid)
            {
                //Assign values here
                rayOrigin = TobiiXR.EyeTrackingData.GazeRay.Origin;
                rayDir= TobiiXR.EyeTrackingData.GazeRay.Direction; //collects the direction of the gaze
                convDistance = TobiiXR.EyeTrackingData.ConvergenceDistance; //collects the convergence distance of the gaze
                leftCoor = leftHand.transform.position; //takes the Steam VR controller lefthand position
                rightCoor = rightHand.transform.position; //takes the Steam Vr controller right hand position
                targetEye = TargetCalculator(rayOrigin, rayDir);//x,y,z and of what the right eye is looking at 

                if (TobiiXR.FocusedObjects.Count > 0)
                {
                    var focusedObject = TobiiXR.FocusedObjects[0];
                    tempVector = TargetCalculator(focusedObject.Origin, focusedObject.Direction);
                    //Debug.Log(tempVector + "," + GameObject.Find("GazeVisualizer").transform.position);
                }

                if (TobiiXR.FocusedObjects.Count == 0)
                    tempVector = Vector3.zero;

                //the following two lines below will write all variables to file
                dataForEye data = new dataForEye(playPos, playerRot, leftCoor, rightCoor, targetEye, rayDir, objectName, time);
                structToCSV(data, file_unit);
            }
            else
            { // else if eye tracking is not enabled, all values will be set to (0,0,0)
                leftCoor = Vector3.zero;
                rightCoor = Vector3.zero;
                targetEye = Vector3.zero;
                objectName = "Nothing. Headset may have been removed/is blinking.";

                //the following two lines below will write all variables to file
                dataForEye data = new dataForEye(playPos, playerRot, leftCoor, rightCoor, targetEye, rayDir, objectName, time);
                structToCSV(data, file_unit);
            }


            //Debug.Log(playPos + "//" + playerRot + "//" + leftCoor + "//" + rightCoor + "//" + targetEye + "//" + rayDir + "//" + objectName + "//" + time); // Logs the data that should be received in the console

        }
        catch (Exception ex)
        {
            throw new ApplicationException("Errors in CheckerGoWork.cs file", ex);
        }
    }

    private void quickAdd(string name)
    {
        using (StreamWriter file = new StreamWriter(file_unit, true))
        {
            file.WriteLine("X_Player Position, Y_Player Position, X_Player Rotation, Y_Player Rotation, Z_Player Rotation, Z_Player Position, X_Left Hand Position, Y_Left Hand Position, Z_Left Hand Position, X_Right Hand Position, Y_Right Hand Position, Z_Right Hand Position, X.Eye, Y.Eye, Z.Eye, RayDir.X, RayDir.Y, RayDir.Z, GameObject, Time");
        }
    }


    Vector3 TargetCalculator(Vector3 focusOrigin, Vector3 focusDir)
    {
        RaycastHit hit;
        var distance = _defaultDistance;
        objectName = "Unidentified";

        if (Physics.Raycast(focusOrigin, focusDir, out hit))
        {
            distance = hit.distance;
            objectName = hit.collider.gameObject.name;
        }

        Vector3 output = focusOrigin + focusDir.normalized * distance;
        return output;
    }

    void Start()
    {
        _mainCamera = CameraHelper.GetMainCamera();
        _defaultDistance = _mainCamera.farClipPlane - 10;
        TobiiXR.Start();
    }
} 

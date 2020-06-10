/**
 * Ardity (Serial Communication for Arduino + Unity)
 * Author: Daniel Wilches <dwilches@gmail.com>
 *
 * This work is released under the Creative Commons Attributions license.
 * https://creativecommons.org/licenses/by/2.0/
 */

using UnityEngine;
using System.Collections;

/**
 * Sample for reading using polling by yourself, and writing too.
 */
public class SampleUserPolling_ReadWrite : MonoBehaviour
{
    public SerialController serialController;
    public IK iKRobot;

    int framecount;
    // Initialization
    void Start()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
        this.iKRobot = GameManager.instance.ikMotor;
        Debug.Log("Press A or Z to execute some actions");
        DontDestroyOnLoad(this.gameObject);
    }

    // Executed each frame
    void Update()
    {
        framecount++;

        string messageAngles = "";
        Vector3 angles = iKRobot.getAnglesForMotorAsVec3();
        //Adding the base angle in the message
        messageAngles += "b";
        if(Mathf.Abs(angles.x) < 100)
        {
            messageAngles += "0";
            if(Mathf.Abs(angles.x) < 10)
            {
                messageAngles += "0";
            }
        }
        messageAngles += Mathf.Abs(angles.x);
        //Adding the left angle in the message
        messageAngles += "l";
        if (Mathf.Abs(angles.y) < 100)
        {
            messageAngles += "0";
            if (Mathf.Abs(angles.y) < 10)
            {
                messageAngles += "0";
            }
        }
        messageAngles += Mathf.Abs(angles.y);
        //Adding the right angle in the message
        messageAngles += "r";
        if (Mathf.Abs(angles.z) < 100)
        {
            messageAngles += "0";
            if (Mathf.Abs(angles.z) < 10)
            {
                messageAngles += "0";
            }
        }
        messageAngles += Mathf.Abs(angles.z);

        if (framecount%120 ==0)
        {
            //Debug.Log("Sending "+ messageAngles);
            serialController.SendSerialMessage(messageAngles);
            framecount = 1;
        }

        //---------------------------------------------------------------------
        // Send data
        //---------------------------------------------------------------------

        // If you press one of these keys send it to the serial device. A
        // sample serial device that accepts this input is given in the README.
        if (Input.GetKeyDown(KeyCode.A))
        {
            //Debug.Log("Sending b120l090r090");
            serialController.SendSerialMessage("b120l120r060");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            //Debug.Log("Sending b060l090r090");
            serialController.SendSerialMessage("b060l060r120");
        }


        //---------------------------------------------------------------------
        // Receive data
        //---------------------------------------------------------------------

        string message = serialController.ReadSerialMessage();

        if (message == null)
            return;

        // Check if the message is plain data or a connect/disconnect event.
        if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
        {
            Debug.Log("Connection established");
        }
        else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
        {
            //Debug.Log("Connection attempt failed or disconnection detected");
        }
        else
        {
            Debug.Log("Message arrived: " + message);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;


public class Arduino : MonoBehaviour
{
    static SerialPort serial;
    

    // Start is called before the first frame update
    void Start()
    {
        serial = new SerialPort();
        //sendMessageToArduino();
    }

    // Update is called once per frame
    void Update()
    {
        
           
    }

    static public void sendMessageToArduino(string message)
    {
        serial.PortName = "COM6";
        serial.Parity = Parity.None;
        serial.BaudRate = 9600;
        serial.DataBits = 8;
        serial.StopBits = StopBits.One;
        serial.NewLine = "\n";
        serial.Open();
        serial.Write(message);
        
        Debug.Log("wrote : " + message + " just now");

        Debug.Log("actually resent : "/* + serial.ReadLine()*/);
        serial.Close();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Runtime.InteropServices.WindowsRuntime;

public class ReadWriteToArduino : MonoBehaviour
{
	public IK ikrobot;
	public string port = "COM7";
	public int baudRate = 9600;
	private SerialPort stream;
	public float delayBetweenMessage = 500;

	private float lastSend = 0;

	private int lastAngle;

	private string message;
	
    // Start is called before the first frame update
    void OnEnable()
    {
		stream = new SerialPort(port, baudRate);
		stream.ReadTimeout = 100;
		stream.Open();
		message = "t090";
	}

	private void OnDisable()
	{
		stream.Close();
	}

	void Update()
    {
		var vec = ikrobot.getAnglesForMotorAsVec3();

		message = ConvertAngle((int)Mathf.Abs(vec.y));
		message += ConvertAngle((int)Mathf.Abs(vec.z));

		
		if(Time.time > lastSend + delayBetweenMessage){
			SendMessage();
			lastSend = Time.time;
		}
		
	}

	private string ConvertAngle(int angle){
		string str = "t";
		if (angle < 100) str += "0";
		if (angle < 10) str += "0";
		str += angle;
		return str;
	}


	public void SendMessage(){
		Debug.Log("Go : " + message);
		WriteToArduino(message);
	}

	public void WriteToArduino(string message)
	{
		stream.WriteLine(message);
		stream.BaseStream.Flush();
	}

	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Runtime.InteropServices.WindowsRuntime;

public class ReadWriteToArduino : MonoBehaviour
{
	public IK ikrobot;
	/*public string port = "COM7";
	public string port2 = "COM4";
	public string port3 = "COM6";
	public int baudRate = 1000000;*/
	private SerialPort stream;
	private SerialPort stream2;
	private SerialPort stream3;
	public float delayBetweenMessage = 500;
	public SerialPortSO serialPortSO;

	private float lastSend = 0;

	private int lastAngle;

	private string message;
	
    // Start is called before the first frame update
    void OnEnable()
    {
		stream = new SerialPort(serialPortSO.port, serialPortSO.baudRate);
		stream.ReadTimeout = 100;
		stream.Open();

		stream2 = new SerialPort(serialPortSO.port2, serialPortSO.baudRate);
		stream2.ReadTimeout = 100;
		stream2.Open();

		stream3 = new SerialPort(serialPortSO.port3, serialPortSO.baudRate);
		stream3.ReadTimeout = 100;
		stream3.Open();

		//message = "t090";
	}

	private void OnDisable()
	{
		if(stream.IsOpen)stream.Close();
		if (stream2.IsOpen) stream2.Close();
		if (stream3.IsOpen) stream3.Close();
	}

	void Update()
    {
		//if (!stream.IsOpen || !stream2.IsOpen || !stream3.IsOpen) return;
		var vec = ikrobot.getAnglesForMotorAsVec3();

		message = ConvertAngle((int)Mathf.Abs(vec.y));
		message += ConvertAngle((int)Mathf.Abs(vec.z));
		message += ConvertAngle((int)Mathf.Abs(vec.x));


		//offset direction will be -1 in arduino
		if(Input.GetKey(KeyCode.LeftArrow)){
			message += "0";//-> -1
		}else if(Input.GetKey(KeyCode.RightArrow)){
			message += "2";//-> 1
		}
		else{
			message += "1";//->0
		}

		//Debug.Log(message);


		if (Time.time > lastSend + delayBetweenMessage){
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
		if (stream != null && stream.IsOpen){
			stream.WriteLine(message);
			stream.BaseStream.Flush();
		}

		if (stream2 != null && stream2.IsOpen)
		{
			stream2.WriteLine(message);
			stream2.BaseStream.Flush();
		}

		if (stream3 != null && stream3.IsOpen)
		{
			stream3.WriteLine(message);
			stream3.BaseStream.Flush();
		}
	}

	
}

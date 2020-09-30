using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SerialPort", menuName = "Reminescence/SerialPort", order = 1)]

public class SerialPortSO : ScriptableObject
{
	public string port = "COM7";
	public string port2 = "COM4";
	public string port3 = "COM6";
	public int baudRate = 1000000;
}

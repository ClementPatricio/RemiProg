using UnityEngine;
using System.Collections;


//to do : move based on kinnect input, if no kinnect, move it in the editor
public class PointTransposer : MonoBehaviour
{
	public Transform playerPoint;
	public Transform robotPoint;

	public MatrixTransposer transposer;
	public Vector3 sensitivity;

	private Vector3 lastPos;

	private void Start()
	{
		lastPos = playerPoint.transform.position;
		robotPoint.transform.position = lastPos;
	}


	private void Update()
	{

		Vector3 dir = playerPoint.position - lastPos;
		robotPoint.transform.Translate(new Vector3(
			transposer.TranslatePosition(dir).x * sensitivity.x,
			transposer.TranslatePosition(dir).y * sensitivity.y,
			transposer.TranslatePosition(dir).z * sensitivity.z));

		lastPos = playerPoint.position;
	}
}

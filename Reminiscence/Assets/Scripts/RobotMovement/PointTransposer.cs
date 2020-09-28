using UnityEngine;
using System.Collections.Generic;
using Kinect = Windows.Kinect;
using Joint = Windows.Kinect.Joint;


//to do : move based on kinnect input, if no kinnect, move it in the editor
public class PointTransposer : MonoBehaviour
{
	public Transform robotPoint;
	public BodySourceManager bodySrcManager;

	public MatrixTransposer transposer;
	public Vector3 sensitivity;

	private Vector3 lastPos;

	private bool hasPlayer;

	public float maxDistance = 10;

	public KinnectParameter kinnectParam;
	//public Vector3 kinnectOffset;
	//public float kinnectPosMultiplier = 10;

	private List<Kinect.JointType> joints = new List<Kinect.JointType> {

		Kinect.JointType.HandLeft,
		Kinect.JointType.HandRight,
		Kinect.JointType.SpineMid,
		Kinect.JointType.Head
	};

	private void Start()
	{
		lastPos = transform.position;
		robotPoint.transform.position = lastPos;
	}


	private void Update()
	{

		Vector3 dir = transform.position - lastPos;

		Vector3 beforePos = robotPoint.position;

		robotPoint.transform.Translate(new Vector3(
			transposer.TranslatePosition(dir).x * sensitivity.x,
			transposer.TranslatePosition(dir).y * sensitivity.y,
			transposer.TranslatePosition(dir).z * sensitivity.z));



		lastPos = transform.position;

		Vector3 dirToRobot = robotPoint.position - Vector3.zero;
		Vector3 dirToBeforePos = beforePos - Vector3.zero;

		Debug.DrawLine(Vector3.zero, robotPoint.position, Color.magenta);

		if(dirToRobot.magnitude > maxDistance){
			if(dirToBeforePos.magnitude <= dirToRobot.magnitude){
				robotPoint.position = beforePos;
			}
			//robotPoint.transform.position = Vector3.zero + dirToRobot.normalized * maxDistance;
		}


		if (kinnectParam.overrideKinnect) return;
		if (!bodySrcManager) return;

		Debug.Log("yo kinnect");

		Kinect.Body[] data = bodySrcManager.GetData();
		if (data == null)
		{
			return;
		}

		Debug.Log("found some data : ");
		List<ulong> trackedIds = new List<ulong>();
		foreach (var body in data)
		{
			if (body == null)
			{
				continue;
			}
			
			if (body.IsTracked)
			{
				trackedIds.Add(body.TrackingId);
				DebugBody(body);
				Vector3 v = GetBodyPoint(body);
				//if (v == Vector3.zero) continue;

				transform.position = v;
				Debug.DrawLine(Vector3.zero, transform.position, Color.red);
			}

			
			//break;
		}


	}

	public void DebugBody(Kinect.Body body)
	{
		foreach (Kinect.JointType jt in joints)
		{
			Kinect.Joint sourceJoint = body.Joints[jt];
			Vector3 targetPosition = GetVector3FromJoint(sourceJoint);
			Debug.DrawLine(Vector3.zero, targetPosition);
		}
	}

	public Vector3 GetBodyPoint(Kinect.Body body)
	{
		//Vector3 v = Vector3.Lerp(body.Joints[Kinect.JointType.HandLeft].Position, body.Joints[Kinect.JointType.HandRight].Position, 0.5f);
		Vector3 lHand = GetVector3FromJoint(body.Joints[Kinect.JointType.HandLeft]);
		Vector3 rHand = GetVector3FromJoint(body.Joints[Kinect.JointType.HandRight]);
		Vector3 spine = GetVector3FromJoint(body.Joints[Kinect.JointType.SpineMid]);

		Vector3 vHand = Vector3.Lerp(lHand, rHand, 0.5f);
		Vector3 finalPoint = Vector3.Lerp(vHand, spine, 0.5f);
		return finalPoint;
	}

	private Vector3 GetVector3FromJoint(Kinect.Joint joint)
	{	
		Vector3 p = new Vector3(joint.Position.X * kinnectParam.kinnectScalePosition, joint.Position.Y * kinnectParam.kinnectScalePosition, joint.Position.Z * kinnectParam.kinnectScalePosition);
		return p + kinnectParam.kinnectOffset;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.grey;
		Gizmos.DrawSphere(transform.position, 0.5f);
		Gizmos.color = Color.cyan;
		Gizmos.DrawSphere(robotPoint.position, 0.5f);
	}
}

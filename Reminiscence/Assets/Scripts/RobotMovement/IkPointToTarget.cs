using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkPointToTarget : MonoBehaviour
{

	public Transform targetPoint;

	private CharacterController _controller;

	private void Awake()
	{
		_controller = GetComponent<CharacterController>();
	}
	void Update()
    {
		if (!targetPoint) return;

		Vector3 dir = targetPoint.position - transform.position;
		if(dir.magnitude < 1)
		{
			_controller.Move(dir);
			
		}else{
			dir = dir.normalized;
			_controller.Move(dir);
		}
		

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public float CameraSpeed;
	
	void Update ()
	{

		Vector3 dir = new Vector3 ( Input.GetAxis ( "Horizontal" ), 0, Input.GetAxis ( "Vertical" ) );
		this.transform.position = this.transform.position + dir * CameraSpeed;

	}
}

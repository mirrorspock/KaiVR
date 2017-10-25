using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaiVR
{
public class CanvasFaceCamera : MonoBehaviour {

	void Start () {
		//get direction (pos of canvas - pos camera)
		Vector3 direction = transform.position - Camera.main.transform.position;

		//set direction to forward
		transform.forward = direction;

	}
	
}
}
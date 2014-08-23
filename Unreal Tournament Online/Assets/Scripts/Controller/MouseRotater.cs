using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseRotater : MonoBehaviour {

	float  RotY;
	Transform camera, player;
	public float MinimumRotateY, MaximumRotateY, MinimumRotateX, MaximumRotateX, SmouthCamera;
	public bool InvertCamera;
	// Use this for initialization
	void Start () {
		camera = transform;
		player = transform.parent.transform;
	}
	
	// Update is called once per frame
	void Update () {
		        if(InvertCamera)
		        {
		        RotY += Input.GetAxis("Mouse Y") * SmouthCamera;
		        }
		        else
		        {
			    RotY += -Input.GetAxis("Mouse Y") * SmouthCamera;
		        }
		        RotY = Mathf.Clamp (RotY, MinimumRotateY, MaximumRotateY);
		        camera.localEulerAngles = new Vector3(RotY, 0, 0);	
		        player.Rotate(0, Input.GetAxis("Mouse X") * SmouthCamera, 0);
		}

	}


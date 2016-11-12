using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float speed = 1;
        Vector3 direction = new Vector3(0, 0, 0);
        if (holding(OVRInput.Button.Up))
        {
            Debug.Log("UP");
        }
        if (holding(OVRInput.Button.Down))
        {
            Debug.Log("DOWN");
        }
        if (holding(OVRInput.Button.Left))
        {
            Debug.Log("LEFT");
        }
        if (holding(OVRInput.Button.Right))
        {
            Debug.Log("RIGHT");
        }
	}

    private bool holding(OVRInput.Button butt) {
        return OVRInput.Get(butt, OVRInput.Controller.Remote) || OVRInput.Get(butt, OVRInput.Controller.Gamepad);
    }
}

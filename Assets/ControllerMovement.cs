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
        float x = 0, z = 0;
        Debug.Log("1");
        if (OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.Gamepad)) {
            Debug.Log("Pressing 1");
        }
        if (holding(OVRInput.Button.Up))
        {
            z += 1;
            Debug.Log("Up!");
        }
        if (holding(OVRInput.Button.Down))
        {
            z -= 1;
        }
        if (holding(OVRInput.Button.Left))
        {
            x -= 1;
        }
        if (holding(OVRInput.Button.Right))
        {
            x += 1;
        }
        getTransform().position += getTransform().forward * z + getTransform().right * x;
	}

    private Transform getTransform() {
        return gameObject.transform;
    }

    private bool holding(OVRInput.Button butt) {
        return OVRInput.Get(butt, OVRInput.Controller.Remote) || OVRInput.Get(butt, OVRInput.Controller.Gamepad);
    }
}

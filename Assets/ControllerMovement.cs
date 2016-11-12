using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMovement : MonoBehaviour {
	// Use this for initialization
	public Transform tr;
	Transform trf;
	public float speed = 1;
	void Start () {
		trf = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
<<<<<<< movement
        float speed = 1;
        Vector3 direction = new Vector3(0, 0, 0);
=======
        
        float x = 0, z = 0;
        Debug.Log("1");
        if (OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.Gamepad)) {
            Debug.Log("Pressing 1");
        }
>>>>>>> local
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
<<<<<<< movement
	}

=======
		Vector3 f = tr.forward * z + tr.right * x;
		f.y = 0;
		trf.position += f * Time.deltaTime * speed;
	}


>>>>>>> local
    private bool holding(OVRInput.Button butt) {
        return OVRInput.Get(butt, OVRInput.Controller.Remote) || OVRInput.Get(butt, OVRInput.Controller.Gamepad);
    }
}

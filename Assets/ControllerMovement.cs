using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMovement : MonoBehaviour {
	// Use this for initialization
	public AudioSource audsrc;
	public Transform tr;
	Transform trf;
	public float speed = 1;
	void Start () {
		trf = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
        
        float x = 0, z = 0;
		if (holding(OVRInput.Button.One)) {
			if (audsrc.isPlaying)
				audsrc.Stop ();
			audsrc.Play ();
        }
        if (holding(OVRInput.Button.Up))
        {
			z++;
        }
        if (holding(OVRInput.Button.Down))
        {
			z--;
        }
        if (holding(OVRInput.Button.Left))
        {
			x--;
        }
        if (holding(OVRInput.Button.Right))
        {
			x++;
    	}


		Vector3 f = tr.forward * z + tr.right * x;
		f.y = 0;
		trf.position += f * Time.deltaTime * speed;
	}


    private bool holding(OVRInput.Button butt) {
        return OVRInput.Get(butt, OVRInput.Controller.Remote) || OVRInput.Get(butt, OVRInput.Controller.Gamepad);
    }
}

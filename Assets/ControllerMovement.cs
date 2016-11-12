using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.Remote)) Debug.Log("RIGHT");
        else Debug.Log("Nope!");
	}
}

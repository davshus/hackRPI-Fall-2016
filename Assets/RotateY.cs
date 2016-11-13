using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateY : MonoBehaviour {
    public float speed = 1;
	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().angularVelocity = new Vector3(0,speed *90,0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

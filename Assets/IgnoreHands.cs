using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreHands : MonoBehaviour {
	public GameObject hands;
	// Use this for initialization
	void Start () {
		foreach (Transform t in hands.transform)
			if (t.GetComponent<Collider>())
				Physics.IgnoreCollision (t.GetComponent<Collider> (), GetComponent<Collider> ()); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}

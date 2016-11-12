using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pseudochild : MonoBehaviour {
	public GameObject pseudoparent;
	Transform tr;
	Vector3 offset;
	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> ();
		offset = tr.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		tr.localPosition = pseudoparent.transform.localPosition + offset;
		tr.localRotation = Quaternion.LookRotation(new Vector3(pseudoparent.transform.forward.x, 0, pseudoparent.transform.forward.z));
	}
}

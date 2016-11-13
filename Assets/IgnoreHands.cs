using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreHands : MonoBehaviour {
	public GameObject hands;
	// Use this for initialization
	void Start () {
        decollide(hands.transform, GetComponent<Collider>());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void decollide(Transform curr, Collider orig) {
        if (curr.GetComponent<Collider>() != null)
        {
            Debug.Log(curr.gameObject.name);
            Physics.IgnoreCollision(curr.GetComponent<Collider>(), GetComponent<Collider>());
        }
        foreach (Transform t in curr)
        {
            decollide(t, orig);
        }
    }

}

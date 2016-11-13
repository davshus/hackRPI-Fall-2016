using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhenToDie : MonoBehaviour {

	void OnCollisionEnter(Collision col)
    {
        Destroy(gameObject);
    }
}

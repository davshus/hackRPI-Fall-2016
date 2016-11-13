using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour {

    private Rigidbody rb;
    public float speed = 1;
    public bool clockwise = true;
    private float realSpeed;
    public bool yRotate=true;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        realSpeed = speed;
        if (!clockwise) realSpeed = -realSpeed;
        Vector3 rot = transform.up;
        if (!yRotate) rot = transform.right;

        rb.AddTorque(rot * realSpeed);
        rb.angularDrag = 0;
    }

    // Update is called once per frame
    void Update ()
    {
    }
}

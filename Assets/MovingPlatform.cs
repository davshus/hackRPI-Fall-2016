using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 speed = new Vector3(1, 1, 1);
    public Vector3 distance = new Vector3(0, 0, 0);
    private Vector3 initial;
    private bool[] increasing = { true, true, true };
    public Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        initial = gameObject.transform.position;
        rb = GetComponent<Rigidbody>();
        for (int i = 0; i < 3; i++)
            if (speed[i] == 0) distance[i] = 0;
            else if (distance[i] == 0) speed[i] = 0;
        rb.velocity = new Vector3(speed.x,speed.y,speed.z);
    }

    void FixedUpdate()
    {
        Vector3 pos = gameObject.transform.position;
        Vector3 v = rb.velocity;
        for (int i = 0; i < 3; i++)
            if (Mathf.Abs(pos[i] - initial[i]) > distance[i]) {
                v[i] = -v[i];
                rb.velocity = v;
            }
    }
}

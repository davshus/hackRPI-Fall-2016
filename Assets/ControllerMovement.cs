using System.Collections;
using System.Collections.Generic;
using Leap.Unity;
using UnityEngine;

public class ControllerMovement : MonoBehaviour {
    // Use this for initialization
    public GameObject marker;
    public float jump = 1;
	public AudioSource audsrc;
	public Transform tr;
	Transform trf;
	public float speed = 1;
    private Rigidbody rb;
    bool ground = true;
    public GameObject hook;
    public float jumpSpeed = 1;

	void Start () {
		trf = GetComponent<Transform> ();
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        GameObject rHand = GameObject.Find("RigidRoundHand_R");
        if (rHand == null || !rHand.activeInHierarchy) {
            Debug.Log("DEAD!");
            marker.SetActive(false);
        }
        else
        {
            RaycastHit hit = new RaycastHit();
            Vector3 palmPos = GameObject.Find("RigidRoundHand_R").GetComponent<RigidHand>().GetPalmPosition();
            Vector3 direc = GameObject.Find("RigidRoundHand_R").GetComponent<RigidHand>().GetArmDirection();
            if (Physics.Raycast(palmPos, direc, out hit, 50.0f))
            {
                if (!marker.activeInHierarchy)
                    marker.SetActive(true);
                marker.transform.position = hit.point;
            }
            else
            {
                marker.SetActive(false);
            }

        }
        float x = 0, z = 0;
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

        if (ground && Mathf.Abs(rb.velocity.y) < 0.1)
        {
            if (holding(OVRInput.Button.One))
            {
                Vector3 f = tr.forward * z;
                f *= speed * jumpSpeed;
                ground = false;
                f.y = jump;
                rb.velocity = f;
            }
            else
            {
                Vector3 f = tr.forward * z + tr.right * x;
                f *= speed;
                f.y = 0;
                trf.position += f * Time.deltaTime;
            }
        }  else if (pressed(OVRInput.Button.One))
        {
            launchGrapple();
        }
    }

    public void launchGrapple() {
        GameObject rHand = GameObject.Find("RigidRoundHand_R");
        if (rHand == null || !rHand.activeInHierarchy) Debug.Log("DEAD!");
        else {
            GameObject obj = Instantiate(hook);
            obj.transform.position=GameObject.Find("RigidRoundHand_R").GetComponent<RigidHand>().GetPalmPosition();
            obj.GetComponent<Rigidbody>().velocity= GameObject.Find("RigidRoundHand_R").GetComponent<RigidHand>().GetArmDirection() * 5;

        }
    }

    private bool holding(OVRInput.Button butt)
    {
        return OVRInput.Get(butt, OVRInput.Controller.Remote) || OVRInput.Get(butt, OVRInput.Controller.Gamepad);
    }
    private bool pressed(OVRInput.Button butt)
    {
        return OVRInput.GetDown(butt, OVRInput.Controller.Remote) || OVRInput.GetDown(butt, OVRInput.Controller.Gamepad);
    }

    void OnCollisionEnter(Collision collision)
    {
        ground = true;
    }
}

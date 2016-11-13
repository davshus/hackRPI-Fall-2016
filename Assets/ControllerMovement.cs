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
    private bool isGrappling = false;
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
            if (Physics.Raycast(palmPos, direc, out hit, 25.0f))
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
        if (rHand == null || !rHand.activeInHierarchy)
        {
            Debug.Log("DEAD!");
            return;
        }
        else
        {
            //GameObject obj = Instantiate(hook);
            RaycastHit hit = new RaycastHit();
            if (!Physics.Raycast(GameObject.Find("RigidRoundHand_R").GetComponent<RigidHand>().GetPalmPosition(), GameObject.Find("RigidRoundHand_R").GetComponent<RigidHand>().GetArmDirection(), out hit, 25.0f)) return;
            if (isGrappling)
            {
                Debug.Log("not grappling");
                if (hit.collider.GetComponent<HingeJoint>() != null) hit.collider.GetComponent<HingeJoint>().connectedBody = null;
                GetComponent<Transform>().rotation = Quaternion.LookRotation(new Vector3(GetComponent<Transform>().forward.x, 0, GetComponent<Transform>().forward.z));
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            } else
            {
                Debug.Log("grappling");
                if (hit.collider.GetComponent<HingeJoint>() == null) hit.collider.gameObject.AddComponent<HingeJoint>();
                hit.collider.GetComponent<HingeJoint>().connectedBody = GetComponent<Rigidbody>();
                hit.collider.GetComponent<HingeJoint>().anchor = hit.collider.GetComponent<Transform>().InverseTransformPoint(hit.point);
                hit.collider.GetComponent<HingeJoint>().axis = Vector3.Cross(GameObject.Find("RigidRoundHand_R").GetComponent<RigidHand>().GetArmDirection(), Vector3.down);
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
            isGrappling = !isGrappling;
            //obj.transform.position = GameObject.Find("RigidRoundHand_R").GetComponent<RigidHand>().GetPalmPosition();
            //obj.GetComponent<Rigidbody>().velocity = GameObject.Find("RigidRoundHand_R").GetComponent<RigidHand>().GetArmDirection() * 5;

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

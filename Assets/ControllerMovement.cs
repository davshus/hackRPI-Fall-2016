using System.Collections;
using System.Collections.Generic;
using Leap.Unity;
using UnityEngine;
using System;

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
    private GameObject grappled = null;
    private int collisions = 0;
    public float grappleTraverseSpeed = 1;
    private LineRenderer lr;
    private RaycastHit rch;
    public int chkpt = 0;
    public const int worldNum = 6;
    public GameObject[] respawns = new GameObject[worldNum];
    private int layerMask;
    void Start () {
		trf = GetComponent<Transform> ();
        rb = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();
        layerMask = 1 << 2;
        layerMask = ~layerMask;
    }
	
	// Update is called once per frame
	void Update () {
        handleControls();
        if (!isGrappling&& InAir()) {
            gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
            //Rigidbody rb = gameObject.GetComponent<Rigidbody>();
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
            if (!Physics.Raycast(GameObject.Find("RigidRoundHand_R").GetComponent<RigidHand>().GetPalmPosition(), GameObject.Find("RigidRoundHand_R").GetComponent<RigidHand>().GetArmDirection(), out hit, 25.0f, layerMask)) return;
            HingeJoint hinge = hit.collider.GetComponent<HingeJoint>();
            if (isGrappling)
            {
                Debug.Log("not grappling");
                Destroy(grappled.GetComponent<HingeJoint>());
                Debug.Log("Destroyed.");
                
                if (hit.collider.gameObject != grappled)
                {
                    Debug.Log("About to launch again.");
                    isGrappling = false;
                    launchGrapple();
                    Debug.Log("Completed second launch.");
                    return;
                }
            } else
            {
                Debug.Log("grappling");
                grappled = hit.collider.gameObject;
                rch = hit;
                Debug.Log("1");
                if (hinge == null) {hinge = hit.collider.gameObject.AddComponent<HingeJoint>();}
                Debug.Log("5");
                GameObject obj = Instantiate(hook);
                obj.GetComponent<Transform>().position = rHand.GetComponent<RigidHand>().GetPalmPosition();
                obj.GetComponent<Rigidbody>().velocity = hit.point - rHand.GetComponent<RigidHand>().GetPalmPosition();
                hinge.connectedBody = GetComponent<Rigidbody>();
                Debug.Log("6");
                hinge.anchor = hit.collider.GetComponent<Transform>().InverseTransformPoint(hit.point);
                Debug.Log("7");
                Vector3 temp = Vector3.Cross(GameObject.Find("RigidRoundHand_R").GetComponent<RigidHand>().GetArmDirection(), Vector3.down);
                hinge.axis = Mathf.Sign(Vector3.Dot(Vector3.Cross(Vector3.Cross(GameObject.Find("RigidRoundHand_R").GetComponent<RigidHand>().GetArmDirection(), Vector3.down), GameObject.Find("RigidRoundHand_R").GetComponent<RigidHand>().GetArmDirection()), temp)) > 0 ? temp : temp * -1;
                Debug.Log("8");
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
        //ground = true;
        collisions++;
/*        if (isGrappling)
        {
            Destroy(grappled.GetComponent<HingeJoint>());
            isGrappling = false;
        }*/
        if (!isGrappling)
        {
            GetComponent<Transform>().rotation = Quaternion.LookRotation(new Vector3(GetComponent<Transform>().forward.x, 0, GetComponent<Transform>().forward.z));
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
        if (collision.collider.gameObject.CompareTag("Death"))
        {
            die();   
        }
        if (collision.collider.name.Contains("Chkpt"))
        {
            Debug.Log(collision.collider.name.Substring(5));
            chkpt = Int32.Parse(collision.collider.name.Substring(5)) > chkpt ? Int32.Parse(collision.collider.name.Substring(5)) : chkpt;
        }

    }
    void die()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Transform>().position = new Vector3(respawns[chkpt].transform.position.x, respawns[chkpt].transform.position.y + respawns[chkpt].transform.lossyScale.y / 2 + 3, respawns[chkpt].transform.position.z);
    }
    void OnCollisionExit(Collision collision)
    {
        collisions--;    }

    private void handleControls() {

        GameObject rHand = GameObject.Find("RigidRoundHand_R");
        if (rHand == null || !rHand.activeInHierarchy)
        {
            Debug.Log("DEAD!");
            marker.SetActive(false);
            if (isGrappling)
            {
                lr.SetPositions(new Vector3[] { GetComponent<Transform>().position, rch.point });
            }
            else
            {
                lr.SetPositions(new Vector3[] { GetComponent<Transform>().position, GetComponent<Transform>().position });
            }
        }
        else
        {
            RaycastHit hit = new RaycastHit();
            Vector3 palmPos = GameObject.Find("RigidRoundHand_R").GetComponent<RigidHand>().GetPalmPosition();
            Vector3 direc = GameObject.Find("RigidRoundHand_R").GetComponent<RigidHand>().GetArmDirection();
            if (Physics.Raycast(palmPos, direc, out hit, 25.0f, layerMask))
            {
                if (!marker.activeInHierarchy)
                    marker.SetActive(true);
                marker.transform.position = hit.point;

            }
            else
            {
                marker.SetActive(false);
            }
            if (isGrappling)
            {
                lr.SetPositions(new Vector3[] { palmPos, rch.point });
            }
            else
            {
                lr.SetPositions(new Vector3[] { palmPos, palmPos });
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
        Debug.Log(x + " " + z);

        if (!InAir())
        {
            if (holding(OVRInput.Button.One))
            {
                Vector3 f = tr.forward * z;
                f *= speed * jumpSpeed;
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
        }
        else if (isGrappling && z == 1)
        {
            Debug.Log("Move Up!");
            Rigidbody connected = grappled.GetComponent<HingeJoint>().connectedBody;
            grappled.GetComponent<HingeJoint>().connectedBody = null;
            gameObject.transform.position += (rch.point - gameObject.transform.position).normalized * grappleTraverseSpeed * .2f;
            grappled.GetComponent<HingeJoint>().connectedBody = connected;
        }
        if (pressed(OVRInput.Button.One) && collisions == 0)
        {
            Debug.Log("Grapple begin!");
            launchGrapple();
        }
    }
    private bool InAir() {
        return collisions == 0 || Mathf.Abs(rb.velocity.y) >= 0.1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public const float SPEEDTORISE = .5f;
    public const float MAXSPEED = 10f;
    public const float ACCELERATIONMAGNITUDE = 4000f;
    public const float FRICTIONALCONSTANT = .5f;

    private Rigidbody rigidbody;
    public string horizontalCtrl = "LeftJoystickHorizontal";
    public string verticalCtrl = "LeftJoystickVertical";
    public string aButton = "AButton";

    public float heightToMaintain = 0f;

    // Use this for initialization
    void Start () {
        // Get rigidbody
        rigidbody = gameObject.GetComponent<Rigidbody>();

        // Set the heightToMaintain
        var hit = new RaycastHit();
        if(heightToMaintain != 0f)
        {
            transform.position = new Vector3(transform.position.x, heightToMaintain, transform.position.z);
        }
        else if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            // Tell distance from ground to maintain
            heightToMaintain = hit.distance + hit.transform.position.y;
        }
    }
	
	// Update is called once per frame
	void Update () {

        // Tell distance from ground to either turn on gravity or rise/fall to preferred height
        var hit = new RaycastHit();
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            rigidbody.useGravity = false;

            var distanceToGround = hit.distance;

            if (transform.position.y < heightToMaintain)
            {
                // Check to see if you need to get up to the distance above the ground
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, SPEEDTORISE, rigidbody.velocity.z);
            }
            else if (transform.position.y > heightToMaintain)
            {
                // Check to see if you need to get down to the distance above the ground
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, -SPEEDTORISE, rigidbody.velocity.z);
            }
        }
        else
        {
            // Turn Gravity on
            rigidbody.useGravity = true;
        }
    }

    private void FixedUpdate()
    {
        // Player controlled movement
        float h = Input.GetAxis(horizontalCtrl);    // Get left analoge stick's horizontal input
        float v = Input.GetAxis(verticalCtrl);      // Get left analoge stick's vertical input
        if (h != 0f || v != 0f)
        {
            Vector3 forceToAdd = (new Vector3(h, 0, -v)).normalized * ACCELERATIONMAGNITUDE;
            if (((forceToAdd/rigidbody.mass * Time.fixedDeltaTime) + rigidbody.velocity).sqrMagnitude >= Mathf.Pow(2, MAXSPEED))
            {
                rigidbody.velocity = ((rigidbody.velocity + (forceToAdd * Time.fixedDeltaTime)).normalized * MAXSPEED);
            }
            else
            {
                rigidbody.AddForce(forceToAdd * Time.fixedDeltaTime);
            }
            Debug.Log("Velocity: " + rigidbody.velocity.magnitude);
            //Debug.Log("Horizontal: " + h + ", Vertical: " + v);
        }
        else
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x * FRICTIONALCONSTANT, rigidbody.velocity.y, rigidbody.velocity.z * FRICTIONALCONSTANT);
        }
        


        //Debug.Log("Horizontal: " + h + ", Vertical: " + v);
        if (Input.GetButtonDown(aButton))
            Debug.Log("A pressed");
    }
}

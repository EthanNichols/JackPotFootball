using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public const float SPEEDTORISE = .5f;
    public const float MAXWALKSPEED = 15f;
    public const float MAXSPRINTSPEED = 20f;
    public const float ACCELERATIONMAGNITUDE = 85f;
    public const float FRICTIONALCONSTANT = .95f;

    public bool tackling = false;
    public bool tackled = false;

    private Rigidbody rigidbody;
    public string horizontalCtrl = "LeftJoystickHorizontal";
    public string verticalCtrl = "LeftJoystickVertical";
    public string aButton = "AButton";

    //public float heightToMaintain = 0f;

    // Use this for initialization
    void Start () {
        // Get rigidbody
        rigidbody = gameObject.GetComponent<Rigidbody>();

        /*
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
        */
    }
	
	// Update is called once per frame
	void Update () {

        Hover();
        
    }

    private void FixedUpdate()
    {
        if (tackled)
        {
            Recover();
            return;
        }

        if (!tackling)
        {
            UpdateMovement();
        }

        Tackle();
        SetDirection();

        //rigidbody.velocity = new Vector3(rigidbody.velocity.x * FRICTIONALCONSTANT, rigidbody.velocity.y, rigidbody.velocity.z * FRICTIONALCONSTANT);
    }

    private void Recover()
    {
        if(rigidbody.velocity == Vector3.zero)
        {
            tackled = false;
        }

        rigidbody.velocity *= .9f;
    }

    private void Tackle()
    {
        if (Input.GetButtonDown(aButton) &&
            !tackling)
        {
            rigidbody.velocity *= 6f;
            tackling = true;
        }

        if (tackling)
        {
            rigidbody.velocity *= .9f;

            if (rigidbody.velocity.magnitude < MAXWALKSPEED)
            {
                tackling = false;
            }
        }
    }

    /// <summary>
    /// Call in FixedUpdate() to update the transform's velocity in the x and z axes
    /// </summary>
    private void UpdateMovement()
    {
        // Player controlled movement
        float h = Input.GetAxis(horizontalCtrl);    // Get left analoge stick's horizontal input
        float v = Input.GetAxis(verticalCtrl);      // Get left analoge stick's vertical input
        if (h != 0f || v != 0f)
        {
            Vector3 leftAnalogInput = new Vector3(h, 0, -v).normalized;
            float magnitude = leftAnalogInput.sqrMagnitude;
            rigidbody.velocity = leftAnalogInput * MAXWALKSPEED;

            //rigidbody.velocity = Vector3.ClampMagnitude((rigidbody.velocity + (forceToAdd * Time.fixedDeltaTime)), MAXWALKSPEED);
            /*
            if (magnitude <= .5625f)
            {
                rigidbody.velocity = Vector3.ClampMagnitude((rigidbody.velocity + (forceToAdd * Time.fixedDeltaTime)), MAXWALKSPEED);
            }
            else
            {
                rigidbody.velocity = Vector3.ClampMagnitude((rigidbody.velocity + (forceToAdd * Time.fixedDeltaTime)), MAXSPRINTSPEED);
            }
            */
        } else
        {
            rigidbody.velocity = Vector3.zero;
        }
    }

    private void SetDirection()
    {
        float h = Input.GetAxis(horizontalCtrl);    // Get left analoge stick's horizontal input
        float v = Input.GetAxis(verticalCtrl);      // Get left analoge stick's vertical input
        if (h != 0f || v != 0f)
        {
            Vector3 leftAnalogInput = new Vector3(h, 0, -v).normalized;

            rigidbody.velocity = leftAnalogInput * rigidbody.velocity.magnitude;
        }
    }

    /// <summary>
    /// Call in Update(). Raycasts down and turns on gravity when it doesn't hit anything. Will move to the distanceToGround
    /// if raycasts down and hits something.
    /// </summary>
    private void Hover()
    {
        // Tell distance from ground to either turn on gravity or rise/fall to preferred height
        var hit = new RaycastHit();
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            //rigidbody.useGravity = false;

            var distanceToGround = hit.distance;
            /*
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
            */
        }
        else
        {
            // Turn Gravity on
            rigidbody.useGravity = true;
        }
    }
}

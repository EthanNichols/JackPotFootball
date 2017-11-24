using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    //Game manager object
    private bool inBounds;
    private GameObject manager;

    // Use this for initialization
    void Start () {

        //Find the game manager
        manager = GameObject.FindGameObjectWithTag("Manager");

        CalcInBound();
    }
	
	// Update is called once per frame
	void Update () {
        //Keep the ball in the arena
        if (inBounds)
        {
            KeepInBounds();
        } else
        {
            CalcInBound();
        }
	}

    private void CalcInBound()
    {
        //Get the arena
        Manager managerScript = manager.GetComponent<Manager>();

        if (Vector3.Magnitude(transform.position) < managerScript.mapSize * .5f)
        {
            inBounds = true;
        }
    }

    /// <summary>
    /// Keep the ball inside of the arena
    /// </summary>
    private void KeepInBounds()
    {
        //Get the arena
        Manager managerScript = manager.GetComponent<Manager>();

        //Get the position of the ball, removing the y value
        Vector3 pos = transform.position;
        pos.y = 0;

        //Test if the ball would go off the area
        if (Vector3.Magnitude(pos) > managerScript.mapSize * .5f)
        {
            //Slightly adjust the position to keep the ball in bounds
            pos = pos.normalized * ((managerScript.mapSize - .1f ) * .5f);
            transform.position = new Vector3(pos.x, transform.position.y, pos.z);

            //Calculate the tangent for where the ball it on the arena
            Vector3 tanget = new Vector3(pos.z, 0, pos.x * -1).normalized;

            Rigidbody body = GetComponent<Rigidbody>();
            Vector3 direction = body.velocity.normalized;

            //Calculate and set the new direction for the ball to move

            Vector3 newDir = (direction - (2 * (Vector3.Dot(direction, tanget) * tanget))).normalized * -body.velocity.magnitude;
            newDir.y = direction.y;

            body.velocity = newDir;
        }
    }
}

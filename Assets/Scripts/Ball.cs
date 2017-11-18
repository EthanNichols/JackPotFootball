using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    //The direction and speed of the ball
    public Vector3 direction;
    public int speed;

    //Game manager object
    private GameObject manager;

    // Use this for initialization
    void Start () {

        //Normalize the direction and find the game manager
        direction = direction.normalized;
        manager = GameObject.FindGameObjectWithTag("Manager");
    }
	
	// Update is called once per frame
	void Update () {

        //Move the object
        transform.position += direction * Time.deltaTime * speed;

        //Keep the ball in the arena
        KeepInBounds();
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

            //Calculate and set the new direction for the ball to move
            direction = (direction - (2 * (Vector3.Dot(direction, tanget) * tanget))).normalized * -1;
        }
    }
}

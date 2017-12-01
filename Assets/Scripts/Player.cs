using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //The amount of time before the player respawns
    public float respawnTime;
    public float points;

    //Game manager, and whether the player is dead or not
    private GameObject manager;
    private bool dead;

    private GameObject holdingBall;

	// Use this for initialization
	void Start () {

        holdingBall = null;

        points = 0;
        Respawn();
        manager = GameObject.FindGameObjectWithTag("Manager");
	}
	
	// Update is called once per frame
	void Update () {

        FellOffStage();

        ScorePoints();
	}

    /// <summary>
    /// handles the player scoring points
    /// </summary>
    private void ScorePoints()
    {
        //Get the game manager
        Manager managerScript = manager.GetComponent<Manager>();

        //Get the position of the player without the y component
        Vector3 localPos = transform.position;
        localPos.y = 0;

        //Test if the player is within the scoring area
        if (Vector3.Distance(localPos, Vector3.zero) < managerScript.scoreArea &&
            holdingBall)
        {
            //Increase the amount of points the player has
            points += holdingBall.GetComponent<Ball>().ballValue;

            //Destroy the ball and make it so the player isn't holding a ball
            Destroy(holdingBall);
            holdingBall = null;
        }
    }

    /// <summary>
    /// Respawn the player onto the platform and set that the player isn't dead
    /// </summary>
    private void Respawn()
    {
        //Spawn the player at a random position on the arena
        float direction = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        transform.position = new Vector3(Mathf.Cos(direction), 0, Mathf.Sin(direction)) * 10;
        transform.position += new Vector3(0, transform.localScale.y * .5f, 0);

        //Set that the player isn't dead
        dead = false;
    }

    /// <summary>
    /// kills the player if the player exits the arena and respawns the player
    /// </summary>
    private void FellOffStage()
    {
        //Get the arena
        Manager managerScript = manager.GetComponent<Manager>();

        //determine if the player 
        if (Vector3.Distance(transform.position, Vector3.zero) > managerScript.deathDistance / 2 &&
            !dead)
        {
            Debug.Log("Fell");
            //Kill the player, call the respawn timer after a certain amount of time
            dead = true;

            //Destory the ball the player is holding
            if (holdingBall) { Destroy(holdingBall); }
            holdingBall = null;

            Invoke("Respawn", respawnTime);
        }
    }

    /// <summary>
    /// checks to see if the player has collided with a ball and needs to pick it up
    /// </summary>
    /// <param name="col"></param>
    private void OnCollisionEnter(Collision col)
    {
        //Test if the player collided with a ball, and the player isn't holding a ball
        if (col.gameObject.tag == "Ball" &&
            !holdingBall)
        {
            //Make the player pickup the ball, and make the ball invisible
            holdingBall = col.gameObject;
            col.gameObject.SetActive(false);
        }

        if (col.gameObject.tag == "Player")
        {
            if (!GetComponent<PlayerMovement>().tackling) { return; }

            col.gameObject.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity * 3;
            col.gameObject.GetComponent<PlayerMovement>().tackled = true;

            if (col.gameObject.GetComponent<Player>().holdingBall)
            {
                GameObject ball = col.gameObject.GetComponent<Player>().holdingBall;
                ball.transform.position = col.transform.position;
                ball.transform.position += new Vector3(0, 2, 0);
                ball.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1, 1), 5, Random.Range(-1, 1));
                ball.SetActive(true);

                col.gameObject.GetComponent<Player>().holdingBall = null;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //The amount of time before the player respawns
    public float respawnTime;

    //Game manager, and whether the player is dead or not
    private GameObject manager;
    private bool dead;

	// Use this for initialization
	void Start () {
        Respawn();
        manager = GameObject.FindGameObjectWithTag("Manager");
	}
	
	// Update is called once per frame
	void Update () {
        if (!dead)
        {
            transform.position += Vector3.forward;
        }

        FellOffStage();
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

    private void FellOffStage()
    {
        //Get the arena
        Manager managerScript = manager.GetComponent<Manager>();

        //determine if the player 
        if (Vector3.Distance(transform.position, Vector3.zero) > managerScript.deathDistance &&
            !dead)
        {
            //Kill the player, call the respawn timer after a certain amount of time
            dead = true;
            Invoke("Respawn", respawnTime);
        }
    }
}

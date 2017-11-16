using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float respawnTime;

    private GameObject manager;
    private bool dead;

	// Use this for initialization
	void Start () {
        Respawn();
        manager = GameObject.FindGameObjectWithTag("manager");
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
        float direction = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        transform.position = new Vector3(Mathf.Cos(direction), 0, Mathf.Sin(direction)) * 10;
        transform.position += new Vector3(0, transform.localScale.y * .5f, 0);

        dead = false;
    }

    private void FellOffStage()
    {
        GameObject arena = manager.GetComponent<Manager>().arena;
        if (transform.position.y < arena.GetComponent<CreateArena>().deathLayerPosition)
        {
            dead = true;
            Invoke("Respawn", respawnTime);
        }
    }
}

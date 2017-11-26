using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Manager : MonoBehaviour {

    public float mapSize;
    public float deathDistance;

    public float shotTimer;
    private float resetShotTimer;

    //List of players
    public List<GameObject> players;
    private List<GameObject> launchers;

	// Use this for initialization
	void Start () {
        SetupArena();

        //Calculate the distance from the center for the player to die
        deathDistance *= mapSize;

        resetShotTimer = shotTimer;
        launchers = GameObject.FindGameObjectsWithTag("Launcher").ToList();
	}
	
	// Update is called once per frame
	void Update () {
        NewBall();
	}

    private void NewBall()
    {
        if (shotTimer > 0) { shotTimer -= Time.deltaTime; }
        else
        {
            shotTimer = resetShotTimer;
            launchers[Random.Range(0, launchers.Count())].GetComponent<Launcher>().Launch();
        }
    }

    private void SetupArena()
    {
        transform.position = new Vector3(0, -.5f, 0);

        //Set the size of the map, as long as it isn't 0
        if (mapSize != 0)
        {
            transform.localScale = new Vector3(mapSize, 1, mapSize);
        } else
        {
            mapSize = transform.localScale.x;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.x);
        }
    }
}

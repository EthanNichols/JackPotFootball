using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public float mapSize;
    public float deathDistance;

    //List of players
    public List<GameObject> players;

	// Use this for initialization
	void Start () {
        SetupArena();

        //Calculate the distance from the center for the player to die
        deathDistance *= mapSize;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void SetupArena()
    {
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

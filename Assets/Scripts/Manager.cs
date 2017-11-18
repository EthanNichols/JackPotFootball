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

        deathDistance *= mapSize;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void SetupArena()
    {
        if (mapSize != 0)
        {
            transform.localScale = new Vector3(mapSize, 1, mapSize);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    //Size of the map and the distance from the map that a player is killed
    public float mapSize;
    public float deathDistance;

    public float scoreArea;

    //The timer it takes for a cannon to shoot
    public float shotTimer;
    private float resetShotTimer;

    //The delay between shots
    public float shotDelay;
    private float resetDelay;

    //The amount of balls allowed at once
    public int maxBalls;

    //Player Prefabs
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    //List of players
    public List<GameObject> players;
    public GameObject scoreUI;

    //The arena, launchers, and point spinner
    private GameObject arena;
    private List<GameObject> launchers;
    private GameObject spinner;

    // Use this for initialization
    void Start()
    {
        //Find the arena and set the arena up
        arena = GameObject.FindGameObjectWithTag("Arena");
        SetupArena();

        //Calculate the distance from the center for the player to die
        deathDistance *= mapSize;

        //Set the reset timers and the amount of balls to 0
        resetShotTimer = shotTimer;
        resetDelay = shotDelay;

        //Find all the launchers and the point spinner
        launchers = GameObject.FindGameObjectsWithTag("Launcher").ToList();
        spinner = GameObject.FindGameObjectWithTag("Spinner");

        GameObject player;
        //Spawn in players
        switch (Settings.PlayerNum)
        {
            case 4:
                player = Instantiate(player4, new Vector3(Random.Range(-10, 10), 1.5f, Random.Range(-10, 10)), Quaternion.identity) as GameObject;
                players.Add(player);
                goto case 3;
            case 3:
                player = Instantiate(player3, new Vector3(Random.Range(-10, 10), 1.5f, Random.Range(-10, 10)), Quaternion.identity) as GameObject;
                players.Add(player);
                goto case 2;
            case 2:
                player = Instantiate(player2, new Vector3(Random.Range(-10, 10), 1.5f, Random.Range(-10, 10)), Quaternion.identity) as GameObject;
                players.Add(player);
                goto default;
            default:
                player = Instantiate(player1, new Vector3(Random.Range(-10, 10), 1.5f, Random.Range(-10, 10)), Quaternion.identity) as GameObject;
                players.Add(player);
                break;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        //Test if a new ball is shot
        NewBall();
        UpdateScore();
    }

    /// <summary>
    /// updates the score in the UI
    /// </summary>
    private void UpdateScore()
    {
        for (int i=0; i<players.Count(); i++)
        {
            scoreUI.GetComponent<ScoreManager>().SetScore(i, (int)players[i].GetComponent<Player>().points);
        }
    }

    /// <summary>
    /// handles the launching of a new ball
    /// </summary>
    private void NewBall()
    {
        //Test if a ball is going to be shot
        if (shotTimer > 0 &&
            shotDelay < 0 &&
            GameObject.FindGameObjectsWithTag("Ball").Count() < maxBalls)
        {
            //Start the timer
            shotTimer -= Time.deltaTime;

            //Display the spinner background
            Color fade = spinner.GetComponent<RawImage>().color;
            fade.a = 1;
            spinner.GetComponent<RawImage>().color = fade;

            //Display the spinner text
            fade = spinner.transform.GetChild(0).GetComponent<Text>().color;
            fade.a = 1;
            spinner.transform.GetChild(0).GetComponent<Text>().color = fade;

            //Set a random value to the string
            spinner.transform.GetChild(0).GetComponent<Text>().text = Random.Range(1, 10).ToString();
            return;
        }

        //Test if there is a delay before the next shot
        if (shotDelay > 0)
        {
            //Start the delay timer
            shotDelay -= Time.deltaTime;

            //Fade out the spinner background
            Color fade = spinner.GetComponent<RawImage>().color;
            fade.a = shotDelay / resetDelay;
            spinner.GetComponent<RawImage>().color = fade;

            //Fade out the spinner text
            fade = spinner.transform.GetChild(0).GetComponent<Text>().color;
            fade.a = shotDelay / resetDelay;
            spinner.transform.GetChild(0).GetComponent<Text>().color = fade;

            return;
        }

        //Test if a ball can be shot
        if (GameObject.FindGameObjectsWithTag("Ball").Count() < maxBalls)
        {
            //Reset the delay and shot timers
            shotTimer = resetShotTimer;
            shotDelay = resetDelay;

            //Assign a random value for the ball, and increase the amount of balls shot
            int value = Random.Range(1, 10);

            //Display the value of the ball
            //Shoot the ball from one of the launchers
            spinner.transform.GetChild(0).GetComponent<Text>().text = value.ToString();
            launchers[Random.Range(0, launchers.Count())].GetComponent<Launcher>().Launch(value);
        }
    }

    /// <summary>
    /// sets the arena's position and size
    /// </summary>
    private void SetupArena()
    {
        //Set the top of the arena to be at 0
        arena.transform.position = new Vector3(0, -.5f, 0);

        //Set the size of the map, as long as it isn't 0
        if (mapSize != 0)
        {
            arena.transform.localScale = new Vector3(mapSize, 1, mapSize);
        }
        else
        {
            //Set the size of the map relative to the default size
            mapSize = transform.localScale.x;
            arena.transform.localScale = new Vector3(mapSize, 1, mapSize);
        }
    }
}

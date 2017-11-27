using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour {

    //The ball that will be shot
    public GameObject projectile;

    //The angle the ball is shot, and the location the ball is going to
    private float desired_angle;
    private Vector3 desiredLoc = Vector3.zero;

    private GameObject manager;

    // Use this for initialization
    void Start () {
        //Find and set the manager
        manager = GameObject.FindGameObjectWithTag("Manager");
	}
	
	// Update is called once per frame
	void Update () {
    }

    public void Launch(int value)
    {
        //Set a random value for the angle the ball is shot
        desired_angle = Random.Range(10f, 80f);

        //Determine an angle and distance in a circle
        float rotation = Random.Range(0, 360) * Mathf.Deg2Rad;
        float distance = Random.Range(0, manager.GetComponent<Manager>().mapSize *.5f);

        //Set the desired location for the ball
        desiredLoc = new Vector3(Mathf.Cos(rotation) * distance, 0, Mathf.Sin(rotation) * distance);//pick the possible locations based on the picked launcher

        //Shoot the ball from the launcher
        GameObject newProjectile = Instantiate(projectile, gameObject.transform.position, Quaternion.identity);

        //Set the value of the ball, and set the velocity of the ball
        newProjectile.GetComponent<Ball>().ballValue = value;
        newProjectile.GetComponent<Rigidbody>().velocity = CalculateVelocity();
    }

    private Vector3 CalculateVelocity()
    {
        //Get the distance from the ball to the desired location
        Vector3 dir = desiredLoc - gameObject.transform.position;

        float height = dir.y;
        dir.y = 0;
        float distance = dir.magnitude;
        float angle_rad = desired_angle * Mathf.Deg2Rad;

        dir.y = distance * Mathf.Tan(angle_rad);
        distance += height / Mathf.Tan(angle_rad);//small corrections


        float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * angle_rad)) * Random.Range(1, 3);
        return velocity * dir.normalized;
    }
}

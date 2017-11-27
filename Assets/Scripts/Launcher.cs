using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour {

    public GameObject projectile;

    private float desired_angle;
    private Vector3 desiredLoc = Vector3.zero;
    private GameObject manager;

    // Use this for initialization
    void Start () {
        manager = GameObject.FindGameObjectWithTag("Manager");
	}
	
	// Update is called once per frame
	void Update () {
    }

    public void Launch()
    {
        desired_angle = Random.Range(10f, 80f);
        float rotation = Random.Range(0, 360) * Mathf.Deg2Rad;
        float distance = Random.Range(0, manager.GetComponent<Manager>().mapSize *.5f);

        desiredLoc = new Vector3(Mathf.Cos(rotation) * distance, 0, Mathf.Sin(rotation) * distance);//pick the possible locations based on the picked launcher

        GameObject newProjectile = Instantiate(projectile, gameObject.transform.position, Quaternion.identity);

        newProjectile.GetComponent<Rigidbody>().velocity = CalculateVelocity();
    }

    private Vector3 CalculateVelocity()
    {
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

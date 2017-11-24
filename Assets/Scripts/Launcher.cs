using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour {

    public GameObject projectile;
    public float desired_angle;

    private Vector3 desiredLoc = Vector3.zero;
    private bool allowedLaunch = true;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (allowedLaunch)
        {
            desired_angle = Random.Range(10, 30);
            Launch(projectile, desired_angle);
        }
    }

    private void Launch(GameObject whatToLaunch, float angle)
    {
        allowedLaunch = false;

        desiredLoc = new Vector3(Random.Range(-25f, 25f), 7f, Random.Range(-24f, 24f));//pick the possible locations based on the picked launcher

        GameObject newProjectile = Instantiate(whatToLaunch, gameObject.transform.position, Quaternion.identity);

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


        float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * angle_rad)) * 3;
        return velocity * dir.normalized;
    }
}

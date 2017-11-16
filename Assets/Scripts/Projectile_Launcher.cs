using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Launcher : MonoBehaviour {

    public GameObject[] possible_start_locations;
    public GameObject projectile;
    public float desired_angle;//angle at which it should launch


    private Vector3 desiredLoc = Vector3.zero;
    private bool allowedLaunch = true;
    private int selectedLauncher = -1;//where the starting position of the projectile will be

    void FixedUpdate()
    {
        //the script requires a callout that would tell it when it should launch the projectile
        if (allowedLaunch)
        {
            DetermineLauncher(possible_start_locations);
            Launch(projectile, selectedLauncher, desired_angle);
        }
    }

    private void DetermineLauncher(GameObject[] locations)
    {
        selectedLauncher = Random.Range(0, locations.Length - 1);
    }

    private void Launch(GameObject whatToLaunch, int launcher, float angle)
    {
        allowedLaunch = false;

        desiredLoc = new Vector3(Random.Range(-25f, 25f), 7f, Random.Range(-24f, 24f));//pick the possible locations based on the picked launcher

        GameObject newProjectile = Instantiate(whatToLaunch, possible_start_locations[launcher].transform.position, Quaternion.identity);

        newProjectile.GetComponent<Rigidbody>().velocity = CalculateVelocity(launcher);
    }


    private Vector3 CalculateVelocity(int launcher)
    {
        Vector3 dir = desiredLoc - possible_start_locations[launcher].transform.position;
        float height = dir.y;
        dir.y = 0;
        float distance = dir.magnitude;
        float angle_rad = desired_angle * Mathf.Deg2Rad;

        dir.y = distance * Mathf.Tan(angle_rad);
        distance += height / Mathf.Tan(angle_rad);//small corrections


        float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * angle_rad));
        return velocity * dir.normalized;
    }
}

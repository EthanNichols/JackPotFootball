using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBallCollision : MonoBehaviour {
    public bool isHolding;
    public Vector3 ballLocation;
    public float grabDistance;
    public GameObject ball;
	// Use this for initialization
	void Start ()
    {
        isHolding = false;
	}
	
	// Update is called once per frame
	void Update ()

    {
        //only checks if isHolding is equal to false
		if(isHolding == false)
        {
            //checks if the distance magnitude is close enough
            if((transform.position - ball.transform.position).magnitude < grabDistance)
            {
                isHolding = true;
                Destroy(ball);
            }

        }
	}
}

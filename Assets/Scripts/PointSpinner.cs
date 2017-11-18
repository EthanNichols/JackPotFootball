using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointSpinner : MonoBehaviour {

    //The max points that can be earned, and the current value
    public int maxValue;
    private int value;

    //The amount of time the spiiner runs for
    public float spinTimer;

    //The text display for the point value
    private GameObject display;

	// Use this for initialization
	void Start () {
        //Set the text display object
        display = transform.GetChild(0).gameObject;
	}

    // Update is called once per frame
    void Update()
    {
        //Test if the spinner is still spinning
        if (spinTimer > 0)
        {
            //Get a random value for the ball
            int setValue = Random.Range(1, maxValue);

            //Set the value of the spinner, and display the value
            display.GetComponent<Text>().text = setValue.ToString();
            value = setValue;

            //Reduve the amount of time the spinner has left
            spinTimer -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Return the value of the spinner for the ball
    /// </summary>
    /// <returns>The value of the spinner for the ball</returns>
    public int GetValue()
    {
        return value;
    }
}

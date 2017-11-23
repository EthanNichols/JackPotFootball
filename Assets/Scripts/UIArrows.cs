using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIArrows : MonoBehaviour {
    public int count;
    public int buttonCount;
    public int numOfButtons;
    public int maxCount;
    public bool up;
    public int numberOfPlayers;
    public Text players;
    public Text map;
    public int playerMax;
    public bool mapOne;
    public List<Button> buttons;
    public ColorBlock colorblock;
    // Use this for initialization
    void Start ()
    {
        buttonCount = 1;
        numberOfPlayers = 1;
        mapOne = true;
        playerMax = 4;
    }
	
	// Update is called once per frame
	void Update ()
    {
        players.text = numberOfPlayers.ToString();
        if (mapOne == true)
        {
            map.text = "Map One";
        }
        else if(mapOne == false)
        {
            map.text = "Map Two";
        }
  
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (buttonCount >= numOfButtons)
            {
                buttonCount = 1;

            }
            else
            {
                buttonCount++;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (buttonCount <= 0)
            {

                buttonCount = numOfButtons;
            }

            else
            {
               
                buttonCount--;
              
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(buttonCount == 2)
            {
                if (numberOfPlayers < playerMax)
                {
                    numberOfPlayers++;
                }
                else
                {
                    numberOfPlayers = 1;
                }
               
            }
            else if(buttonCount == 1)
            {
                if (numberOfPlayers > 1)
                {
                    numberOfPlayers--;
                }
                else
                {
                    numberOfPlayers = 4;
                }
                
            }

            else if(buttonCount == 3)
            {
                mapOne = !mapOne;
            }
            else if (buttonCount == 4)
            {
                mapOne = !mapOne;
            }
            else if (buttonCount == 5)
            {

            }

        }
    }
   
}

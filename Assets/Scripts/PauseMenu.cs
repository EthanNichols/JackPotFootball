using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour {
    public GameObject canvas;
    public GameObject eventsys;
    public GameObject menu_member;
    private bool menuEnabler = false;

	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Pause"))//Enable the pause menu
        {
            Debug.Log("PressedPause");
            menuEnabler = !menuEnabler;
            canvas.SetActive(menuEnabler);
            if (menuEnabler)
            {
                eventsys.GetComponent<EventSystem>().SetSelectedGameObject(null);
                menu_member.SetActive(true);
                eventsys.GetComponent<EventSystem>().SetSelectedGameObject(menu_member);//select the first member in the pause menu
                Time.timeScale = 0f;//shut down game progress
            }
            else
            {
                Time.timeScale = 1f;//set time to the oiginal rate
            }

        }
        else if (Input.GetButtonDown("Cancel"))//When anyone presses B, return to normal game progression
        {
            menuEnabler = false;
            canvas.SetActive(menuEnabler);
            Time.timeScale = 1f;
        }
	}
}

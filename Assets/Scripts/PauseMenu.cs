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
        if (Input.GetButtonDown("Pause"))
        {
            Debug.Log("PressedPause");
            menuEnabler = !menuEnabler;
            canvas.SetActive(menuEnabler);
            if (menuEnabler)
            {
                eventsys.GetComponent<EventSystem>().SetSelectedGameObject(null);
                menu_member.SetActive(true);
                eventsys.GetComponent<EventSystem>().SetSelectedGameObject(menu_member);
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }

        }
        else if (Input.GetButtonDown("Cancel"))
        {
            menuEnabler = false;
            canvas.SetActive(menuEnabler);
            Time.timeScale = 1f;
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour {

    private PauseMenu menuRef;
    /// <summary>
    /// resumes the game
    /// </summary>
	public void Resume()
    {
        menuRef = FindObjectOfType<PauseMenu>();
        menuRef.canvas.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Options()
    {
        //make a reference to the options scene/ui here
    }
    public void Quit()
    {
        //make a reference to the scene to load here
    }
}

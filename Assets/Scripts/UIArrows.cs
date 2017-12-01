using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIArrows : MonoBehaviour {
    private const int MAXPLAYERS = 4;

    public int numberOfPlayers;
    public int levelIndex;
    public Text playerCountText;
    public Text levelText;
    public List<string> levels;

    private void Start()
    {
        numberOfPlayers = 1;
        levelIndex = 0;
        playerCountText.text = numberOfPlayers.ToString();

        levels = new List<string>();
        levels.Add("Game");
        levels.Add("MovementScene");
        levels.Add("SpawnPlayers");
        levelText.text = levels[levelIndex];
    }

    /// <summary>
    /// increases the number of players
    /// </summary>
    public void IncreasePlayerCount()
    {
        numberOfPlayers++;
        if(numberOfPlayers > MAXPLAYERS)
        {
            numberOfPlayers = 1;
        }
        playerCountText.text = numberOfPlayers.ToString();
    }

    /// <summary>
    /// decreases the number of players
    /// </summary>
    public void DecreasePlayerCount()
    {
        numberOfPlayers--;
        if(numberOfPlayers < 1)
        {
            numberOfPlayers = MAXPLAYERS;
        }
        playerCountText.text = numberOfPlayers.ToString();
    }

    /// <summary>
    /// changes the selected scene by increasing the scene index
    /// </summary>
    public void IncreaseSceneIndex()
    {
        levelIndex++;
        if (levelIndex >= levels.Count)
        {
            levelIndex = 0;
        }
        levelText.text = levels[levelIndex];
    }

    /// <summary>
    /// changes the selected scene by decreasing the scene index
    /// </summary>
    public void DecreaseSceneIndex()
    {
        levelIndex--;
        if (levelIndex < 0)
        {
            levelIndex = levels.Count - 1;
        }
        levelText.text = levelText.text = levels[levelIndex];
    }

    /// <summary>
    /// starts the selected scene with the selected number of players
    /// </summary>
    public void StartGamePressed()
    {
        Settings.PlayerNum = numberOfPlayers;
        SceneManager.LoadScene(levels[levelIndex]);
    }
}

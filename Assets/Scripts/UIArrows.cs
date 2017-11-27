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
        levelText.text = levels[levelIndex];
    }

    public void IncreasePlayerCount()
    {
        numberOfPlayers++;
        if(numberOfPlayers > MAXPLAYERS)
        {
            numberOfPlayers = 1;
        }
        playerCountText.text = numberOfPlayers.ToString();
    }

    public void DecreasePlayerCount()
    {
        numberOfPlayers--;
        if(numberOfPlayers < 1)
        {
            numberOfPlayers = MAXPLAYERS;
        }
        playerCountText.text = numberOfPlayers.ToString();
    }

    public void IncreaseSceneIndex()
    {
        levelIndex++;
        if (levelIndex >= levels.Count)
        {
            levelIndex = 0;
        }
        levelText.text = levels[levelIndex];
    }

    public void DecreaseSceneIndex()
    {
        levelIndex--;
        if (levelIndex < 0)
        {
            levelIndex = levels.Count - 1;
        }
        levelText.text = levelText.text = levels[levelIndex];
    }

    public void StartGamePressed()
    {
        SceneManager.LoadScene(levels[levelIndex]);
    }
}

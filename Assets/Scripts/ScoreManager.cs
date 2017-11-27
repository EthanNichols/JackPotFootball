using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour{
    public int playerNum;
    private int[] playerScores;
    private Text[] playerTexts;
    public GameObject textPrefab;  // Prefab for the text element that will be used to keep score
	void Start()
    {
        playerScores = new int[playerNum];
        playerTexts = new Text[playerNum];

        for (int i = 1; i <= playerNum; i++)
        {
            GameObject g = Instantiate(textPrefab);

            g.transform.SetParent(gameObject.transform);

            Text text = g.GetComponent<Text>();
            playerTexts[i - 1] = text;
            playerScores[i - 1] = 0;
            text.text = TextString(i, 0);
        }
    }

    /// <summary>
    /// Public method to be called by other scripts to update the score of a given player
    /// </summary>
    public void UpdateScore(int player, int points)
    {
        playerScores[player - 1] += points;
        playerTexts[player - 1].text = TextString(player, playerScores[player - 1]);
    }

    /// <summary>
    /// Returns the string to be displayed for a given player
    /// </summary>
    private string TextString(int number, int score)
    {
        return "Player " + number + ": " + score;
    }


}

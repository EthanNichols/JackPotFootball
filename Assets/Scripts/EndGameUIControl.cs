using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameUIControl : MonoBehaviour {

    public GameObject[] winnerStuff;
    public RectTransform[] playerLocations;   
    public bool gameHasEnded = false;
    public bool animationsUndergo = false;

    private Queue<int> whoGoesWhen = new Queue<int>();
    private int switched_local_copy = -1;
    private bool allowed = true;
    private Animator animController;
    private List<int> playerBallCounts = new List<int>();//Key is the player's number, while the value is his turn
    // Use this for initialization
    private void Start () {
        animController = gameObject.GetComponent<Animator>();
        playerBallCounts.Add(1);
        playerBallCounts.Add(3);
        playerBallCounts.Add(3);
        playerBallCounts.Add(5);
    }
	private void Update () {
        if (Input.GetKeyDown(KeyCode.G))
            gameHasEnded = true;

        if (gameHasEnded)
        {
            animController.SetBool("game_ended", true);
            OrderAnimation();
            gameHasEnded = !gameHasEnded;
            animationsUndergo = true;
            animController.Play("Nothing");
        }
        
        if (animationsUndergo)
        {
            bool canTransition = false;
            int localswiched = animController.GetInteger("switched");
            if (localswiched == 0 && allowed)
            {
                canTransition = true;
                allowed = false;
                switched_local_copy = localswiched;
            }
            else if(switched_local_copy < localswiched)
            {
                canTransition = true;
                switched_local_copy = localswiched;
            }
            //make sure to create a bool that gets set to true once peek gives null
            if (canTransition)
            {
                animController.SetInteger("player1Goes", -1);
                animController.SetInteger("player2Goes", -1);
                animController.SetInteger("player3Goes", -1);
                animController.SetInteger("player4Goes", -1);
                int outcome = whoGoesWhen.Peek();
                RectTransform winnerSpot = null;
                switch (outcome)
                {
                    case 1:
                        animController.SetInteger("player1Goes", 1);
                        break;
                    case 2:
                        animController.SetInteger("player2Goes", 1);
                        break;
                    case 3:
                        animController.SetInteger("player3Goes", 1);
                        break;
                    case 4:
                        animController.SetInteger("player4Goes", 1);
                        break;
                    default:
                        Debug.Log("smth is wrong with the queue" + outcome);
                        break;
                }
                if (whoGoesWhen.Count == 1)
                    winnerSpot = playerLocations[playerLocations.Length-1];
                whoGoesWhen.Dequeue();
                if (whoGoesWhen.Count == 0)
                {
                    animController.SetBool("everyoneAppeared", true);
                    animationsUndergo = false;
                    winnerStuff[0].GetComponent<RectTransform>().position = winnerSpot.position;
                    winnerStuff[1].GetComponent<RectTransform>().position = new Vector3(winnerSpot.position.x, winnerSpot.position.y + 500f, winnerSpot.position.z);
                }
            }
            
        }

	}
    private void OrderAnimation()
    {      
          int highestScore = -1;
          for (int j = 0; j < playerBallCounts.Count; j++)
          {
              if (playerBallCounts[j] > highestScore)
                  highestScore = playerBallCounts[j];
          }

          for (int i = 0; i < 4; i++)
          {
            if (playerBallCounts[i] == highestScore)
                continue;
            else
                whoGoesWhen.Enqueue(i+1);
          }

        whoGoesWhen.Enqueue(playerBallCounts.IndexOf(highestScore)+1);
    }
       
  
}

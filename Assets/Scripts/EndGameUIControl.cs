using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameUIControl : MonoBehaviour {

    public GameObject[] winnerStuff;//the crown and the golden background
    public RectTransform[] playerLocations;//used locations for finding out where the crown and yellow background should be at the time the winner shows up
    public bool gameHasEnded = false;
    public bool animationsUndergo = false;

    private Queue<int> whoGoesWhen = new Queue<int>();//Used for determining who shows on screen first to last
    private int switched_local_copy = -1;
    private bool allowed = true;
    private Animator animController;
    public List<int> playerBallCounts = new List<int>();//Key is the player's number, while the value is his turn
    // Use this for initialization
    private void Start () {
        animController = gameObject.GetComponent<Animator>();

        playerBallCounts.Add(1);

        playerBallCounts.Add(7);
        playerBallCounts.Add(6);
        playerBallCounts.Add(5);


    }
    private void Update () {

        //do this once the game has ended, adjust the bool accordingly, since it's a public variable
        if (gameHasEnded)
        {
            animController.SetBool("game_ended", true);
            OrderAnimation();//this puts the player in a queue according to how many points they've earned
            gameHasEnded = !gameHasEnded;
            animationsUndergo = true;
            animController.Play("Nothing");//initialize the animations
        }
        
        //Start this once the animations are allowed to play
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
            //Animation Controller
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
                //exception for the first player to show up
                if (whoGoesWhen.Count == 1)
                {
                    int playerNum = whoGoesWhen.Peek()-1;
                    winnerSpot = playerLocations[playerNum];
                }                   
                whoGoesWhen.Dequeue();
                //Once everyone showed up it's time to crown the champion
                if (whoGoesWhen.Count == 0)
                {
                
                    animController.SetBool("everyoneAppeared", true);
                    animationsUndergo = false;
                    winnerStuff[0].GetComponent<RectTransform>().position = winnerSpot.position;
                }
            }   
        }
	}

    /// <summary>
    /// sets the queue for who shows on screen first to last
    /// </summary>
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

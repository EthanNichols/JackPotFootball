using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Used for transferring data between scenes, such as settings
/// </summary>
public static class Settings {

    private static int playerNum = 1;


    /// <summary>
    /// sets the player number
    /// </summary>
    public static int PlayerNum
    {
        get
        {
            return playerNum;
        }
        set
        {
            playerNum = value;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player 
{
    private int highScore;
    public int totalCoin;
    private int totalCoinCollected;

    public Player()
    {
        highScore = PlayerPrefs.GetInt("Highscore");
        totalCoin = PlayerPrefs.GetInt("Infected");
    }

    
}

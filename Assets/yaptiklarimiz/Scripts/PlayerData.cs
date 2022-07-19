using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData : MonoBehaviour
{
    public int highScore;
    public int totalCoin; //coin we have
    public int totalCoinCollected; //all coins that we collected


    public PlayerData()
    {
        highScore = PlayerPrefs.GetInt("Highscore");
        totalCoin = PlayerPrefs.GetInt("Infected");
    }
}

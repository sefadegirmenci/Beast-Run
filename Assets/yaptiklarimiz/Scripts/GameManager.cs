using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
/*using GoogleMobileAds;
using GoogleMobileAds.Api;*/

public class GameManager : MonoBehaviour
{
    public string id = "ca-app-pub-7453373669362236~3568926068";
    Player playerToSave;
   

    public Animator caption_Animator;

    int counter = 0;

    public GameObject characters;
    public GameObject bonebody;

    private const int COIN_SCORE_AMOUNT = 5;
    private int lastScore;
    public static GameManager Instance { set; get; }

    public bool IsDead { set; get; }
    public bool isGameStarted = false;
    private PlayerMotor motor;
    public Animator gameCanvasAnim;
    public int hiscore;


    //UI AND UI FIELDS
    public Text scoreText, coinText, modifierText, highscoreText;
    private float score, coinScore, modifierScore;

    //Death Menu
    public Animator deathMenuAnim;
    public Text deadScoreText, deadCoinText;
    //END

    public bool IsScrolling;
    

    private void Awake()
    {

        Instance = this;
        modifierScore = 1;

        motor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();

        scoreText.text = score.ToString("0");
        modifierText.text = "x" + modifierScore.ToString("0.0");
        coinText.text = coinScore.ToString("0");
        hiscore = PlayerPrefs.GetInt("Highscore");
        highscoreText.text = hiscore.ToString();

       


    }


    private void Update()
    {
    

        if ((MobileInput.Instance.Tap) && !isGameStarted)
        {
            FindObjectOfType<Audiomanager>().Play("Theme");
   
            IsScrolling = true;
            isGameStarted = true;
            Vector3 temp = bonebody.transform.position;
            temp.y -= 0.5f;
            characters.transform.position = temp;
            bonebody.transform.position = temp;
            motor.StartRunning();
            gameCanvasAnim.SetTrigger("Hide");


            FindObjectOfType<CameraMotor>().IsMoving = true;




        }

        if (isGameStarted && !IsDead)
        {
            counter++;
            if (counter == 200)
            {
                counter = 0;
            }

            //BUMP THE SCORE UP
            score += (Time.deltaTime * modifierScore);
            if (lastScore != (int)score)
            {
                lastScore = (int)score;
                scoreText.text = score.ToString("0");
            }

        }
    }
    public void UpdateModifier(float modifierAmount)
    {
        modifierScore = 1.0f + modifierAmount;
        modifierText.text = "x" + modifierScore.ToString("0.0");
    }

    public void GetCoin()
    {
        FindObjectOfType<Audiomanager>().Play("Eat");
        coinScore++;
        coinText.text = coinScore.ToString("0");
        score += COIN_SCORE_AMOUNT;
        scoreText.text = score.ToString("0");
    }

    public void onPlayButton()
    {
        FindObjectOfType<Audiomanager>().Play("ButtonPush");

     

        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void onMarketButton()
    {
        FindObjectOfType<Audiomanager>().Play("ButtonPush");
        ShowOptions so = new ShowOptions();
        Advertisement.Show("rewardedVideo", so);
      

        SceneManager.LoadScene("market");

    }

    public void OnDeath()
    {
        FindObjectOfType<Audiomanager>().Stop("Theme");
        FindObjectOfType<Audiomanager>().Play("PlayerDeath");
        IsDead = true;
        deadScoreText.text = scoreText.text;
        deadCoinText.text = coinScore.ToString("0");

    



        deathMenuAnim.SetTrigger("Dead");

        IsScrolling = false;



        //check if this is the highest score

        if (score > PlayerPrefs.GetInt("Highscore"))
        {
            float s = score;
            if (s % 1 == 0)
                s++;
            hiscore = (int)s;
            PlayerPrefs.SetInt("Highscore", hiscore);
            caption_Animator.SetTrigger("Advice");
            FindObjectOfType<Audiomanager>().Play("HiScore");
            PlayerPrefs.SetInt("ScoreToUpdate", hiscore);
        }
        else PlayerPrefs.SetInt("ScoreToUpdate", 0);
        hiscore = PlayerPrefs.GetInt("Highscore");

        PlayerPrefs.SetInt("Infected", PlayerPrefs.GetInt("Infected") + (int)coinScore);

        SaveSystem.SaveData();

        
    }

  
    public void RequestRevive()
    {

        ShowOptions so = new ShowOptions();
        Advertisement.Show("rewardedVideo", so);
        deathMenuAnim.SetTrigger("Revive");

        FindObjectOfType<CameraMotor>().transform.Translate(new Vector3(0.0f, 0.0f, -5.0f));


            IsDead = false;
            isGameStarted = false;

             FindObjectOfType<CharacterController>().Move(new Vector3(0.0f, 0.0f, -17.0f));
        Vector3 newvector = FindObjectOfType<CharacterController>().transform.position;
        newvector.y = 0.0f;
        FindObjectOfType<CharacterController>().transform.position = newvector;               
    }



}









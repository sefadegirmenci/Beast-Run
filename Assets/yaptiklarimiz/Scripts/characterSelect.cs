using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class characterSelect : MonoBehaviour
{
    private GameObject[] characterList;
    public Image plague;

    public int index;
    public bool[] characterBought;
    public int infected;
    private int coin;


    public int[] characterPrices;
    public int selected;
    public Button buyButton;
    public Text infectedText;

    // Start is called before the first frame update
    void Start()
    {
        //BUNU ANASAYFA EKLE
        PlayerData data =SaveSystem.LoadPlayer();
        PlayerPrefs.SetInt("Highscore", data.highScore);
        PlayerPrefs.SetInt("Infected", data.totalCoin);


        infected = PlayerPrefs.GetInt("Infected");

        FindObjectOfType<Audiomanager>().Play("MarketBackground");
        selected = PlayerPrefs.GetInt("Character Selected");
        characterList = new GameObject[transform.childCount];
        characterBought = new bool[transform.childCount];
        characterPrices = new int[transform.childCount];

        infectedText.gameObject.SetActive(true);
        infectedText.text = infected+"" ;
        //fill price array
            characterPrices[1] = 100;
        characterPrices[2] = 170;
        characterPrices[3] = 250;
        characterPrices[4] = 350;
        characterPrices[5] = 500;
    


        //fill virus array
        for (int i = 0; i < transform.childCount; i++)
            characterList[i] = transform.GetChild(i).gameObject;

        //toggle off viruses
        foreach (GameObject go in characterList)
            go.SetActive(false);

        //toggle on first virus
        if (characterList[selected])
            characterList[selected].SetActive(true);

 
        buyButton.GetComponentInChildren<Text>().text = "Selected";

        plague.gameObject.SetActive(false);
        characterBought[selected] = true;

    }
  
    public void ToggleLeft()
    {
        FindObjectOfType<Audiomanager>().Play("Button");
        //deactivate previous character
        characterList[index].SetActive(false);
        //initialize to false the image
        plague.gameObject.SetActive(false);

        index--;
        if (index < 0)
            index = characterList.Length - 1;
        //activating current character
        characterList[index].SetActive(true);

        //if character is owned -> select , if not owned -> buy
        if (characterBought[index])
            buyButton.GetComponentInChildren<Text>().text = "Select";

        else
        {
            buyButton.GetComponentInChildren<Text>().text = characterPrices[index] + "";
            plague.gameObject.SetActive(true);
        }
        //indicate selected character
        if (index==selected) buyButton.GetComponentInChildren<Text>().text = "Selected";

        


    }
    public void ToggleRight()
    {
        FindObjectOfType<Audiomanager>().Play("Button");

        characterList[index].SetActive(false);

        index++;
        if (index == characterList.Length)
            index = 0;

        characterList[index].SetActive(true);
        plague.gameObject.SetActive(false);
        //karakterin satın alınma durumuna göre butonu değiştir
        if (characterBought[index]) buyButton.GetComponentInChildren<Text>().text = "Select";
        else
        {
            buyButton.GetComponentInChildren<Text>().text = characterPrices[index] + "";
            plague.gameObject.SetActive(true);
        }
        if (index == selected) buyButton.GetComponentInChildren<Text>().text = "Selected";


      
   
  

    }

    public void playGame()
    {
        FindObjectOfType<Audiomanager>().Stop("MarketBackground");
        FindObjectOfType<Audiomanager>().Play("Button");


        PlayerPrefs.SetInt("Infected", infected);
        SceneManager.LoadScene("Game");
        PlayerPrefs.SetInt("Character Selected", selected);
        
    }

    public void buyCharacter()
    {
        plague.gameObject.SetActive(false);
        if (!characterBought[index])
        {

            if (infected >= characterPrices[index])
            {
                characterBought[index] = true;
                selected = index;
                infected -= characterPrices[index];
                infectedText.text = infected + "";
                buyButton.GetComponentInChildren<Text>().text = "Selected";
                FindObjectOfType<Audiomanager>().Play("MonsterBuy");
            }
            else
            {
                buyButton.GetComponentInChildren<Text>().text = "Not Enough Infected";
                FindObjectOfType<Audiomanager>().Play("MonsterNotBuy");

            }
        }
        else
        {
            FindObjectOfType<Audiomanager>().Play("Button");
            buyButton.GetComponentInChildren<Text>().text = "Selected";
            selected = index;
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

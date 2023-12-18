using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public Image currentHappy;
    public Image currentClean;
    public Image currentHunger;

    public Text happyText;
    public Text cleanText;
    public Text hungryText;

    private float happiness = 100;
    private float hygiene = 100;
    private float hunger = 100;
    private float max = 100;

    public Button Feed;
    public Button Clean;
    public Button Play;

    public Text GameOverText;
    public Text PlayBubbleText;
    public Text CleanBubbleText;
    public Text FoodBubbleText;

    public Text HungryText;
    public Text CleanText;
    public Text HappyText;

    public Image DinoFeliz;
    public Image DinoEntendiado;
    public Image DinoFome;
    public Image DinoSujo;
    public Image DinoEat;
    public Image DinoSoap;
    public Image Ghost;
    public Image Crown;
    public GameObject GameOverPanel;

    private void Start()
    {
        InvokeRepeating("IncreaseGummy", 0, 5);

        Button btn1 = Feed.GetComponent<Button>();
        btn1.onClick.AddListener(FeedThePet);
        
        Button btn2 = Clean.GetComponent<Button>();
        btn2.onClick.AddListener(CleanThePet);
        
        Button btn3 = Play.GetComponent<Button>();
        btn3.onClick.AddListener(PlayThePet);

        CleanBubbleText.CrossFadeAlpha(0, 0.001f, true);
        FoodBubbleText.CrossFadeAlpha(0, 0.001f, true);
        PlayBubbleText.CrossFadeAlpha(0, 0.001f, true);


        DinoFeliz.gameObject.SetActive(true);
        DinoEntendiado.gameObject.SetActive(false);
        DinoFome.gameObject.SetActive(false);
        DinoSujo.gameObject.SetActive(false);
        DinoSoap.gameObject.SetActive(false);
        DinoEat.gameObject.SetActive(false);
        Ghost.gameObject.SetActive(false);
        Crown.gameObject.SetActive(false);
        GameOverPanel.gameObject.SetActive(false);
        GameOverText.gameObject.SetActive(false);

        happiness = PersistentData.persistentHappiness;
        hygiene = PersistentData.persistentHygiene;
        hunger = PersistentData.persistentHunger;

        UpdateHungerBar();
        UpdateHappyBar();
        UpdateHygieneBar();
        Update();
    }

    private void Update()
    {
        happiness -= 1.5f * Time.deltaTime;
        if(happiness < 0)
        {
            happiness = 0;
        }
        else if(happiness > 100)
        {
            happiness = 100;
        }

        hunger -= 1f * Time.deltaTime;
        if (hunger < 0)
        {
            hunger = 0;
        }
        else if (hunger > 100)
        {
            hunger = 100;
        }

        hygiene -= 0.5f * Time.deltaTime;
        if (hygiene < 0)
        {
            hygiene = 0;
        }
        else if (hygiene > 100)
        {
            hygiene = 100;
        }

        PersistentData.persistentHappiness = happiness;
        PersistentData.persistentHygiene = hygiene;
        PersistentData.persistentHunger = hunger;

        needsCheck();
        goodParentCheck();
        UpdateHungerBar();
        UpdateHappyBar();
        UpdateHygieneBar();
        GameOver();
    }

    private void needsCheck()
    {
        if (happiness <= 70)
        {
            PlayBubbleText.CrossFadeAlpha(1, 0.5f, true);
        }
        else
        {
            PlayBubbleText.CrossFadeAlpha(0, 0.5f, true);
        }

        if (hunger <= 50)
        {
            FoodBubbleText.CrossFadeAlpha(1, 0.5f, true);
        }
        else
        {
            FoodBubbleText.CrossFadeAlpha(0, 0.5f, true);
        }

        if (hygiene <= 20)
        {
            CleanBubbleText.CrossFadeAlpha(1, 0.5f, true);
        }
        else
        {
            CleanBubbleText.CrossFadeAlpha(0, 0.5f, true);
        }
    }

    private void IncreaseGummy()
    {
        if (happiness >= 70 || hygiene >= 20 || hunger >= 50)
        {
            PersistentData.Gummy += 1;
        }
    }

    private void goodParentCheck()
    {
        if(happiness >= 70 && hygiene >= 20 && hunger >= 50)
        {
            DinoFeliz.gameObject.SetActive(true);
            DinoEntendiado.gameObject.SetActive(false);
            DinoFome.gameObject.SetActive(false);
            DinoSujo.gameObject.SetActive(false);
            DinoEat.gameObject.SetActive(false);
            DinoSoap.gameObject.SetActive(false);
        }
        else if(happiness < 70 && hygiene >= 20 && hunger >= 50)
        {
            DinoFeliz.gameObject.SetActive(false);
            DinoEntendiado.gameObject.SetActive(true);
            DinoFome.gameObject.SetActive(false);
            DinoSujo.gameObject.SetActive(false);
            DinoEat.gameObject.SetActive(false);
            DinoSoap.gameObject.SetActive(false);
        }
        else if(hygiene >= 20 && hunger < 50)
        {
            DinoFeliz.gameObject.SetActive(false);
            DinoEntendiado.gameObject.SetActive(false);
            DinoFome.gameObject.SetActive(true);
            DinoSujo.gameObject.SetActive(false);
            DinoEat.gameObject.SetActive(false);
            DinoSoap.gameObject.SetActive(false);
        }
        else if(hygiene < 20)
        {
            DinoFeliz.gameObject.SetActive(false);
            DinoEntendiado.gameObject.SetActive(false);
            DinoFome.gameObject.SetActive(false);
            DinoSujo.gameObject.SetActive(true);
            DinoEat.gameObject.SetActive(false);
            DinoSoap.gameObject.SetActive(false);
        }
    }

    private void UpdateHungerBar()
    {
        float ratio = hunger / max;
        currentHunger.rectTransform.localScale = new Vector3(ratio, 1, 1);
        hungryText.text = (ratio * 100).ToString("0") + "%";
    }

    private void UpdateHappyBar()
    {
        float ratio = happiness / max;
        currentHappy.rectTransform.localScale = new Vector3(ratio, 1, 1);
        happyText.text = (ratio * 100).ToString("0") + "%";
    }

    private void UpdateHygieneBar()
    {
        float ratio = hygiene / max;
        currentClean.rectTransform.localScale = new Vector3(ratio, 1, 1);
        cleanText.text = (ratio * 100).ToString("0") + "%";
    }

    private void FeedThePet()
    {
        hunger += 20;
        happiness += 10;
        PersistentData.Gummy -= 3;

        needsCheck();
        UpdateHungerBar();
        UpdateHappyBar();
        goodParentCheck();
    }


    private void CleanThePet()
    {

        hygiene += 20;
        happiness += 5;
        PersistentData.Gummy -= 5;
        needsCheck();
        UpdateHygieneBar();
        UpdateHappyBar();
        goodParentCheck();
    }

    private void PlayThePet()
    {
        happiness += 25;
        hunger -= 10;
        hygiene -= 20;
        PersistentData.Gummy -= 2;
        needsCheck();
        UpdateHappyBar();
        UpdateHungerBar();
        UpdateHygieneBar();
        goodParentCheck();
    }

    private void GameOver()
    {
        if (happiness <= 0 || hygiene <= 0 || hunger <= 0)
        {
            GameOverText.gameObject.SetActive(true);
            GameOverPanel.gameObject.SetActive(true);
            Ghost.gameObject.SetActive(true);
            Crown.gameObject.SetActive(true);
            DinoFeliz.gameObject.SetActive(false);
            DinoEntendiado.gameObject.SetActive(false);
            DinoFome.gameObject.SetActive(false);
            DinoSujo.gameObject.SetActive(false);
            DinoEat.gameObject.SetActive(false);
            DinoSoap.gameObject.SetActive(false);

            HungryText.gameObject.SetActive(false);
            HappyText.gameObject.SetActive(false);
            CleanText.gameObject.SetActive(false);

            Feed.interactable = false;
            Clean.interactable = false;
            Play.interactable = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PuzzelController : MonoBehaviour
{
    public GameObject puzzelField;
    public GameObject cardHandlerPrefab;
    public Sprite backCardSprite;
    public int size;
    public bool canSelect;

    public bool firstGuess;
    public bool secondGuess;
    public string firstGuessName;
    public string secondGuessName;
    public int firstGuessIndex;
    public int secondGuessIndex;
    public int countGuesses;
    public int countGuessesCorrect;
    public int correctGuesses;


    public List<Card> cards = new List<Card>();

    // Start is called before the first frame update
    void Start()
    {
        correctGuesses = size / 2;
        CreateButtons(size);
        canSelect = true;
    }

    void CreateButtons(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            GameObject buttonGameObject = Instantiate(cardHandlerPrefab);
            buttonGameObject.transform.SetParent(puzzelField.transform, false);

            TMP_Text text = buttonGameObject.transform.GetChild(0).GetComponentInChildren<TMP_Text>();
            text.text = cards[i].nameCard;

            RawImage icon = buttonGameObject.transform.GetChild(0).GetComponentInChildren<RawImage>();
            icon.texture = cards[i].icon;

            Button button = buttonGameObject.GetComponent<Button>();
            cards[i].button = button;

            Card newCard = cards[i];

            button.onClick.AddListener(() => StartEventButton(newCard));
        }
    }

    public void ResetEventButton()
    {
        firstGuess = false;
        secondGuess = false;
        firstGuessName = "";
        secondGuessName = "";
        canSelect = true;
    }

    public void StartEventButton(Card card)
    {
        if (!canSelect)
            return;
        
        card.button.enabled = false;

        StartCoroutine(ShowCard(card.button.gameObject.transform.GetChild(0).gameObject));
        
        if (!firstGuess)
        {
            firstGuess = true;
            firstGuessName = card.nameCard;
            firstGuessIndex = card.id;
        }
        else if (!secondGuess)
        {
            secondGuess = true;
            secondGuessName = card.nameCard;
            secondGuessIndex = card.id;
            StartCoroutine(Check());
        }
    }

    IEnumerator Check()
    {
        canSelect = false;
        countGuesses++;

        if (secondGuessName == firstGuessName)
        {
            countGuessesCorrect++;
            yield return new WaitForSeconds(.5f);
            StartCoroutine(CheckTheGameIsFinish());
            Debug.Log("The puzzle Match");
            
            //ActivarPiedraPapelTijeras
            //Sumar puntos
        }
        else
        {
            yield return new WaitForSeconds(.8f);
            StartCoroutine(HideCards());
            Debug.Log("The puzzle dont Match");
        }
    }

    IEnumerator CheckTheGameIsFinish()
    {
        if (countGuessesCorrect < correctGuesses) 
        {
            countGuesses++;
            ResetEventButton();
        }
        else
        {
            yield return new WaitForSeconds(.5f);
            Debug.Log("EndGame");
            
            for (int i = 0; i < size; i++)
            {
                cards[i].button.transform.GetChild(0).gameObject.SetActive(false);
                cards[i].button.enabled = false;
            }
        }
        
        yield return null;
    }

    IEnumerator HideCards()
    {
        for (int i = 0; i < size; i++)
        {
            if (cards[i].nameCard == firstGuessName && cards[i].id == firstGuessIndex ||
                cards[i].nameCard == secondGuessName && cards[i].id == secondGuessIndex)
            {
                cards[i].button.transform.GetChild(0).gameObject.SetActive(false);
                cards[i].button.enabled = true;
            }
        }

        ResetEventButton();
        yield return null;
    }


    IEnumerator ShowCard(GameObject handlerButton)
    {
        handlerButton.SetActive(true);

        //smoothTimeUpdate += Time.unscaledDeltaTime;

        //Ra

        //if (eventTimerImage != null)
        //{
        //    ui.eventTimerImage.fillAmount = smoothTimeUpdate / eventData.time;
        //}

        yield return null;
    }
}

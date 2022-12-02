using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PuzzelController : MonoBehaviour
{
    public GameObject Boddy;
    public GameObject puzzelField;
    public GameObject puzzelFieldRandom;
    public GameObject secondGamePanel;
    public GameObject cardHandlerPrefab;
    public Sprite backCardSprite;
    public bool canSelect;

    public bool firstGuess;
    public bool secondGuess;
    public int countGuesses;
    public int countGuessesCorrect;
    public int correctGuesses;

    int firstGuessIndex;
    int secondGuessIndex; 
    string firstGuessName;
    string secondGuessName;

    public List<Card> cards = new List<Card>();
    public List<Card> provitionalCards = new List<Card>();

    //boolsDelOtroJuego
    public bool isStartSecundGame;
    public bool result;
    
    public void StartningSettup(int size)
    {
        correctGuesses = size / 2;
        StartCoroutine(CreateButtons(size));
    }

    IEnumerator CreateButtons(int quantity)
    {
        if (!Boddy.activeInHierarchy)
            Boddy.gameObject.SetActive(true);

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
            
            provitionalCards.Add(newCard);

            yield return new WaitForSeconds(.3f);
        }

        StartCoroutine(Shuffle(provitionalCards));
    }

    public void ResetEventButton()
    {
        firstGuess = false;
        secondGuess = false;
        firstGuessName = "";
        secondGuessName = "";
        canSelect = true;
    }

    IEnumerator StartSecondGame()
    {
        SoundManager.Instance?.EndSound("MainBackGround");
        secondGamePanel.SetActive(true);
        SoundManager.Instance?.PlayNewSound("LevelBackGround");
        isStartSecundGame = true;
        yield return new WaitUntil(() => !isStartSecundGame);
        secondGamePanel.SetActive(false);

        if (result)
        {
            countGuessesCorrect++;
            StartCoroutine(CheckTheGameIsFinish());
        }
        else
        {
            StartCoroutine(HideCards());
            StartCoroutine(Shuffle(provitionalCards));
        }
        
        yield return null;

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
            Debug.Log(1);
            firstGuessIndex = card.id;
        }
        else if (!secondGuess)
        {
            secondGuess = true;
            secondGuessName = card.nameCard;
            secondGuessIndex = card.id;
            Debug.Log(2);
            StartCoroutine(Check());
        }
    }

    IEnumerator Shuffle(List<Card> list)
    {
        //crear un randome de los provicionales.
        for (int i = 0; i < list.Count; i++)
        {
            Card provitional = list[i];
            int randomNum = Random.Range(i,list.Count);
            list[i] = list[randomNum];
            list[randomNum] = provitional;
        }

        if (puzzelField.activeInHierarchy)
        {
            puzzelFieldRandom.SetActive(true);

            for (int i = 0; i < list.Count; i++)
            {
                provitionalCards[i].button.gameObject.transform.SetParent(puzzelFieldRandom.transform, false);
            }
            
            puzzelField.SetActive(false);
        }
        else
        {
            
            puzzelField.SetActive(true);
            
            for (int i = 0; i < list.Count; i++)
            {
                provitionalCards[i].button.gameObject.transform.SetParent(puzzelField.transform, false);
            }

            puzzelFieldRandom.SetActive(false);
        }
        
        ResetEventButton();

        canSelect = true;
      
        yield return null;

    }

    IEnumerator Check()
    {
        canSelect = false;
        countGuesses++;

        if (secondGuessName == firstGuessName)
        {
            //ActivarPiedraPapelTijeras
            StartCoroutine(StartSecondGame());
            Debug.Log("The puzzle Match");
            yield return new WaitForSeconds(.2f);
        }
        else
        {
            yield return new WaitForSeconds(.5f);
            StartCoroutine(HideCards());
            Debug.Log("The puzzle dont Match");
        }

        yield return null;
    }

    IEnumerator CheckTheGameIsFinish()
    {
        if (countGuessesCorrect < correctGuesses) 
        {
            ResetEventButton();
        }
        else
        {
            yield return new WaitForSeconds(.5f);
            for (int i = 0; i < provitionalCards.Count; i++)
            {
                cards[i].button.transform.GetChild(0).gameObject.SetActive(false);
                cards[i].button.enabled = false;
            }

            GameManager.Instance.Winner();

        }

        yield return null;
    }

    IEnumerator HideCards()
    {
        canSelect = false;
        for (int i = 0; i < provitionalCards.Count; i++)
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

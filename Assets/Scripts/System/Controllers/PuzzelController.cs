using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PuzzelController : MonoBehaviour
{
    public GameObject puzzelField;
    public GameObject puzzelFieldRandom;
    public GameObject cardHandlerPrefab;
    public Sprite backCardSprite;
    public int size;
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


    // Start is called before the first frame update
    void Start()
    {
        correctGuesses = size / 2;
        StartCoroutine(CreateButtons(size));
    }

    IEnumerator CreateButtons(int quantity)
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
        ResetEventButton();
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
            Debug.Log("sss");
        }
        else
        {
            Debug.Log("sss2");
            
            puzzelField.SetActive(true);
            
            for (int i = 0; i < list.Count; i++)
            {
                provitionalCards[i].button.gameObject.transform.SetParent(puzzelField.transform, false);
            }

            puzzelFieldRandom.SetActive(false);
        }
        yield return null;
        
        Debug.Log("xx");
        canSelect = true;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(Shuffle(provitionalCards));
        }
    }

    IEnumerator Check()
    {
        canSelect = false;
        countGuesses++;

        if (secondGuessName == firstGuessName)
        {
            countGuessesCorrect++;
            StartCoroutine(CheckTheGameIsFinish());
            Debug.Log("The puzzle Match");
            
            //ActivarPiedraPapelTijeras
            //Sumar puntos
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
        canSelect = false;

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
        canSelect = false;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PuzzelController : MonoBehaviour
{
    public GameObject puzzelField;
    public GameObject cardHandlerPrefab;
    public Sprite backCardSprite;

    public List<Card> cards = new List<Card>();
    public List<Button> buttons = new List<Button>();

    // Start is called before the first frame update
    void Start()
    {
        CreateButtons(8); 
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

            buttonGameObject.transform.GetChild(0).gameObject.SetActive(false);
            Button button = buttonGameObject.GetComponent<Button>();
            buttons.Add(button);
            button.onClick.AddListener(StartEventButton);
        }
    }

    public void StartEventButton()
    {
        StartCoroutine(ShowCard());
    }

    IEnumerator ShowCard()
    {
        yield return null;
    }
}

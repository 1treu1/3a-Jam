using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameChoices {
    NONE,
    ROCK,
    PAPER,  
    SCISSORS
}

public class GameplayController : MonoBehaviour {

    [SerializeField]
    private Sprite rock_Sprite, paper_Sprite, scissors_Sprite;

    [SerializeField]
    private Image playerChoice_Img, oponentChoice_Img;

    [SerializeField]
    private TMPro.TMP_Text infoText;

    private GameChoices player_Choice = GameChoices.NONE, opponent_Choice = GameChoices.NONE;

    private AnimationController animationController;

    public bool isStartSecundGame;
    public bool result;

    private void Awake()
    {
        animationController = GetComponent<AnimationController>();
    }
    public void SetChoices(GameChoices gameChoices)
    {
        switch (gameChoices)
        {
            case GameChoices.ROCK:
                playerChoice_Img.sprite = rock_Sprite;
                player_Choice = GameChoices.ROCK;
                break;

            case GameChoices.PAPER:
                playerChoice_Img.sprite = paper_Sprite;
                player_Choice = GameChoices.PAPER;
                break;

            case GameChoices.SCISSORS:
                playerChoice_Img.sprite = scissors_Sprite;
                player_Choice = GameChoices.SCISSORS;
                break;
        }
        
        SetOpponentChoice();
        DetermineWinner();
    }

    void SetOpponentChoice()
    {
        int index = Random.Range(0, 3);

        switch (index)
        {
            case 0:
                opponent_Choice = GameChoices.ROCK;
                oponentChoice_Img.sprite = rock_Sprite;
                break;

            case 1:
                opponent_Choice = GameChoices.PAPER;
                oponentChoice_Img.sprite = paper_Sprite; 
                break;

            case 2:
                opponent_Choice = GameChoices.SCISSORS;
                oponentChoice_Img.sprite = scissors_Sprite;
                break;
        }
    }

    void DetermineWinner()
    {
        if (player_Choice == opponent_Choice)
        {
            infoText.text = "It's Draw";
            StartCoroutine(DisplayWinnerAndRestart());
            return;
        }

        if (player_Choice == GameChoices.PAPER &&  opponent_Choice ==GameChoices.ROCK)
        {
            infoText.text = "You Winner";
            StartCoroutine(DisplayWinnerAndRestart());
            result = true;
            GameManager.Instance.secundGameController.isStartSecundGame = false;
            return; 
        }

        if (opponent_Choice == GameChoices.PAPER && player_Choice == GameChoices.ROCK)
        {
            infoText.text = "You Lose";
            StartCoroutine(DisplayWinnerAndRestart());
            result = false;
            GameManager.Instance.secundGameController.isStartSecundGame = false;
            return;
        }


        if (player_Choice == GameChoices.ROCK && opponent_Choice == GameChoices.SCISSORS)
        {
            infoText.text = "You Winner";
            StartCoroutine(DisplayWinnerAndRestart());
            result = true;
            GameManager.Instance.secundGameController.isStartSecundGame = false;
            return;
        }

        if (opponent_Choice == GameChoices.ROCK && player_Choice == GameChoices.SCISSORS)
        {
            infoText.text = "You Lose";
            StartCoroutine(DisplayWinnerAndRestart());
            result = false;
            GameManager.Instance.secundGameController.isStartSecundGame = false;
            return;
        }

        if (player_Choice == GameChoices.SCISSORS && opponent_Choice == GameChoices.PAPER)
        {
            infoText.text = "You Winner";
            StartCoroutine(DisplayWinnerAndRestart());
            result = true;
            GameManager.Instance.secundGameController.isStartSecundGame = false;
            return;
        }

        if (opponent_Choice == GameChoices.SCISSORS && player_Choice == GameChoices.PAPER)
        {
            infoText.text = "You Lose";
            StartCoroutine(DisplayWinnerAndRestart());
            result = false;
            GameManager.Instance.secundGameController.isStartSecundGame = false;
            return;
        }
    }

    IEnumerator DisplayWinnerAndRestart()
    {
        
        yield return new WaitForSeconds(2f);
        infoText.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(2f);
        infoText.gameObject.SetActive(false);

        animationController.ResetAnimations();
        yield return new WaitForSeconds(1f);
    }

}// class

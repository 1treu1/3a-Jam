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

public class GameplayController : MonoBehaviour{ 

    [SerializeField]
    private Sprite rock_Sprite, paper_Sprite, scissors_Sprite;

    [SerializeField]
    private Image playerChoice_Img, oponentChoice_Img;

    [SerializeField]
    private Text infoText;

    private GameChoices player_Choice = GameChoices.NONE, opponent_Choice = GameChoices.NONE; 

    private AnimationController AnimationController;
    


   
}// class

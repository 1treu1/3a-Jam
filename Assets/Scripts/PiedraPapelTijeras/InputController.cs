using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    private AnimationController AnimationController;
    private GameplayController GameplayController;

    private string playersChoice;

    void Awake() {
        AnimationController = GetComponent<AnimationController>();
        GameplayController = GetComponent<GameplayController>();
    }
    
    public void GetChoice() { 

        string choiceName = UnityEngine.EventSystems.
            EventSystem.current.currentSelectedGameObject.name;

        GameChoices SelectedChoice = GameChoices.NONE;

        switch(choiceName) {
 
            case "Rock": 
                SelectedChoice = GameChoices.ROCK;
                break;

            case "Paper": 
                SelectedChoice = GameChoices.PAPER;
                break;

            case "Scissors":
                SelectedChoice = GameChoices.SCISSORS; 
                break;

        }

        //GameplayController.SetChoices(SelectedChoice);
        AnimationController.PlayerMadeChoice();       

    }


    
}//class



















 

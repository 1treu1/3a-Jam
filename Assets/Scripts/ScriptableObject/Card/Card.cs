using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Card", menuName = "ScriptableObjects/Card", order = 2)]
public class Card : ScriptableObject
{
    public string nameCard;
    public int id;
    public Texture icon;
    public Button button;
}

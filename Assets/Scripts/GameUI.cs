using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TurnManager turnManager;
    public TextMeshProUGUI turn;
    public TextMeshProUGUI check;
    public RectTransform background;
    bool prevTurn = true;
    void Start()
    {

    }

    void Update()
    {
        if (turnManager.turnWhite != prevTurn)
        {
            if (turnManager.turnWhite)
            {
                turn.text = "TURN: WHITE";
            }
            else
            {
                turn.text = "TURN: BLACK";
            }
            prevTurn = turnManager.turnWhite;
        }
        if (turnManager.check.inCheck)
        {
            check.enabled = true;
            if(turnManager.turnWhite)
            {
                check.text = "IN CHECK: WHITE";
            }
            {
                check.text = "IN CHECK: BLACK";
            }/*
            background.anchoredPosition = new Vector2(250,80);
            background.sizeDelta = new Vector2(525, 170);*/
        }
        else
        {
            check.enabled = false;/*
            background.anchoredPosition = new Vector2(250, 40);
            background.sizeDelta = new Vector2(525, 85);*/
        }
    }
}

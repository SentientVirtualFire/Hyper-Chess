using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TurnManager turnManager;
    public TextMeshProUGUI turn;
    public GameObject check;
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
            check.SetActive(true);
            background.anchoredPosition = new Vector2(190,80);
            background.sizeDelta = new Vector2(400, 170);
        }
        else
        {
            check.SetActive(false);
            background.anchoredPosition = new Vector2(190, 40);
            background.sizeDelta = new Vector2(400, 85);
        }
    }
}

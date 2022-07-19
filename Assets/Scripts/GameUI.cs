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
        if (turnManager.isCheck)
        {
            check.enabled = true;
            check.gameObject.transform.parent.gameObject.SetActive(true);
            check.text = $"IN CHECK: {turnManager.checkedTeam}";
        }
        else
        {
            check.enabled = false;
            check.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
}
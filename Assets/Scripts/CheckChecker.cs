using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckChecker : MonoBehaviour
{
    public GameObject BlackKing;
    public GameObject WhiteKing;
    public bool inCheck;
    public string checkedTeam;
    public TurnManager turnManager;
    GameObject blacks;
    GameObject whites;
    bool prevTurn = true;
    void Start()
    {
        blacks = turnManager.blacks;
        whites = turnManager.whites;
    }
    void Update()
    {
        if(turnManager.turnWhite != prevTurn)
        {
            CheckCheck();
            prevTurn = turnManager.turnWhite;
        }
    }
    public bool CheckCheck()
    {
        foreach (var i in TurnManager.AllMovesFinder(whites))
        {
            foreach (var j in i.attacks)
            {
                if (j.CompareTag("King"))
                {
                    checkedTeam = "BLACK";
                    inCheck = true;
                }
            }
        }
        foreach (var i in TurnManager.AllMovesFinder(blacks))
        {
            foreach (var j in i.attacks)
            {
                if (j.CompareTag("King"))
                {
                    checkedTeam = "WHITE";
                    inCheck = true;
                }
            }
        }
        if(inCheck)
        {
            return true;
        }
        return false;
    }
}
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
    }
    void Update()
    {
        if(turnManager.turnWhite != prevTurn)
        {
            List<Board> boards = turnManager.boards;
            int turnNum = turnManager.turnNum;
            if (CheckCheck() && boards[turnNum - 1].isCheck)
            {
                TurnManager.LoadBoard(boards[turnNum - 1], turnManager);
                boards.RemoveAt(turnNum + 1);
            }
            prevTurn = turnManager.turnWhite;
        }
    }
    public bool CheckCheck()
    {
        foreach (var i in TurnManager.AllMovesFinder(false))
        {
            foreach (var j in i.attacks)
            {
                if (j.CompareTag("King"))
                {
                    if (i.piece.layer == 6)
                    {
                        checkedTeam = "BLACK";
                    }
                    else
                    {
                        checkedTeam = "WHITE";
                    }
                    return true;
                }
            }
        }
        return false;
    }
}
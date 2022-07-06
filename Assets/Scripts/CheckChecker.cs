using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckChecker : MonoBehaviour
{
    public GameObject BlackKing;
    public GameObject WhiteKing;
    public bool inCheck;
    public string checkedTeam;
    public TurnManager turnManager;
    public List<Moves> checkMoves = new List<Moves>();
    public int MoveCount;
    public GameObject attacker;
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
            if (!turnManager.turnWhite)
            {
                if (CheckCheck(whites))
                {
                    inCheck = true;
                    checkedTeam = "BLACK";
                    checkMoves = FindSolutions(blacks);
                }
                else
                {
                    inCheck = false;
                }
            }
            else
            {
                if (CheckCheck(blacks))
                {
                    inCheck = true;
                    checkedTeam = "WHITE";
                    checkMoves = FindSolutions(whites);
                }
                else
                {
                    inCheck = false;
                }
            }
            foreach(var i in checkMoves)
            {
                print(i.attacks.Count+i.positions.Count);
            }
            prevTurn = turnManager.turnWhite;
        }
    }
    bool CheckCheck(GameObject team)
    {
        foreach (var i in turnManager.allMovesFinder(team))
        {
            foreach (var j in i.attacks)
            {
                if (j.tag == "King")
                {
                    attacker = i.piece;
                    return true;
                }
            }
        }
        return false;
    }
    List<Moves> FindSolutions(GameObject attacked)
    {
        List<Moves> allMoves = new List<Moves>();
        GameObject defender;
        GameObject offender;
        if (attacked == whites)
        {
            defender = WhiteKing;
        }
        else
        {
            defender = BlackKing;
        }
        if (attacked == blacks)
        {
            offender = whites;
        }
        else
        {
            offender = blacks;
        }
        foreach (var i in turnManager.allMovesFinder(attacked))
        {
            List<Vector3> moves = new List<Vector3>();
            List<GameObject> attackMoves = new List<GameObject>();
            Vector3 origin = i.piece.transform.position;
            foreach (var j in i.attacks)
            {
                if(j == attacker)
                {
                    MoveCount++;
                    attackMoves.Add(j);
                }
            }
            foreach (var j in i.positions)
            {
                GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = j;
                i.piece.transform.position = j;
                if (!CheckCheck(offender))
                {
                    MoveCount++;
                    moves.Add(j);
                }
            }
            i.piece.transform.position = origin;
            Moves unitMoves = new Moves();
            unitMoves.piece = i.piece;
            unitMoves.positions = moves;
            unitMoves.attacks = attackMoves;
            allMoves.Add(unitMoves);
        }
        return allMoves;
    }
}
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
    public List<Moves> checkMoves = new List<Moves>();
    public int MoveCount;
    GameObject attacker;
    GameObject attacked;
    GameObject offender;
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
                    attacked = BlackKing;
                    offender = whites;
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
                    attacked = WhiteKing;
                    offender = blacks;
                    checkMoves = FindSolutions(whites);
                }
                else
                {
                    inCheck = false;
                }
            }
            prevTurn = turnManager.turnWhite;
        }
    }
    bool CheckCheck(GameObject team)
    {
        foreach (var i in TurnManager.AllMovesFinder(team))
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
    List<Moves> FindSolutions(GameObject defender)
    {
        List<Moves> allMoves = new List<Moves>();
        foreach (var i in TurnManager.AllMovesFinder(defender))
        {
            List<Vector3> moves = new List<Vector3>();
            List<GameObject> attackMoves = new List<GameObject>();
            Vector3 origin = i.piece.transform.position;
            if(i.attacks.Contains(attacker))
            {
                MoveCount++;
                attackMoves.Add(attacker);
            }
            foreach (var j in attacker.GetComponent<IPiece>().PathFinder().positions.Intersect(i.positions))
            {
                i.piece.transform.position = j;
                if ((attacker.GetComponent<IPiece>().PathFinder().attacks.Contains(attacked)))
                {
                    MoveCount++;
                    moves.Add(j);
                }
                i.piece.transform.position = origin;
            }
            Moves unitMoves = new Moves();
            unitMoves.piece = i.piece;
            unitMoves.positions = moves;
            unitMoves.attacks = attackMoves;
            allMoves.Add(unitMoves);
        }
        return allMoves;
    }
}
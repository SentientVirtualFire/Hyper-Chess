using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public Pawn pawn;
    public Rook rook;
    public Knight knight;
    public Bishop bishop;
    public Queen queen;
    public King king;
    public GameObject whites;
    public GameObject blacks;
    public Camera mainCamera;
    public GameObject moveTarget;
    public GameObject attackTarget;
    public bool turnWhite = true;
    public int turnNum = 0;

    GameObject selected;
    List<GameObject> moveCubes = new List<GameObject>();
    List<GameObject> attackCubes = new List<GameObject>();
    List<Moves> allMoves = new List<Moves>();
    string[] allTags = new string[7] { "Pawn", "UnmovedPawn", "Rook", "Knight", "Bishop", "Queen", "King" };

    void Start()
    {
        allMoves = allMovesFinder(whites);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            int layerMask;
            if (turnWhite)
            {
                layerMask = 1 << 7;
            }
            else
            {
                layerMask = 1 << 6;
            }
            layerMask = ~layerMask;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                if ((hit.transform.tag == "MoveTarget" || hit.transform.tag == "AttackTarget") && selected != null)
                {
                    nextTurn(selected, hit.transform.gameObject);
                    selected = null;
                }
                else if (allTags.Contains(hit.transform.tag))
                {
                    if (turnWhite)
                    {
                        allMoves = allMovesFinder(whites);
                    }
                    else
                    {
                        allMoves = allMovesFinder(blacks);
                    }
                    selected = hit.transform.gameObject;
                    foreach(var unit in allMoves)
                    { 
                        if(unit.piece == hit.transform.gameObject)
                        { 
                            foreach (var pos in unit.positions)
                            {
                                GameObject cube = Instantiate(moveTarget, pos, Quaternion.identity);
                                moveCubes.Add(cube);
                            }
                            foreach (var piece in unit.attacks)
                            {
                                GameObject cube = Instantiate(attackTarget, piece.transform.position, Quaternion.identity);
                                cube.GetComponent<Target>().target = piece;
                                attackCubes.Add(cube);
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (var item in moveCubes)
                {
                    Destroy(item);
                }
                foreach (var item in attackCubes)
                {
                    Destroy(item);
                }
                selected = null;
            }
        }
    }
    public List<Moves> allMovesFinder(GameObject team, bool doKing = true)
    {
        List<Moves> moves = new List<Moves>();
        foreach (Transform child in team.transform)
        {
            if (child.transform.childCount > 0)
            {
                foreach (Transform grandChild in child.transform)
                {
                    moves.Concat(moveChecker(grandChild.gameObject, moves, doKing));
                }
            }
            else
            {
                moves.Concat(moveChecker(child.gameObject, moves, doKing));
            }
        }
        return moves;
    }
    List<Moves> moveChecker(GameObject unit, List<Moves> moves, bool doKing = true)
    {
        if (unit.transform.tag == "Pawn" || unit.transform.tag == "UnmovedPawn")
        {
            Moves pieceMoves = new Moves();
            if (doKing)
            {
                pieceMoves = pawn.pathFinder(unit);
            }
            else
            {
                pieceMoves = pawn.justAttackPaths(unit);
            }
            moves.Add(pieceMoves);
        }
        else if (unit.transform.tag == "Rook")
        {
            Moves pieceMoves = rook.pathFinder(unit);
            moves.Add(pieceMoves);
        }
        else if (unit.transform.tag == "Knight")
        {
            Moves pieceMoves = knight.pathFinder(unit);
            moves.Add(pieceMoves);
        }
        else if (unit.transform.tag == "Bishop")
        {
            Moves pieceMoves = bishop.pathFinder(unit);
            moves.Add(pieceMoves);
        }
        else if (unit.transform.tag == "Queen")
        {
            Moves pieceMoves = queen.pathFinder(unit);
            moves.Add(pieceMoves);
        }
        else if (unit.transform.tag == "King" && doKing)
        {
            Moves pieceMoves = king.pathFinder(unit);
            moves.Add(pieceMoves);
        }
        return moves;
    }
    void nextTurn(GameObject movedPiece, GameObject target)
    {
        movedPiece.transform.position = target.transform.position;
        if (target.transform.tag == "AttackTarget")
        {
            Destroy(target.transform.gameObject.GetComponent<Target>().target);
        }
        allMoves.Clear();
        foreach (var item in moveCubes)
        {
            Destroy(item);
        }
        foreach (var item in attackCubes)
        {
            Destroy(item);
        }
        if (turnWhite)
        {
            turnWhite = false;
        }
        else
        {
            turnWhite = true;
        }
        
        turnNum++;
    }
}
public class Moves
{
    public GameObject piece;
    public List<GameObject> attacks;
    public List<Vector3> positions = new List<Vector3>();
    public int turn;
}
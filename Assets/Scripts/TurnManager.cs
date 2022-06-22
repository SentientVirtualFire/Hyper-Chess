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
    public bool turnWhite = true;

    GameObject selected;
    List<GameObject> moveCubes = new List<GameObject>();
    List<Moves> allMoves = new List<Moves>();
    string[] allTags = new string[7] { "Pawn", "UnmovedPawn", "Rook", "Knight", "Bishop", "Queen", "King" };

    public class Moves
    {
        public GameObject piece;
        public List<Vector3> positions = new List<Vector3>();
    }

    void Start()
    {
        allMoves = allMovesFinder(whites);
        foreach (var i in allMoves)
        {
            Debug.Log(i);
        }
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            foreach (var item in moveCubes)
            {
                Destroy(item);
            }

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
                if (hit.transform.tag == "MoveTarget" && selected != null)
                {
                    selected.transform.position = hit.transform.position;
                    selected = null;
                    foreach (var item in moveCubes)
                    {
                        Destroy(item);
                    }
                    if (turnWhite)
                    {
                        allMoves = allMovesFinder(blacks);
                        turnWhite = false;
                    }
                    else
                    {
                        allMoves = allMovesFinder(whites);
                        turnWhite = true;
                    }
                }
                else if (allTags.Contains(hit.transform.tag))
                    selected = hit.transform.gameObject;
                foreach (var unit in allMoves)
                {
                    if (unit.piece == selected)
                    {
                        foreach (var pos in unit.positions)
                        {
                            GameObject cube = Instantiate(moveTarget, pos, Quaternion.identity);
                            moveCubes.Add(cube);
                        }
                    }
                }
            }
            else
            {
                selected = null;
            }
        }
    }
    public List<Moves> allMovesFinder(GameObject team)
    {
        List<Moves> moves = new List<Moves>();
        foreach (Transform child in team.transform)
        {
            if (child.transform.childCount > 0)
            {
                foreach (Transform grandChild in child.transform)
                {
                    moves.Concat(moveChecker(grandChild.gameObject, moves));
                }
            }
            else
            {
                moves.Concat(moveChecker(child.gameObject, moves));
            }
        }

        return moves;
    }
    public List<Moves> moveChecker(GameObject unit, List<Moves> moves)
    {
        if (unit.transform.tag == "Pawn" || unit.transform.tag == "UnmovedPawn")
        {
            Moves pieceMoves = new Moves();
            pieceMoves.piece = unit;
            pieceMoves.positions = pawn.pathFinder(unit);
            moves.Add(pieceMoves);
        }
        else if (unit.transform.tag == "Rook")
        {
            Moves pieceMoves = new Moves();
            pieceMoves.piece = unit;
            pieceMoves.positions = rook.pathFinder(unit);
            moves.Add(pieceMoves);
        }
        else if (unit.transform.tag == "Knight")
        {
            Moves pieceMoves = new Moves();
            pieceMoves.piece = unit.gameObject;
            pieceMoves.positions = knight.pathFinder(unit);
            moves.Add(pieceMoves);
        }
        else if (unit.transform.tag == "Bishop")
        {
            Moves pieceMoves = new Moves();
            pieceMoves.piece = unit.gameObject;
            pieceMoves.positions = bishop.pathFinder(unit);
            moves.Add(pieceMoves);
        }
        else if (unit.transform.tag == "Queen")
        {
            Moves pieceMoves = new Moves();
            pieceMoves.piece = unit.gameObject;
            pieceMoves.positions = queen.pathFinder(unit);
            moves.Add(pieceMoves);
        }
        else if (unit.transform.tag == "King")
        {
            Moves pieceMoves = new Moves();
            pieceMoves.piece = unit.gameObject;
            pieceMoves.positions = king.pathFinder(unit);
            moves.Add(pieceMoves);
        }
        return moves;
    }
}
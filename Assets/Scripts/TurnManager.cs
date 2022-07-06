using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

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
    public CheckChecker check;
    public bool turnWhite = true;
    public bool do3D;
    public float duration;
    public int turnNum = 0;
    GameObject selected;
    List<GameObject> moveCubes = new List<GameObject>();
    List<GameObject> attackCubes = new List<GameObject>();
    public Moves allMoves = new Moves();
    string[] allTags = new string[6] { "Pawn", "Rook", "Knight", "Bishop", "Queen", "King" };

    void Start()
    {

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
            foreach (var item in moveCubes)
            {
                Destroy(item);
            }
            foreach (var item in attackCubes)
            {
                Destroy(item);
            }
            layerMask = ~layerMask;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                if ((hit.transform.tag == "MoveTarget" || hit.transform.tag == "AttackTarget") && selected != null)
                {
                    nextTurn(selected, hit.transform.gameObject);
                    selected = null;
                }
                else if (allTags.Contains(hit.transform.tag) && selected == null)
                {
                    selected = hit.transform.gameObject;
                    Vector3 offset = new Vector3(0, 0.5f, 0);
                    allMoves = moveChecker(selected);
                    if (check.inCheck)
                    {
                        List<Vector3> checkPositions = new List<Vector3>();
                        List<GameObject> checkAttacks = new List<GameObject>();
                        foreach (var i in check.checkMoves)
                        {
                            checkAttacks.AddRange(i.attacks);
                            checkPositions.AddRange(i.positions);
                        }
                        allMoves.attacks.RemoveAll(move => !checkAttacks.Contains(move));
                        allMoves.positions.RemoveAll(move => !checkPositions.Contains(move));
                    }
                    foreach (var pos in allMoves.positions)
                    {
                        GameObject cube = Instantiate(moveTarget, pos + offset, Quaternion.identity);
                        moveCubes.Add(cube);
                    }
                    foreach (var piece in allMoves.attacks)
                    {
                        GameObject cube = Instantiate(attackTarget, piece.transform.position + offset, Quaternion.identity);
                        cube.GetComponent<Target>().target = piece;
                        attackCubes.Add(cube);
                    }
                }
                else
                {
                    selected = null;
                }
            }
            else
            {
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
                    moves.Add(moveChecker(grandChild.gameObject, doKing));
                }
            }
            else
            {
                moves.Add(moveChecker(child.gameObject, doKing));
            }
        }
        return moves;
    }
    Moves moveChecker(GameObject unit, bool doKing = true)
    {
        Moves moves = new Moves();
        if (unit.transform.tag == "Pawn")
        {
            Moves pieceMoves = new Moves();
            if (doKing)
            {
                pieceMoves = pawn.pathFinder(unit, do3D);
            }
            else
            {
                pieceMoves = pawn.justAttackPaths(unit, do3D);
            }
            moves = pieceMoves;
        }
        else if (unit.transform.tag == "Rook")
        {
            Moves pieceMoves = rook.pathFinder(unit);
            moves = pieceMoves;
        }
        else if (unit.transform.tag == "Knight")
        {
            Moves pieceMoves = knight.pathFinder(unit);
            moves = pieceMoves;
        }
        else if (unit.transform.tag == "Bishop")
        {
            Moves pieceMoves = bishop.pathFinder(unit);
            moves = pieceMoves;
        }
        else if (unit.transform.tag == "Queen")
        {
            Moves pieceMoves = queen.pathFinder(unit);
            moves = pieceMoves;
        }
        else if (unit.transform.tag == "King" && doKing)
        {
            Moves pieceMoves = king.pathFinder(unit);
            moves = pieceMoves;
        }
        return moves;
    }
    void nextTurn(GameObject movedPiece, GameObject target)
    {
        StartCoroutine(MovePiece(movedPiece,target));
        if (target.transform.tag == "AttackTarget")
        {
            GameObject attacked = target.transform.gameObject.GetComponent<Target>().target;
            ParticleSystemRenderer renderer = attacked.GetComponent<ParticleSystemRenderer>(); 
            renderer.mesh = movedPiece.GetComponent<MeshFilter>().mesh;
            renderer.material = movedPiece.GetComponent<MeshRenderer>().material;
            attacked.GetComponent<ParticleSystem>().Play();
        }
        allMoves = new Moves();
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
    IEnumerator MovePiece(GameObject mover, GameObject movee)
    {
        check.enabled = false;
        float time = 0;
        Vector3 targetPos = movee.transform.position - new Vector3(0,0.5f,0);
        while (time < duration)
        {
            float t = time / duration;
            t = t * t * (3f - 2f * t);
            mover.transform.position = Vector3.Lerp(mover.transform.position, targetPos, t);
            time += Time.deltaTime;
            yield return null;
        }
        mover.transform.position = targetPos;
        check.enabled = true;
    }
}
public class Moves
{
    public GameObject piece;
    public List<GameObject> attacks = new List<GameObject>();
    public List<Vector3> attackMoves = new List<Vector3>();
    public List<Vector3> positions = new List<Vector3>();
    public Moves()
    {
        foreach(var i in attacks)
        {
            attackMoves.Add(i.transform.position);
        }
    }
}
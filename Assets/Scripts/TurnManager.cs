using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class TurnManager : MonoBehaviour
{
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
                if ((hit.transform.CompareTag("MoveTarget") || hit.transform.CompareTag("AttackTarget")) && selected != null)
                {
                    nextTurn(selected, hit.transform.gameObject);
                    selected = null;
                }
                else if (allTags.Contains(hit.transform.tag) && selected == null)
                {
                    selected = hit.transform.gameObject;
                    Vector3 offset = new Vector3(0, 0.5f, 0);
                    allMoves = selected.GetComponent<IPiece>().PathFinder();
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
    public static List<Moves> AllMovesFinder(GameObject team, bool doKing = true)
    {
        List<Moves> moves = new List<Moves>();
        List<GameObject> allPieces = new List<GameObject>();
        foreach (Transform child in team.transform)
        {
            if (child.transform.childCount > 0)
            {
                foreach (Transform grandChild in child.transform)
                {
                    allPieces.Add(grandChild.gameObject);
                }
            }
            else
            {
                allPieces.Add(child.gameObject);
            }
        }
        foreach (var i in allPieces)
        {
            if (doKing && !i.gameObject.CompareTag("Pawn") && !i.gameObject.CompareTag("King"))
            {
                moves.Add(i.gameObject.GetComponent<IPiece>().PathFinder());
            }
            else
            {
                if (i.gameObject.CompareTag("Pawn"))
                {
                    moves.Add(i.gameObject.GetComponent<Pawn>().JustAttackPaths());
                }
            }
        }
        return moves;
    }
    void nextTurn(GameObject movedPiece, GameObject target)
    {
        StartCoroutine(MovePiece(movedPiece,target.transform.position));
        if (target.transform.CompareTag("AttackTarget"))
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
    public IEnumerator MovePiece(GameObject mover, Vector3 movee)
    {
        check.enabled = false;
        float time = 0;
        Vector3 targetPos = movee - new Vector3(0,0.5f,0);
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
public interface IPiece
{
    Moves PathFinder();
}
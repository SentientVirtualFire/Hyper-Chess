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
    public GameObject emptyTarget;
    public bool turnWhite = true;
    public bool do3D;
    public bool do4D;
    public bool do5D;
    public float duration;
    public int turnNum = 0;


    GameObject selected;
    List<GameObject> moveCubes = new List<GameObject>();
    List<GameObject> attackCubes = new List<GameObject>();
    List<GameObject> emptyCubes = new List<GameObject>();

    public List<Moves> allMoves = new List<Moves>();
    string[] allTags = new string[6] { "Pawn", "Rook", "Knight", "Bishop", "Queen", "King" };

    void Start()
    {
        for (int x = 0; x <= 7; x++)
        {
            for (float y = 0; y <= 12.25f; y+=1.75f)
            {
                for (int z = 0; z <= 7; z++)
                {
                    if(Physics.OverlapSphere(new Vector3(x, y + 0.5f, z), 0.01f).Length == 0)
                    { 
                        emptyCubes.Add(Instantiate(emptyTarget, new Vector3(x, y+0.5f, z), Quaternion.identity));
                    }
                }
            }
        }
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
                    allMoves = moveChecker(hit.transform.gameObject);
                    selected = hit.transform.gameObject;
                    Vector3 offset = new Vector3(0,0.5f,0);
                    foreach(var unit in allMoves)
                    {
                        foreach (var pos in unit.positions)
                        {
                            GameObject cube = Instantiate(moveTarget, pos+offset, Quaternion.identity);
                            moveCubes.Add(cube);
                        }
                        foreach (var piece in unit.attacks)
                        {
                            GameObject cube = Instantiate(attackTarget, piece.transform.position+offset, Quaternion.identity);
                            cube.GetComponent<Target>().target = piece;
                            attackCubes.Add(cube);
                        }
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
                    moves.Concat(moveChecker(grandChild.gameObject, doKing));
                }
            }
            else
            {
                moves.Concat(moveChecker(child.gameObject, doKing));
            }
        }
        return moves;
    }
    List<Moves> moveChecker(GameObject unit, bool doKing = true)
    {
        List<Moves> moves = new List<Moves>();
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
        StartCoroutine(MovePiece(movedPiece,target));
        if (target.transform.tag == "AttackTarget")
        {
            GameObject attacked = target.transform.gameObject.GetComponent<Target>().target;
            ParticleSystemRenderer renderer = attacked.GetComponent<ParticleSystemRenderer>(); 
            renderer.mesh = movedPiece.GetComponent<MeshFilter>().mesh;
            renderer.material = movedPiece.GetComponent<MeshRenderer>().material;
            attacked.GetComponent<ParticleSystem>().Play();
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
        foreach (var item in emptyCubes)
        {
            Destroy(item);
        }
        for (int x = 0; x <= 7; x++)
        {
            for (float y = 0; y <= 12.25f; y += 1.75f)
            {
                for (int z = 0; z <= 7; z++)
                {
                    if (Physics.OverlapSphere(new Vector3(x, y + 0.5f, z), 0.01f).Length == 0)
                    {
                        emptyCubes.Add(Instantiate(emptyTarget, new Vector3(x, y + 0.5f, z), Quaternion.identity));
                    }
                }
            }
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
    }
}
public class Moves
{
    public GameObject piece;
    public List<GameObject> attacks;
    public List<Vector3> positions = new List<Vector3>();
    public int turn;
}
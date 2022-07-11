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
    public List<Board> boards = new List<Board>();
    public bool turnWhite = true;
    public bool do3D;
    public float pieceSpeed;
    public int turnNum = 0;
    public Moves allMoves = new Moves();
    public List<GameObject> pieceRef = new List<GameObject>();
    GameObject selected;
    List<GameObject> moveCubes = new List<GameObject>();
    List<GameObject> attackCubes = new List<GameObject>();
    string[] allTags = new string[6] { "Pawn", "Rook", "Knight", "Bishop", "Queen", "King" };
    bool moving = false;

    void Start()
    {
        boards.Add(new Board(this));
    }
    void Update()
    {
        if(!moving)
        {
            if (check.inCheck && boards[turnNum - 1].isCheck)
            {
                LoadBoard(boards[turnNum - 1]);
                boards.RemoveAt(turnNum + 1);
            }
            else if (Input.GetMouseButtonDown(0))
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
            if (Input.GetKeyDown(KeyCode.K))
            {
                LoadBoard(boards[turnNum - 1]);
            }
        }
    }
    public static List<Moves> AllMovesFinder(GameObject team, bool doKing = true)
    {
        List<Moves> moves = new List<Moves>();
        foreach (var i in GetAllPieces())
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
            ParticleSystem ps = attacked.GetComponent<ParticleSystem>();
            var main = ps.main;
            main.duration = pieceSpeed;
            ps.Play();
        }
        allMoves = new Moves();
        foreach (var item in moveCubes.Concat(attackCubes))
        {
            Destroy(item);
        }
        turnWhite = !turnWhite;
        turnNum++;
    }
    public IEnumerator MovePiece(GameObject mover, Vector3 movee)
    {
        moving = true;
        float time = 0;
        Vector3 targetPos = movee - new Vector3(0,0.5f,0);
        while (time < pieceSpeed)
        {
            float t = time / pieceSpeed;
            t = t * t * (3f - 2f * t);
            mover.transform.position = Vector3.Lerp(mover.transform.position, targetPos, t);
            time += Time.deltaTime;
            yield return null;
        }
        mover.transform.position = targetPos;
        check.CheckCheck();
        boards.Add(new Board(this));
        moving = false;
    }
    public void LoadBoard(Board board)
    {
        foreach (var i in GetAllPieces())
        {
            Destroy(i);
        }
        foreach (var piece in board.pieces)
        {
            foreach (var i in pieceRef)
            {
                if (i.CompareTag(piece.tag) && i.layer == piece.layer)
                {
                    Instantiate(i, piece.position, Quaternion.identity);
                }
            }
        }
        turnNum = board.turnNum;
        turnWhite = board.turnWhite;
    }
    public static List<GameObject> GetAllPieces()
    {
        List<GameObject> allPieces = new List<GameObject>();
        string[] allTags = new string[6] { "Pawn", "Rook", "Knight", "Bishop", "Queen", "King" };
        foreach (var i in allTags)
        {
            allPieces.AddRange(GameObject.FindGameObjectsWithTag(i));
        }
        return allPieces;
    }
}
public interface IPiece
{
    Moves PathFinder();
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class TurnManager : MonoBehaviour
{
    [Range(0.001f, 1)]
    public float opaqueAlpha;
    [Range(0.001f, 1)]
    public float transpAlpha;
    [Range(0.01f, 5)]
    public float pieceSpeed;
    public bool turnWhite = true;
    public bool isCheck;
    public int turnNum = 0;
    public string checkedTeam;
    public bool? checkedIsWhite;
    public GameObject killed;
    [SerializeField]
    public List<Board> boards = new List<Board>();
    public static Vector3 boardBound1 = new Vector3(0, 0, 0);
    public static Vector3 boardBound2 = new Vector3(7,14,7);
    public GameObject whites;
    public GameObject blacks;
    public Camera mainCamera;
    public GameUI ui;
    public GameObject moveTarget;
    public GameObject attackTarget;
    public Transform higherBoards;
    public List<GameObject> pieceRef = new List<GameObject>();
    public static string[] allTags = new string[6] { "Pawn", "Rook", "Knight", "Bishop", "Queen", "King" };
    GameObject selected;
    Moves allMoves = new Moves();
    List<GameObject> moveCubes = new List<GameObject>();
    List<GameObject> attackCubes = new List<GameObject>();
    List<Transform> tiles = new List<Transform>();
    bool moving = false;
    (List<Moves>, List<Moves>, List<Moves>) owo = (new List<Moves>(), new List<Moves>(), new List<Moves>());
    public List<Moves> owo1;
    public List<Moves> owo2;
    public List<Moves> owo3;
    void Start()
    {
        ui.gameObject.SetActive(true);
        boards.Add(new Board(this));
        foreach (Transform light in higherBoards.GetChild(0)) tiles.Add(light);
        foreach (Transform dark in higherBoards.GetChild(1)) tiles.Add(dark);
        ShowHideHigherTiles();
    }
    void Update()
    {
        owo1 = owo.Item1;
        owo2 = owo.Item2;
        owo3 = owo.Item3;
        if (!moving && ui.isPlaying)
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
                foreach (var item in moveCubes.Concat(attackCubes))
                {
                    Destroy(item);
                }
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                {
                    if ((hit.transform.CompareTag("MoveTarget") || hit.transform.CompareTag("AttackTarget")) && selected != null)
                    {
                        StartCoroutine(MovePiece(selected, hit.transform.position - new Vector3(0, 0.5f, 0), hit.transform.gameObject));
                        foreach (var item in moveCubes.Concat(attackCubes))
                        {
                            Destroy(item);
                        }
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
        }
    }
    IEnumerator MovePiece(GameObject mover, Vector3 target, GameObject movee = null, bool justMove = false)
    {
        moving = true;
        if (!justMove && movee != null)
        {
            if (movee.transform.CompareTag("AttackTarget"))
            {
                GameObject attacked = movee.transform.gameObject.GetComponent<Target>().target;
                killed = attacked;
                ParticleSystemRenderer renderer = attacked.GetComponent<ParticleSystemRenderer>();
                renderer.mesh = mover.GetComponent<MeshFilter>().mesh;
                renderer.material = mover.GetComponent<MeshRenderer>().material;
                ParticleSystem ps = attacked.GetComponent<ParticleSystem>();
                var main = ps.main;
                main.duration = pieceSpeed;
                ps.Play();
            }
            else
            {
                killed = null;
            }
        }
        float time = 0;
        Vector3 targetPos = target;
        while (time < pieceSpeed)
        {
            float t = time / pieceSpeed;
            t = t * t * (3f - 2f * t);
            mover.transform.position = Vector3.Lerp(mover.transform.position, targetPos, t);
            time += Time.deltaTime;
            yield return null;
        }
        mover.transform.position = targetPos;
        ShowHideHigherTiles();
        if (!justMove)
        {
            turnWhite = !turnWhite;
            turnNum++;
            owo.Item1 = AllMovesFinder();
            if (CheckCheck())
            {
                if (CheckCheckMate())
                {
                    print("check mate");
                }
                else
                {
                    print("not check mate");
                }
                if (checkedIsWhite == turnWhite)
                {
                    isCheck = true;
                    if (boards[turnNum - 1].isCheck)
                    {
                        LoadBoard(boards[turnNum - 1], this);
                    }
                }
                else if (checkedIsWhite == !turnWhite)
                {
                    LoadBoard(boards[turnNum - 1], this);
                }
            }
            else
            {
                isCheck = false;
            }
            boards.Add(new Board(this));
            if(boards.Count - 1 > turnNum)
            {
                boards.RemoveRange(turnNum + 1, boards.Count - 1 - turnNum);
            }
        }
        if(mover.CompareTag("Pawn") && (mover.transform.position.z == 0 || mover.transform.position.z == 7))
        {
            ui.SetUpPromotion(mover);
        }
        moving = false;
    }
    public bool CheckCheck(int? teamLayer = null)
    {
        foreach (var i in AllMovesFinder(teamLayer:teamLayer))
        {
            foreach (var j in i.attacks)
            {
                if (j.CompareTag("King"))
                {
                    if(teamLayer == null)
                    { 
                        if (i.piece.layer == 6)
                        {
                            checkedTeam = "BLACK";
                            checkedIsWhite = false;
                        }
                        else
                        {
                            checkedTeam = "WHITE";
                            checkedIsWhite = true;
                        }
                    }
                    return true;
                }
                else if(teamLayer == null)
                {
                    checkedIsWhite = null;
                }
            }
        }
        return false;
    }
    public bool CheckCheckMate()
    {
        int n = 0;
        foreach (var i in AllMovesFinder(teamLayer:checkedIsWhite.Value ? 6 : 7))
        {
            Vector3 origin = i.piece.transform.position;
            foreach (var j in i.positions)
            {
                i.piece.transform.position = j;
                n++;
                owo.Item2 = AllMovesFinder(teamLayer: checkedIsWhite.Value ? 7 : 6);
                if (!CheckCheck(checkedIsWhite.Value ? 7 : 6))
                {
                    i.piece.transform.position = origin;
                    print(n);
                    return false;
                }
                i.piece.transform.position = origin;
            }
            foreach (var j in i.attacks)
            {
                i.piece.transform.position = j.transform.position;
                n++;
                j.SetActive(false);
                owo.Item3 = AllMovesFinder(teamLayer: checkedIsWhite.Value ? 7 : 6);
                if (!CheckCheck(checkedIsWhite.Value ? 7 : 6))
                {
                    i.piece.transform.position = origin;
                    j.SetActive(true);
                    print(n);
                    return false;
                }
                i.piece.transform.position = origin;
                j.SetActive(true);
            }

        }
        print(n);
        return true;
    }
    void ShowHideHigherTiles()
    {
        List<Vector3> allPieces = GetAllPieces().Select(piece => piece.transform.position).ToList();
        foreach (Transform tile in tiles)
        {
            Color newColour = tile.gameObject.GetComponent<MeshRenderer>().material.color;
            if (allPieces.Contains(tile.position))
            {
                newColour.a = opaqueAlpha;
            }
            else
            {
                newColour.a = transpAlpha;
            }
            tile.gameObject.GetComponent<MeshRenderer>().material.color = newColour;
        }
    }
    public static void LoadBoard(Board board, TurnManager tm)
    {
        List<GameObject> pieceList = TurnManager.GetAllPieces();
        List<GameObject> pieces = board.pieces.Select(piece => piece.gObject).ToList();
        foreach (var i in board.pieces)
        {
            if (pieceList.Contains(i.gObject))
            {
                tm.StartCoroutine(tm.MovePiece(i.gObject, i.position, justMove:true));
            }
        }
        if (tm.killed != null)
        {
            GameObject.Instantiate(tm.killed, tm.killed.transform.position, Quaternion.identity);
        }
        GameObject p = pieces.Find(piece => !pieceList.Contains(piece));
        tm.turnNum = board.turnNum;
        tm.turnWhite = board.turnWhite;
    }
    public static List<Moves> AllMovesFinder(bool doKing = true, int? teamLayer = null)
    {
        List<Moves> moves = new List<Moves>();
        bool layerCheck = false;
        if(teamLayer.HasValue)
        {
            layerCheck = true;
        }
        foreach (var i in GetAllPieces())
        {
            if (i.layer == teamLayer || !layerCheck)
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
        }
        return moves;
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
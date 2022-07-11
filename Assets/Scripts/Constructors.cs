using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constructors : MonoBehaviour
{

}
public class Moves
{
    public GameObject piece;
    public List<GameObject> attacks = new List<GameObject>();
    public List<Vector3> attackMoves = new List<Vector3>();
    public List<Vector3> positions = new List<Vector3>();
    public Moves()
    {
        foreach (var i in attacks)
        {
            attackMoves.Add(i.transform.position);
        }
    }
}
public class Board
{
    public List<Piece> pieces = new List<Piece>();
    public int turnNum;
    public bool isCheck;
    public bool turnWhite;
    public Board(TurnManager tm)
    {
        foreach (var i in TurnManager.GetAllPieces())
        {
            pieces.Add(new Piece(i));
        }
        turnNum = tm.turnNum;
        isCheck = tm.check.inCheck;
        turnWhite = tm.turnWhite;
    }
}
public class Piece
{
    public string tag;
    public int layer;
    public Vector3 position;
    public Piece(GameObject obj)
    {
        tag = obj.tag;
        layer = obj.layer;
        position = obj.transform.position;
    }
}
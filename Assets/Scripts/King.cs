using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : MonoBehaviour, IPiece
{
    (int, float, int)[] offsets = { (1, 0, 0), (1, 0, -1), (0, 0, -1), (-1, 0, -1), (-1, 0, 0), (-1, 0, 1), (0, 0, 1), (1, 0, 1), (1, 1, 0), (-1, 1, 0), (0, 1, 1), (0, 1, -1), (0, 1, 0), (1, -1, 0), (-1, -1, 0), (0, -1, 1), (0, -1, -1), (0, -1, 0) };  
    public GameObject whites;
    public GameObject blacks;
    void Start()
    {
        whites = GameObject.Find("Whites");
        blacks = GameObject.Find("Blacks");
    }
    public Moves PathFinder()
    {
        Vector3 pos = gameObject.transform.position;
        List<Vector3> moves = new List<Vector3>();
        List<GameObject> attackMoves = new List<GameObject>();
        foreach (var i in offsets)
        {
            Vector3 newPos= gameObject.transform.position + new Vector3(i.Item1, i.Item2 * 2, i.Item3);
            if (newPos.x >= 0 && newPos.x <=7 && newPos.z >= 0 && newPos.z <= 7 && newPos.y >= 0 && newPos.y <= 14)
            {
                Collider[] intersecting = Physics.OverlapSphere(newPos, 0.01f);
                if (intersecting.Length > 0)
                {
                    if (intersecting[0].gameObject.layer != gameObject.layer)
                    {
                        attackMoves.Add(intersecting[0].gameObject);
                    }
                    else
                    {
                        moves.Remove(newPos);
                    }
                }
                else
                {
                    moves.Add(newPos);
                }
            }
        }
        if(gameObject.layer == 6)
        {
           foreach(var i in TurnManager.AllMovesFinder(false))
           {
                moves.RemoveAll(move => i.attackMoves.Contains(move));
                moves.RemoveAll(move => i.positions.Contains(move));
           }
        }
        else
        {
            foreach (var i in TurnManager.AllMovesFinder(false))
            {
                moves.RemoveAll(move => i.attackMoves.Contains(move));
                moves.RemoveAll(move => i.positions.Contains(move));
            }
        }
        Moves allMoves = new Moves() { piece = gameObject, positions = moves, attacks = attackMoves };
        return allMoves;
    }
}
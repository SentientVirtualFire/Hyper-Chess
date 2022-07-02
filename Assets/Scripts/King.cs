using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : MonoBehaviour
{
    (int, float, int)[] offsets = { (1, 0, 0), (1, 0, -1), (0, 0, -1), (-1, 0, -1), (-1, 0, 0), (-1, 0, 1), (0, 0, 1), (1, 0, 1), (1, 1, 0), (-1, 1, 0), (0, 1, 1), (0, 1, -1), (0, 1, 0), (1, -1, 0), (-1, -1, 0), (0, -1, 1), (0, -1, -1), (0, -1, 0) };  
    public TurnManager turnManager;
    List<Vector3> opposingMoves;
    public Moves pathFinder(GameObject king)
    {
        Vector3 pos = king.transform.position;
        List<Vector3> moves = new List<Vector3>();
        List<GameObject> attackMoves = new List<GameObject>();
        foreach (var i in offsets)
        {
            Vector3 newPos= king.transform.position + new Vector3(i.Item1, i.Item2 * 1.75f, i.Item3);
            if (newPos.x >= 0 && newPos.x <=7 && newPos.z >= 0 && newPos.z <= 7 && newPos.y >= 0 && newPos.y <= 12.25f)
            {
                Collider[] intersecting = Physics.OverlapSphere(newPos, 0.01f);
                if (intersecting.Length > 0)
                {
                    if (intersecting[0].gameObject.layer != king.layer)
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
        if(king.layer == 6)
        {
           foreach(var i in turnManager.allMovesFinder(turnManager.blacks, false))
           {
                foreach (var j in i.positions)
                {
                    moves.RemoveAll(move => move == j);
                }
           }
        }
        else
        {
            foreach (var i in turnManager.allMovesFinder(turnManager.whites, false))
            {
                foreach (var j in i.positions)
                {
                    moves.RemoveAll(move => move == j);
                }
            }
        }
        Moves allMoves = new Moves();
        allMoves.piece = king;
        allMoves.positions = moves;
        allMoves.attacks = attackMoves;
        return allMoves;
    }
}
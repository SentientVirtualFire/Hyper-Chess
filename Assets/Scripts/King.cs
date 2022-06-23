using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : MonoBehaviour
{
    (int, float, int)[] offsets = { (1, 0, 0), (1, 0, -1), (0, 0, -1), (-1, 0, -1), (-1, 0, 0), (-1, 0, 1), (0, 0, 1), (1, 0, 1) };  
    public TurnManager turnManager;
    List<Vector3> opposingMoves;
    public List<Vector3> pathFinder(GameObject king)
    {
        Vector3 pos = king.transform.position;
        List<Vector3> moves = new List<Vector3>();
        foreach(var i in offsets)
        {
            Vector3 newPos= king.transform.position + new Vector3(i.Item1, i.Item2, i.Item3);
            if (newPos.x >= 0 && newPos.x <=7 && newPos.z >= 0 && newPos.z <= 7)
            {
                Collider[] intersecting = Physics.OverlapSphere(newPos + new Vector3(0, 0.5f, 0), 0.01f);
                if (intersecting.Length > 0)
                {
                    if (intersecting[0].gameObject.layer != king.layer)
                    {
                        moves.Add(newPos);
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
                    foreach (var k in moves)
                    {
                        if (j == k)
                        {
                            moves.Remove(k);
                        }
                    }
                }
            }
        }
        return moves;
    }
}

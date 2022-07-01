using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    (int,float,int)[] offsets = {(1, 0, 2),(2 , 0, 1),(2, 0, -1),(1, 0, -2),(-1, 0, -2),(-2, 0, -1),(-2, 0, 1),(-1, 0, 2),(0, 2, 1), (0, 2, -1), (1, 2, 0), (-1, 2, 0), (-2, 1, 0), (2, 1, 0), (0, 1, 2), (0, 1, -2), (0, -2, 1), (0, -2, -1), (1, -2, 0), (-1, -2, 0), (-2, -1, 0), (2, -1, 0), (0, -1, 2), (0, -1, -2) };
    public Moves pathFinder(GameObject knight)
    {
        Vector3 pos = knight.transform.position;
        List<Vector3> moves = new List<Vector3>();
        List<GameObject> attackMoves = new List<GameObject>();
        foreach (var i in offsets)
        {
            Vector3 newPos = pos + new Vector3(i.Item1, i.Item2 * 1.75f, i.Item3);
            if (newPos.x <= 7 && newPos.x >= 0 && newPos.z <= 7 && newPos.z >= 0 && newPos.y <= 12.25 && newPos.y >= 0)
            {
                Collider[] intersecting = Physics.OverlapSphere(newPos + new Vector3(0,0.5f,0), 0.01f);
                if (intersecting.Length > 0)
                {
                    if(intersecting[0].gameObject.layer != knight.layer)
                    {
                        attackMoves.Add(intersecting[0].gameObject);
                    }
                }
                else {
                    moves.Add(newPos);
                }
            }
        }
        Moves allMoves = new Moves();
        allMoves.piece = knight;
        allMoves.positions = moves;
        allMoves.attacks = attackMoves;
        return allMoves;
    }
}
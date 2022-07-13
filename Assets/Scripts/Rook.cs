using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Rook : MonoBehaviour, IPiece
{
    Vector3 pos = new Vector3();
    List<Vector3> moves = new List<Vector3>();
    List<GameObject> attackMoves = new List<GameObject>();
    public Moves PathFinder()
    {
        pos = gameObject.transform.position;
        moves = new List<Vector3>();
        attackMoves = new List<GameObject>();
        for (var x = pos.x - 1; x >= 0; x--)
        {
            if (LoopContent(new Vector3(x, pos.y, pos.z)))
            {
                break;
            }
        }
        for (var x = pos.x + 1; x <= 7; x++)
        {
            if (LoopContent(new Vector3(x, pos.y, pos.z)))
            {
                break;
            }
        }
        for (var z = pos.z - 1; z >= 0; z--)
        {
            if (LoopContent(new Vector3(pos.x, pos.y, z)))
            {
                break;
            }
        }
        for (var z = pos.z + 1; z <= 7; z++)
        {
            if (LoopContent(new Vector3(pos.x, pos.y, z)))
            {
                break;
            }
        }
        for (var y = pos.y + 2; y <= 14; y+=2)
        {
            if (LoopContent(new Vector3(pos.x, y, pos.z)))
            {
                break;
            }
        }
        for (var y = pos.y - 2; y >= 0; y -= 2)
        {
            if (LoopContent(new Vector3(pos.x, y, pos.z)))
            {
                break;
            }
        }
        Moves allMoves = new Moves() { piece = gameObject, positions = moves, attacks = attackMoves };
        return allMoves;
    }
    bool LoopContent(Vector3 vector)
    {
        Collider[] intersecting = Physics.OverlapSphere(vector, 0.01f);
        if (intersecting.Length == 0)
        {
            moves.Add(vector);
        }
        else
        {
            if (intersecting[0].gameObject.layer != gameObject.layer)
            {
                attackMoves.Add(intersecting[0].gameObject);
                return true;
            }
            else
            {
                return true;
            }
        }
        return false;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : MonoBehaviour, IPiece
{
    Vector3 pos = new Vector3();
    List<Vector3> moves = new List<Vector3>();
    List<GameObject> attackMoves = new List<GameObject>();
    public Moves PathFinder()
    {
        pos = gameObject.transform.position;
        moves = new List<Vector3>();
        attackMoves = new List<GameObject>();
        for (var (x, z) = (pos.x + 1, pos.z + 1); x <= 7 && z <= 7; x++, z++)
        {
            if (LoopContent(new Vector3(x, pos.y, z)))
            {
                break;
            }
        }
        for (var (x, z) = (pos.x + 1, pos.z - 1); x <= 7 && z >= 0; x++, z--)
        {
            if (LoopContent(new Vector3(x, pos.y, z)))
            {
                break;
            }
        }
        for (var (x, z) = (pos.x - 1, pos.z - 1); x >= 0 && z >= 0; x--, z--)
        {
            if (LoopContent(new Vector3(x, pos.y, z)))
            {
                break;
            }
        }
        for (var (x, z) = (pos.x - 1, pos.z + 1); x >= 0 && z <= 7; x--, z++)
        {
            if (LoopContent(new Vector3(x, pos.y, z)))
            {
                break;
            }
        }
        for (var (y, z) = (pos.y - 2, pos.z + 1); y >= 0 && z <= 7; y-=2, z++)
        {
            if (LoopContent(new Vector3(pos.x, y, z)))
            {
                break;
            }
        }
        for (var (y, z) = (pos.y - 2, pos.z - 1); y >= 0 && z >= 0; y-=2, z--)
        {
            if (LoopContent(new Vector3(pos.x, y, z)))
            {
                break;
            }
        }
        for (var (y, x) = (pos.y - 2, pos.x - 1); y >= 0 && x >= 0; y -= 2, x--)
        {
            if (LoopContent(new Vector3(x, y, pos.z)))
            {
                break;
            }
        }
        for (var (y, x) = (pos.y - 2, pos.x + 1); y >= 0 && x <= 7; y -= 2, x++)
        {
            if (LoopContent(new Vector3(x, y, pos.z)))
            {
                break;
            }
        }
        for (var (y, x) = (pos.y + 2, pos.x + 1); y <= 14 && x <= 7; y += 2, x++)
        {
            if (LoopContent(new Vector3(x, y, pos.z)))
            {
                break;
            }
        }
        for (var (y, x) = (pos.y + 2, pos.x - 1); y <= 14 && x >= 0; y += 2, x--)
        {
            if (LoopContent(new Vector3(x, y, pos.z)))
            {
                break;
            }
        }
        for (var (y, z) = (pos.y + 2, pos.z + 1); y <= 14 && z <= 7; y += 2, z++)
        {
            if (LoopContent(new Vector3(pos.x, y, z)))
            {
                break;
            }
        }
        for (var (y, z) = (pos.y + 2, pos.z - 1); y <= 14 && z >= 0; y += 2, z--)
        {
            if (LoopContent(new Vector3(pos.x, y, z)))
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
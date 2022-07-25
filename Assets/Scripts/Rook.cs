using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Rook : MonoBehaviour, IPiece
{
    Vector3 pos = new Vector3();
    List<Vector3> moves = new List<Vector3>();
    List<Vector3> kingPath = new List<Vector3>();
    List<GameObject> attacks = new List<GameObject>();
    bool kpFound = false;
    public Moves PathFinder()
    {
        pos = gameObject.transform.position;
        kpFound = false;
        moves = new List<Vector3>();
        kingPath = new List<Vector3>();
        attacks = new List<GameObject>();
        for (var x = pos.x - 1; x >= 0; x--)
        {
            if (LoopContent(new Vector3(x, pos.y, pos.z)))
            {
                break;
            }
        }
        if(!kpFound)
        { 
            kingPath.Clear();
        }
        for (var x = pos.x + 1; x <= 7; x++)
        {
            if (LoopContent(new Vector3(x, pos.y, pos.z)))
            {
                break;
            }
        }
        if (!kpFound)
        {
            kingPath.Clear();
        }
        for (var z = pos.z - 1; z >= 0; z--)
        {
            if (LoopContent(new Vector3(pos.x, pos.y, z)))
            {
                break;
            }
        }
        if (!kpFound)
        {
            kingPath.Clear();
        }
        for (var z = pos.z + 1; z <= 7; z++)
        {
            if (LoopContent(new Vector3(pos.x, pos.y, z)))
            {
                break;
            }
        }
        if (!kpFound)
        {
            kingPath.Clear();
        }
        for (var y = pos.y + 2; y <= 14; y+=2)
        {
            if (LoopContent(new Vector3(pos.x, y, pos.z)))
            {
                break;
            }
        }
        if (!kpFound)
        {
            kingPath.Clear();
        }
        for (var y = pos.y - 2; y >= 0; y -= 2)
        {
            if (LoopContent(new Vector3(pos.x, y, pos.z)))
            {
                break;
            }
        }
        if (!kpFound)
        {
            kingPath.Clear();
        }
        Moves allMoves = new Moves() { piece = gameObject, positions = moves, attacks = attacks, kingPath = kingPath };
        return allMoves;
    }
    bool LoopContent(Vector3 vector)
    {
        Collider[] intersecting = Physics.OverlapSphere(vector, 0.01f);
        if (intersecting.Length == 0)
        {
            moves.Add(vector);
            if (!kpFound)
            {
                kingPath.Add(vector);
            }
        }
        else
        {
            if (intersecting[0].gameObject.layer != gameObject.layer)
            {
                if(intersecting[0].gameObject.CompareTag("King"))
                {
                    kpFound = true;
                }
                attacks.Add(intersecting[0].gameObject);
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Rook : MonoBehaviour, IPiece
{
    public Moves PathFinder()
    {
        Vector3 pos = gameObject.transform.position;
        List<Vector3> moves = new List<Vector3>();
        List<GameObject> attackMoves = new List<GameObject>();
        for (var x = pos.x - 1; x >= 0; x--)
        {
            Collider[] intersecting = Physics.OverlapSphere(new Vector3(x, pos.y, pos.z), 0.01f);
            if (intersecting.Length == 0)
            {
                moves.Add(new Vector3(x, pos.y, pos.z));
            }
            else
            {
                if (intersecting[0].gameObject.layer != gameObject.layer)
                {
                    attackMoves.Add(intersecting[0].gameObject);
                    break;
                }
                else
                {
                    break;
                }
            }
        }
        for (var x = pos.x + 1; x <= 7; x++)
        {
            Collider[] intersecting = Physics.OverlapSphere(new Vector3(x, pos.y, pos.z), 0.01f);
            if (intersecting.Length == 0)
            {
                moves.Add(new Vector3(x, pos.y, pos.z));
            }
            else
            {
                if (intersecting[0].gameObject.layer != gameObject.layer)
                {
                    attackMoves.Add(intersecting[0].gameObject);
                    break;
                }
                else
                {
                    break;
                }
            }
        }
        for (var z = pos.z - 1; z >= 0; z--)
        {
            Collider[] intersecting = Physics.OverlapSphere(new Vector3(pos.x, pos.y, z), 0.01f);
            if (intersecting.Length == 0)
            {
                moves.Add(new Vector3(pos.x, pos.y, z));
            }
            else
            {
                if (intersecting[0].gameObject.layer != gameObject.layer)
                {
                    attackMoves.Add(intersecting[0].gameObject);
                    break;
                }
                else
                {
                    break;
                }
            }
        }
        for (var z = pos.z + 1; z <= 7; z++)
        {
            Collider[] intersecting = Physics.OverlapSphere(new Vector3(pos.x, pos.y, z), 0.01f);
            if (intersecting.Length == 0)
            {
                moves.Add(new Vector3(pos.x, pos.y, z));
            }
            else
            {
                if (intersecting[0].gameObject.layer != gameObject.layer)
                {
                    attackMoves.Add(intersecting[0].gameObject);
                    break;
                }
                else
                {
                    break;
                }
            }
        }
        for (var y = pos.y + 2; y <= 14; y+=2)
        {
            Collider[] intersecting = Physics.OverlapSphere(new Vector3(pos.x, y, pos.z), 0.01f);
            if (intersecting.Length == 0)
            {
                moves.Add(new Vector3(pos.x, y, pos.z));
            }
            else
            {
                if (intersecting[0].gameObject.layer != gameObject.layer)
                {
                    attackMoves.Add(intersecting[0].gameObject);
                    break;
                }
                else
                {
                    break;
                }
            }
        }
        for (var y = pos.y - 2; y >= 0; y -= 2)
        {
            Collider[] intersecting = Physics.OverlapSphere(new Vector3(pos.x, y, pos.z), 0.01f);
            if (intersecting.Length == 0)
            {
                moves.Add(new Vector3(pos.x, y, pos.z));
            }
            else
            {
                if (intersecting[0].gameObject.layer != gameObject.layer)
                {
                    attackMoves.Add(intersecting[0].gameObject);
                    break;
                }
                else
                {
                    break;
                }
            }
        }
        Moves allMoves = new Moves();
        allMoves.piece = gameObject;
        allMoves.positions = moves;
        allMoves.attacks = attackMoves;
        return allMoves;
    }
}
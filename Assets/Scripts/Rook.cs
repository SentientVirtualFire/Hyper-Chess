using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Rook : MonoBehaviour
{
    public Moves pathFinder(GameObject rook)
    {
        Vector3 pos = rook.transform.position;
        List<Vector3> moves = new List<Vector3>();
        List<GameObject> attackMoves = new List<GameObject>();
        for (var x = pos.x - 1; x >= 0; x--)
        {
            Collider[] intersecting = Physics.OverlapSphere(new Vector3(x, 0.5f, pos.z), 0.01f);
            if (intersecting.Length == 0)
            {
                moves.Add(new Vector3(x, 0, pos.z));
            }
            else
            {
                if (intersecting[0].gameObject.layer != rook.layer)
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
            Collider[] intersecting = Physics.OverlapSphere(new Vector3(x, 0.5f, pos.z), 0.01f);
            if (intersecting.Length == 0)
            {
                moves.Add(new Vector3(x, 0, pos.z));
            }
            else
            {
                if (intersecting[0].gameObject.layer != rook.layer)
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
            Collider[] intersecting = Physics.OverlapSphere(new Vector3(pos.x, 0.5f, z), 0.01f);
            if (intersecting.Length == 0)
            {
                moves.Add(new Vector3(pos.x, 0, z));
            }
            else
            {
                if (intersecting[0].gameObject.layer != rook.layer)
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
            Collider[] intersecting = Physics.OverlapSphere(new Vector3(pos.x, 0.5f, z), 0.01f);
            if (intersecting.Length == 0)
            {
                moves.Add(new Vector3(pos.x, 0, z));
            }
            else
            {
                if (intersecting[0].gameObject.layer != rook.layer)
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
        allMoves.piece = rook;
        allMoves.positions = moves;
        allMoves.attacks = attackMoves;
        return allMoves;
    }
}
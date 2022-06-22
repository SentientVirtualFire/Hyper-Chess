using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : MonoBehaviour
{
    public List<Vector3> pathFinder(GameObject bishop)
    {
        Vector3 pos = bishop.transform.position;
        List<Vector3> moves = new List<Vector3>();
        for (var (x, z) = (pos.x + 1, pos.z + 1); x <= 7 && z <= 7; x++, z++)
        {
            Collider[] intersecting = Physics.OverlapSphere(new Vector3(x, 0.5f, z), 0.01f);
            if (intersecting.Length == 0)
            {
                moves.Add(new Vector3(x, 0, z));
            }
            else
            {
                if (intersecting[0].gameObject.layer != bishop.layer)
                {
                    moves.Add(new Vector3(x, 0, z));
                    break;
                }
                else
                {
                    break;
                }
            }
        }
        for (var (x, z) = (pos.x + 1, pos.z - 1); x <= 7 && z >= 0; x++, z--)
        {
            Collider[] intersecting = Physics.OverlapSphere(new Vector3(x, 0.5f, z), 0.01f);
            if (intersecting.Length == 0)
            {
                moves.Add(new Vector3(x, 0, z));
            }
            else
            {
                if (intersecting[0].gameObject.layer != bishop.layer)
                {
                    moves.Add(new Vector3(x, 0, z));
                    break;
                }
                else
                {
                    break;
                }
            }
        }
        for (var (x, z) = (pos.x - 1, pos.z - 1); x >= 0 && z >= 0; x--, z--)
        {
            Collider[] intersecting = Physics.OverlapSphere(new Vector3(x, 0.5f, z), 0.01f);
            if (intersecting.Length == 0)
            {
                moves.Add(new Vector3(x, 0, z));
            }
            else
            {
                if (intersecting[0].gameObject.layer != bishop.layer)
                {
                    moves.Add(new Vector3(x, 0, z));
                    break;
                }
                else
                {
                    break;
                }
            }
        }
        for (var (x, z) = (pos.x - 1, pos.z + 1); x >= 0 && z <= 7; x--, z++)
        {
            Collider[] intersecting = Physics.OverlapSphere(new Vector3(x, 0.5f, z), 0.01f);
            if (intersecting.Length == 0)
            {
                moves.Add(new Vector3(x, 0, z));
            }
            else
            {
                if (intersecting[0].gameObject.layer != bishop.layer)
                {
                    moves.Add(new Vector3(x, 0, z));
                    break;
                }
                else
                {
                    break;
                }
            }
        }
        return moves;
    }
}
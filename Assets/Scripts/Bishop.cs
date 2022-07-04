using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : MonoBehaviour
{
    public Moves pathFinder(GameObject bishop)
    {
        Vector3 pos = bishop.transform.position;
        List<Vector3> moves = new List<Vector3>();
        List<GameObject> attackMoves = new List<GameObject>();
        for (var (x, z) = (pos.x + 1, pos.z + 1); x <= 7 && z <= 7; x++, z++)
        {
            Collider[] intersecting = Physics.OverlapSphere(new Vector3(x, pos.y, z), 0.01f);
            if (intersecting.Length == 0)
            {
                moves.Add(new Vector3(x, pos.y, z));
            }
            else
            {
                if (intersecting[0].gameObject.layer != bishop.layer)
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
        for (var (x, z) = (pos.x + 1, pos.z - 1); x <= 7 && z >= 0; x++, z--)
        {
            Collider[] intersecting = Physics.OverlapSphere(new Vector3(x, pos.y, z), 0.01f);
            if (intersecting.Length == 0)
            {
                moves.Add(new Vector3(x, pos.y, z));
            }
            else
            {
                if (intersecting[0].gameObject.layer != bishop.layer)
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
        for (var (x, z) = (pos.x - 1, pos.z - 1); x >= 0 && z >= 0; x--, z--)
        {
            Collider[] intersecting = Physics.OverlapSphere(new Vector3(x, pos.y, z), 0.01f);
            if (intersecting.Length == 0)
            {
                moves.Add(new Vector3(x, pos.y, z));
            }
            else
            {
                if (intersecting[0].gameObject.layer != bishop.layer)
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
        for (var (x, z) = (pos.x - 1, pos.z + 1); x >= 0 && z <= 7; x--, z++)
        {
            Collider[] intersecting = Physics.OverlapSphere(new Vector3(x, pos.y, z), 0.01f);
            if (intersecting.Length == 0)
            {
                moves.Add(new Vector3(x, pos.y, z));
            }
            else
            {
                if (intersecting[0].gameObject.layer != bishop.layer)
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
        for (var (y, z) = (pos.y - 2, pos.z + 1); y >= 0 && z <= 7; y-=2, z++)
        {
            Collider[] intersecting = Physics.OverlapSphere(new Vector3(pos.x, y, z), 0.01f);
            if (intersecting.Length == 0)
            {
                moves.Add(new Vector3(pos.x, y, z));
            }
            else
            {
                if (intersecting[0].gameObject.layer != bishop.layer)
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
        for (var (y, z) = (pos.y - 2, pos.z - 1); y >= 0 && z >= 0; y-=2, z--)
        {
            Collider[] intersecting = Physics.OverlapSphere(new Vector3(pos.x, y, z), 0.01f);
            if (intersecting.Length == 0)
            {
                moves.Add(new Vector3(pos.x, y, z));
            }
            else
            {
                if (intersecting[0].gameObject.layer != bishop.layer)
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
        for (var (y, x) = (pos.y - 2, pos.x - 1); y >= 0 && x >= 0; y -= 2, x--)
        {
            Collider[] intersecting = Physics.OverlapSphere(new Vector3(x, y, pos.z), 0.01f);
            if (intersecting.Length == 0)
            {
                moves.Add(new Vector3(x, y, pos.z));
            }
            else
            {
                if (intersecting[0].gameObject.layer != bishop.layer)
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
        for (var (y, x) = (pos.y - 2, pos.x + 1); y >= 0 && x <= 7; y -= 2, x++)
        {
            Collider[] intersecting = Physics.OverlapSphere(new Vector3(x, y, pos.z), 0.01f);
            if (intersecting.Length == 0)
            {
                moves.Add(new Vector3(x, y, pos.z));
            }
            else
            {
                if (intersecting[0].gameObject.layer != bishop.layer)
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
        for (var (y, x) = (pos.y + 2, pos.x + 1); y <= 14 && x <= 7; y += 2, x++)
        {
            Collider[] intersecting = Physics.OverlapSphere(new Vector3(x, y, pos.z), 0.01f);
            if (intersecting.Length == 0)
            {
                moves.Add(new Vector3(x, y, pos.z));
            }
            else
            {
                if (intersecting[0].gameObject.layer != bishop.layer)
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
        for (var (y, x) = (pos.y + 2, pos.x - 1); y <= 14 && x >= 0; y += 2, x--)
        {
            Collider[] intersecting = Physics.OverlapSphere(new Vector3(x, y, pos.z), 0.01f);
            if (intersecting.Length == 0)
            {
                moves.Add(new Vector3(x, y, pos.z));
            }
            else
            {
                if (intersecting[0].gameObject.layer != bishop.layer)
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
        for (var (y, z) = (pos.y + 2, pos.z + 1); y <= 14 && z <= 7; y += 2, z++)
        {
            Collider[] intersecting = Physics.OverlapSphere(new Vector3(pos.x, y, z), 0.01f);
            if (intersecting.Length == 0)
            {
                moves.Add(new Vector3(pos.x, y, z));
            }
            else
            {
                if (intersecting[0].gameObject.layer != bishop.layer)
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
        for (var (y, z) = (pos.y + 2, pos.z - 1); y <= 14 && z >= 0; y += 2, z--)
        {
            Collider[] intersecting = Physics.OverlapSphere(new Vector3(pos.x, y, z), 0.01f);
            if (intersecting.Length == 0)
            {
                moves.Add(new Vector3(pos.x, y, z));
            }
            else
            {
                if (intersecting[0].gameObject.layer != bishop.layer)
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
        allMoves.piece = bishop;
        allMoves.positions = moves;
        allMoves.attacks = attackMoves;
        return allMoves;
    }
}
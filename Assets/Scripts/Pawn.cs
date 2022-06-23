using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    public List<Vector3> pathFinder(GameObject pawn)
    { 
        Vector3 pos = pawn.transform.position;
        List<Vector3> moves = new List<Vector3>();
        if (pawn.layer == 6)
        {
            Collider[] intersectingA = Physics.OverlapSphere(new Vector3(pos.x, 0.5f, pos.z + 1), 0.01f);
            Collider[] intersectingB = Physics.OverlapSphere(new Vector3(pos.x, 0.5f, pos.z + 2), 0.01f);
            if (pawn.tag == "UnmovedPawn" && intersectingB.Length == 0)
            {
                moves.Add(new Vector3(pos.x, 0, pos.z + 1));
                moves.Add(new Vector3(pos.x, 0, pos.z + 2));
                if((pawn.layer == 6 && pos.z != 1) || (pawn.layer == 7 && pos.z != -6))
                { 
                    pawn.tag = "Pawn";
                }
            }
            else if(intersectingA.Length == 0)
            {
                moves.Add(new Vector3(pos.x, 0, pos.z + 1));
            }
            if (pos.z != 7)
            {
                Collider[] intersecting1 = Physics.OverlapSphere(new Vector3(pos.x + 1, 0.5f, pos.z + 1), 0.01f);
                if (intersecting1.Length > 0)
                {
                    if (intersecting1[0].gameObject.layer != pawn.layer)
                        moves.Add(new Vector3(pos.x + 1, 0.5f, pos.z + 1));
                }
                Collider[] intersecting2 = Physics.OverlapSphere(new Vector3(pos.x - 1, 0.5f, pos.z + 1), 0.01f);
                if (intersecting2.Length > 0)
                {
                    if (intersecting2[0].gameObject.layer != pawn.layer)
                        moves.Add(new Vector3(pos.x - 1, 0.5f, pos.z + 1));
                }
            }
        }
        else
        {
            Collider[] intersectingA = Physics.OverlapSphere(new Vector3(pos.x, 0.5f, pos.z - 1), 0.01f);
            Collider[] intersectingB = Physics.OverlapSphere(new Vector3(pos.x, 0.5f, pos.z - 2), 0.01f);
            if (pawn.tag == "UnmovedPawn" && intersectingB.Length == 0)
            {
                moves.Add(new Vector3(pos.x, 0, pos.z - 1));
                moves.Add(new Vector3(pos.x, 0, pos.z - 2));
                if ((pawn.layer == 6 && pos.z != 1) || (pawn.layer == 7 && pos.z != -6))
                {
                    pawn.tag = "Pawn";
                }
            }
            else if (intersectingA.Length == 0)
            {
                moves.Add(new Vector3(pos.x, 0, pos.z - 1));
            }
            if (pos.z != 0)
            {
                Collider[] intersecting1 = Physics.OverlapSphere(new Vector3(pos.x + 1, 0.5f, pos.z - 1), 0.01f);
                if (intersecting1.Length > 0)
                {
                    if (intersecting1[0].gameObject.layer != pawn.layer)
                        moves.Add(new Vector3(pos.x + 1, 0.5f, pos.z - 1));
                }
                Collider[] intersecting2 = Physics.OverlapSphere(new Vector3(pos.x - 1, 0.5f, pos.z - 1), 0.01f);
                if (intersecting2.Length > 0)
                {
                    if (intersecting2[0].gameObject.layer != pawn.layer)
                        moves.Add(new Vector3(pos.x - 1, 0.5f, pos.z - 1));
                }
            }
        }     
        return moves;
    }
    public List<Vector3> justAttackPaths(GameObject pawn)
    {
        Vector3 pos = pawn.transform.position;
        List<Vector3> moves = new List<Vector3>();
        if (pawn.layer == 6)
        {
            if (pos.z != 7)
            {
                Collider[] intersecting1 = Physics.OverlapSphere(new Vector3(pos.x + 1, 0.5f, pos.z + 1), 0.01f);
                if (intersecting1.Length > 0)
                {
                    if (intersecting1[0].gameObject.layer != pawn.layer)
                        moves.Add(new Vector3(pos.x + 1, 0.5f, pos.z + 1));
                }
                Collider[] intersecting2 = Physics.OverlapSphere(new Vector3(pos.x - 1, 0.5f, pos.z + 1), 0.01f);
                if (intersecting2.Length > 0)
                {
                    if (intersecting2[0].gameObject.layer != pawn.layer)
                        moves.Add(new Vector3(pos.x - 1, 0.5f, pos.z + 1));
                }
            }
        }
        else
        {
            if (pos.z != 0)
            {
                Collider[] intersecting1 = Physics.OverlapSphere(new Vector3(pos.x + 1, 0.5f, pos.z - 1), 0.01f);
                if (intersecting1.Length > 0)
                {
                    if (intersecting1[0].gameObject.layer != pawn.layer)
                        moves.Add(new Vector3(pos.x + 1, 0.5f, pos.z - 1));
                }
                Collider[] intersecting2 = Physics.OverlapSphere(new Vector3(pos.x - 1, 0.5f, pos.z - 1), 0.01f);
                if (intersecting2.Length > 0)
                {
                    if (intersecting2[0].gameObject.layer != pawn.layer)
                        moves.Add(new Vector3(pos.x - 1, 0.5f, pos.z - 1));
                }
            }
        }
        return moves;
    }
}
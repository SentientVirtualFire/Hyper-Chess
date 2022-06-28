using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    public Moves pathFinder(GameObject pawn)
    { 
        Vector3 pos = pawn.transform.position;
        List<Vector3> moves = new List<Vector3>();
        List<GameObject> attackMoves = new List<GameObject>();
        if (pawn.layer == 6)
        {
            Collider[] intersectingA = Physics.OverlapSphere(new Vector3(pos.x, 0.5f, pos.z + 1), 0.01f);
            Collider[] intersectingB = Physics.OverlapSphere(new Vector3(pos.x, 0.5f, pos.z + 2), 0.01f);
            if (pos.z == 1 && intersectingB.Length == 0)
            {
                moves.Add(new Vector3(pos.x, 0, pos.z + 1));
                moves.Add(new Vector3(pos.x, 0, pos.z + 2));
            }
            else if (intersectingA.Length == 0)
            {
                moves.Add(new Vector3(pos.x, 0, pos.z + 1));
            }
            {
                Collider[] intersecting1 = Physics.OverlapSphere(new Vector3(pos.x + 1, 0.5f, pos.z + 1), 0.01f);
                if (intersecting1.Length > 0)
                {
                    if (intersecting1[0].gameObject.layer != pawn.layer)
                    {
                        attackMoves.Add(intersecting1[0].gameObject);
                    }
                }
                Collider[] intersecting2 = Physics.OverlapSphere(new Vector3(pos.x - 1, 0.5f, pos.z + 1), 0.01f);
                if (intersecting2.Length > 0)
                {
                    if (intersecting2[0].gameObject.layer != pawn.layer)
                    {
                        attackMoves.Add(intersecting2[0].gameObject);
                    }
                }
            }
        }
        else
        {
            Collider[] intersectingA = Physics.OverlapSphere(new Vector3(pos.x, 0.5f, pos.z - 1), 0.01f);
            Collider[] intersectingB = Physics.OverlapSphere(new Vector3(pos.x, 0.5f, pos.z - 2), 0.01f);
            if (pos.z == 6 && intersectingB.Length == 0)
            {
                moves.Add(new Vector3(pos.x, 0, pos.z - 1));
                moves.Add(new Vector3(pos.x, 0, pos.z - 2));
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
                        attackMoves.Add(intersecting1[0].gameObject);
                }
                Collider[] intersecting2 = Physics.OverlapSphere(new Vector3(pos.x - 1, 0.5f, pos.z - 1), 0.01f);
                if (intersecting2.Length > 0)
                {
                    if (intersecting2[0].gameObject.layer != pawn.layer)
                        attackMoves.Add(intersecting2[0].gameObject);
                }
            }
        }
        Moves allMoves = new Moves();
        allMoves.piece = pawn;
        allMoves.positions = moves;
        allMoves.attacks = attackMoves;
        return allMoves;
    }
    public Moves justAttackPaths(GameObject pawn)
    {
        Vector3 pos = pawn.transform.position;
        List<GameObject> attackMoves = new List<GameObject>();
        if (pawn.layer == 6)
        {
            if (pos.z != 7)
            {
                Collider[] intersecting1 = Physics.OverlapSphere(new Vector3(pos.x + 1, 0.5f, pos.z + 1), 0.01f);
                if (intersecting1.Length > 0)
                {
                    if (intersecting1[0].gameObject.layer != pawn.layer)
                        attackMoves.Add(intersecting1[0].gameObject);
                }
                Collider[] intersecting2 = Physics.OverlapSphere(new Vector3(pos.x - 1, 0.5f, pos.z + 1), 0.01f);
                if (intersecting2.Length > 0)
                {
                    if (intersecting2[0].gameObject.layer != pawn.layer)
                        attackMoves.Add(intersecting2[0].gameObject);
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
                        attackMoves.Add(intersecting1[0].gameObject);
                }
                Collider[] intersecting2 = Physics.OverlapSphere(new Vector3(pos.x - 1, 0.5f, pos.z - 1), 0.01f);
                if (intersecting2.Length > 0)
                {
                    if (intersecting2[0].gameObject.layer != pawn.layer)
                        attackMoves.Add(intersecting2[0].gameObject);
                }
            }
        }
        Moves allMoves = new Moves();
        allMoves.piece = pawn;
        allMoves.attacks = attackMoves;
        return allMoves;
    }
}
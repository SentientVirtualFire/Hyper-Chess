using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour, IPiece
{
    bool unMoved = true;
    public Moves PathFinder()
    {
        Vector3 pos = gameObject.transform.position;
        List<Vector3> moves = new List<Vector3>();
        if(gameObject.layer == 6)
        {
            if(pos.z != 1 || pos.y != 0)
            {
                unMoved = false;
            }
        }
        else
        {
            if (pos.z != 6 || pos.y != 0)
            {
                unMoved = false;
            }
        }
        if (gameObject.layer == 6)
        {
            Collider[] intersectingA = Physics.OverlapSphere(new Vector3(pos.x, pos.y, pos.z + 1), 0.01f);
            Collider[] intersectingB = Physics.OverlapSphere(new Vector3(pos.x, pos.y, pos.z + 2), 0.01f);
            if (unMoved && intersectingA.Length == 0 && intersectingB.Length == 0 && pos.z <=5)
            {
                moves.Add(new Vector3(pos.x, pos.y, pos.z + 1));
                moves.Add(new Vector3(pos.x, pos.y, pos.z + 2));
            }
            else if (intersectingA.Length == 0 && pos.z <= 6)
            {
                moves.Add(new Vector3(pos.x, pos.y, pos.z + 1));
            }
        }
        else
        {
            Collider[] intersectingA = Physics.OverlapSphere(new Vector3(pos.x, pos.y, pos.z - 1), 0.01f);
            Collider[] intersectingB = Physics.OverlapSphere(new Vector3(pos.x, pos.y, pos.z - 2), 0.01f);
            if (unMoved && intersectingA.Length == 0 && intersectingB.Length == 0 && pos.z >= 2)
            {
                moves.Add(new Vector3(pos.x, pos.y, pos.z - 1));
                moves.Add(new Vector3(pos.x, pos.y, pos.z - 2));
            }
            else if (intersectingA.Length == 0 && pos.z >= 1)
            {
                moves.Add(new Vector3(pos.x, pos.y, pos.z - 1));
            }
        }
        Collider[] intersectingC = Physics.OverlapSphere(new Vector3(pos.x, pos.y + 2, pos.z), 0.01f);
        Collider[] intersectingD = Physics.OverlapSphere(new Vector3(pos.x, pos.y + 4, pos.z), 0.01f);
        if (unMoved && intersectingD.Length == 0 && pos.y <= 10)
        {
            moves.Add(new Vector3(pos.x, pos.y + 2, pos.z));
            moves.Add(new Vector3(pos.x, pos.y + 4, pos.z));
        }
        else if (intersectingC.Length == 0 && pos.y <= 12)
        {
            moves.Add(new Vector3(pos.x, pos.y + 2, pos.z));
        }
        return new Moves() { piece = gameObject, positions = moves, attacks = JustAttackPaths().attacks };
    }
    public Moves JustAttackPaths()
    {
        Vector3 pos = gameObject.transform.position;
        List<GameObject> attackMoves = new List<GameObject>();
        if (gameObject.layer == 6)
        {
            if (pos.z != 7)
            {
                Collider[] intersecting1 = Physics.OverlapSphere(new Vector3(pos.x + 1, pos.y, pos.z + 1), 0.01f);
                if (intersecting1.Length > 0)
                {
                    if (intersecting1[0].gameObject.layer != gameObject.layer)
                        attackMoves.Add(intersecting1[0].gameObject);
                }
                Collider[] intersecting2 = Physics.OverlapSphere(new Vector3(pos.x - 1, pos.y, pos.z + 1), 0.01f);
                if (intersecting2.Length > 0)
                {
                    if (intersecting2[0].gameObject.layer != gameObject.layer)
                        attackMoves.Add(intersecting2[0].gameObject);
                }
            }
        }
        else
        {
            if (pos.z != 0)
            {
                Collider[] intersecting1 = Physics.OverlapSphere(new Vector3(pos.x + 1, pos.y, pos.z - 1), 0.01f);
                if (intersecting1.Length > 0)
                {
                    if (intersecting1[0].gameObject.layer != gameObject.layer)
                        attackMoves.Add(intersecting1[0].gameObject);
                }
                Collider[] intersecting2 = Physics.OverlapSphere(new Vector3(pos.x - 1, pos.y, pos.z - 1), 0.01f);
                if (intersecting2.Length > 0)
                {
                    if (intersecting2[0].gameObject.layer != gameObject.layer)
                        attackMoves.Add(intersecting2[0].gameObject);
                }
            }
        }
        if (pos.y <= 12)
        {
            Collider[] intersecting1 = Physics.OverlapSphere(new Vector3(pos.x + 1, pos.y + 2, pos.z), 0.01f);
            Collider[] intersecting2 = Physics.OverlapSphere(new Vector3(pos.x - 1, pos.y + 2, pos.z), 0.01f);
            Collider[] intersecting3 = Physics.OverlapSphere(new Vector3(pos.x, pos.y + 2, pos.z + 1), 0.01f);
            Collider[] intersecting4 = Physics.OverlapSphere(new Vector3(pos.x, pos.y + 2, pos.z - 1), 0.01f);
            if (intersecting1.Length > 0 && pos.x <= 6)
            {
                if (intersecting1[0].gameObject.layer != gameObject.layer)
                    attackMoves.Add(intersecting1[0].gameObject);
            }
            if (intersecting2.Length > 0 && pos.x >= 0)
            {
                if (intersecting2[0].gameObject.layer != gameObject.layer)
                    attackMoves.Add(intersecting2[0].gameObject);
            }
            if (intersecting3.Length > 0 && pos.z <= 6)
            {
                if (intersecting3[0].gameObject.layer != gameObject.layer)
                    attackMoves.Add(intersecting3[0].gameObject);
            }
            if (intersecting4.Length > 0 && pos.z >= 0)
            {
                if (intersecting4[0].gameObject.layer != gameObject.layer)
                    attackMoves.Add(intersecting4[0].gameObject);
            }
        }
        Moves allMoves = new Moves() { piece = gameObject, attacks = attackMoves };
        return allMoves;
    }
}
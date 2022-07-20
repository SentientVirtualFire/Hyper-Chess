using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pawn : MonoBehaviour, IPiece
{
    bool unMoved = true;
    TurnManager tm;
    GameUI ui;
    void Start()
    {
        tm = GameObject.Find("GameManager").GetComponent<TurnManager>();
        ui = GameObject.Find("UI").GetComponent<GameUI>();
    }
    void Update()
    {
       
    }
    public Moves PathFinder()
    {
        Vector3 pos = gameObject.transform.position;
        List<Vector3> moves = new List<Vector3>();
        int forward = 1;
        if(gameObject.layer == 6)
        {
            if(pos.z != 1 || pos.y != 0)
            {
                unMoved = false;
            }
            forward = 1;
        }
        else
        {
            if (pos.z != 6 || pos.y != 0)
            {
                unMoved = false;
            }
            forward = -1;
        }
        Collider[] intersectingA = Physics.OverlapSphere(new Vector3(pos.x, pos.y, pos.z + forward), 0.01f);
        Collider[] intersectingB = Physics.OverlapSphere(new Vector3(pos.x, pos.y, pos.z + 2*forward), 0.01f);
        if (unMoved && intersectingA.Length == 0 && intersectingB.Length == 0)
        {
            moves.Add(new Vector3(pos.x, pos.y, pos.z + forward));
            moves.Add(new Vector3(pos.x, pos.y, pos.z + 2* forward));
        }
        else if (intersectingA.Length == 0 && pos.z <= 6 && pos.z >= 1)
        {
            moves.Add(new Vector3(pos.x, pos.y, pos.z + forward));
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
        int forward = 1;
        if (gameObject.layer == 6)
        {
            forward = 1;
        }
        else
        {
            forward = -1;
        }
        if (pos.z != 7 && pos.z != 0)
        {
            Collider[] intersecting1 = Physics.OverlapSphere(new Vector3(pos.x + 1, pos.y, pos.z + forward), 0.01f);
            if (intersecting1.Length > 0)
            {
                if (intersecting1[0].gameObject.layer != gameObject.layer)
                    attackMoves.Add(intersecting1[0].gameObject);
            }
            Collider[] intersecting2 = Physics.OverlapSphere(new Vector3(pos.x - 1, pos.y, pos.z + forward), 0.01f);
            if (intersecting2.Length > 0)
            {
                if (intersecting2[0].gameObject.layer != gameObject.layer)
                    attackMoves.Add(intersecting2[0].gameObject);
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

    public void PromoteSelf(string promoteTo, Button button)
    {
        foreach (var i in tm.pieceRef)
        {
            if(i.layer == gameObject.layer && i.CompareTag(promoteTo))
            {
                GameObject.Instantiate(i, transform.position, Quaternion.identity);
                button.onClick.RemoveAllListeners();
                button.gameObject.transform.parent.gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }

}
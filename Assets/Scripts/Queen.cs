using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Queen : MonoBehaviour, IPiece
{
    public Moves PathFinder()
    {
        Moves bishopPos = GetComponent<Bishop>().PathFinder();
        Moves rookPos = GetComponent<Rook>().PathFinder();
        Moves allMoves = new Moves();
        allMoves.piece = gameObject;
        allMoves.positions = bishopPos.positions.Concat(rookPos.positions).ToList();
        allMoves.attacks = bishopPos.attacks.Concat(rookPos.attacks).ToList();
        return allMoves;
    }
}
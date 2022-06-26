using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Queen : MonoBehaviour
{
    public Rook rook;
    public Bishop bishop;
    public Moves pathFinder(GameObject queen)
    {
        Moves bishopPos = bishop.pathFinder(queen);
        Moves rookPos = rook.pathFinder(queen);
        Moves allMoves = new Moves();
        allMoves.piece = queen;
        allMoves.positions = bishopPos.positions.Concat(rookPos.positions).ToList();
        allMoves.attacks = bishopPos.attacks.Concat(rookPos.attacks).ToList();
        return allMoves;
    }
}
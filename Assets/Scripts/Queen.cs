using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Queen : MonoBehaviour
{
    public Rook rook;
    public Bishop bishop;
    public List<Vector3> pathFinder(GameObject queen)
    {
        return rook.pathFinder(queen).Concat(bishop.pathFinder(queen)).ToList();
    }
}
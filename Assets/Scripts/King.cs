using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : MonoBehaviour
{
    (int, float, int)[] offsets = { (1, 0, 0), (1, 0, -1), (0, 0, -1), (-1, 0, -1), (-1, 0, 0), (-1, 0, 1), (0, 0, 1), (1, 0, 1) };
    public List<Vector3> pathFinder(GameObject king/*, Transform opponents*/)
    {
        Vector3 pos = king.transform.position;
        List<Vector3> moves = new List<Vector3>();
        GameObject[] allUnits = new GameObject[31];

        




        return moves;
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Queen : MonoBehaviour, IPiece
{
    public Moves PathFinder()
    {
        return new Moves() { piece = gameObject, positions = GetComponent<Bishop>().PathFinder().positions.Concat(GetComponent<Rook>().PathFinder().positions).ToList(), attacks = GetComponent<Bishop>().PathFinder().attacks.Concat(GetComponent<Rook>().PathFinder().attacks).ToList(), kingPath = GetComponent<Bishop>().PathFinder().kingPath.Concat(GetComponent<Rook>().PathFinder().kingPath).ToList() };
    }
}
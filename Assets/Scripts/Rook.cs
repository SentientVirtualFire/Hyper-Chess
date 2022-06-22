using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Rook : MonoBehaviour
{
    public List<Vector3> pathFinder(GameObject rook)
    {
        Vector3 pos = rook.transform.position;
        List<Vector3> movesX = new List<Vector3>();
        List<Vector3> movesZ = new List<Vector3>();
        for (int x = 0; x <= 7; x++)
        {
            Collider[] intersecting = Physics.OverlapSphere(new Vector3(x, 0.5f, pos.z), 0.01f);
            if (intersecting.Length == 0)
            {
                movesX.Add(new Vector3(x, 0, pos.z));
            }
            else
            {
                GameObject problemChild = intersecting[0].gameObject;
                if (problemChild.layer != rook.gameObject.layer)
                {
                    for (float i = pos.x + 1; i <= 7; i++)
                    {
                        Collider[] intersecting2 = Physics.OverlapSphere(new Vector3(i, 0.5f, pos.z), 0.01f);
                        if (intersecting2.Length > 0)
                        {
                            if (problemChild.layer != rook.gameObject.layer)
                            {
                                movesX.Add(new Vector3(i, 0, pos.z));
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            movesX.Add(new Vector3(i, 0, pos.z));
                        }
                    }
                    for (float i = pos.x - 1; i >= 0; i--)
                    {
                        Collider[] intersecting2 = Physics.OverlapSphere(new Vector3(i, 0.5f, pos.z), 0.01f);
                        if (intersecting2.Length > 0)
                        {
                            if (problemChild.layer != rook.gameObject.layer)
                            {
                                movesX.Add(new Vector3(i, 0, pos.z));
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            movesX.Add(new Vector3(i, 0, pos.z));
                        }

                    }
                    break;
                }
                else
                {
                    if (problemChild.transform.position.x < rook.transform.position.x)
                    {
                        movesX.Clear();
                    }
                    if (problemChild.transform.position.x > rook.transform.position.x)
                    {
                        break;
                    }
                }
            }
        }
        for (int z = 0; z <= 7; z++)
        {
            Collider[] intersecting = Physics.OverlapSphere(new Vector3(pos.x, 0.5f, z), 0.01f);
            if (intersecting.Length == 0)
            {
                movesZ.Add(new Vector3(pos.x, 0, z));
            }
            else
            {
                GameObject problemChild = intersecting[0].gameObject;
                if (problemChild.layer != rook.gameObject.layer)
                {
                    for (float i = pos.z + 1; i <= 7; i++)
                    {
                        Collider[] intersecting2 = Physics.OverlapSphere(new Vector3(pos.x, 0.5f, i), 0.01f);
                        if (intersecting2.Length > 0)
                        {
                            problemChild = intersecting2[0].gameObject;
                            if (problemChild.layer != rook.gameObject.layer)
                            {
                                movesZ.Add(new Vector3(pos.x, 0, i));
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            movesZ.Add(new Vector3(pos.x, 0, i));
                        }
                    }
                    for (float i = pos.z -1 ; i >= 0; i--)
                    {
                        Collider[] intersecting2 = Physics.OverlapSphere(new Vector3(pos.x, 0.5f, i), 0.01f);
                        if (intersecting2.Length > 0)
                        {
                            problemChild = intersecting2[0].gameObject;
                            if (problemChild.layer != rook.gameObject.layer)
                            {
                                movesZ.Add(new Vector3(pos.x, 0, i));
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            movesZ.Add(new Vector3(pos.x, 0, i));
                        }
                    }
                    break;
                }
                else
                {
                    if (problemChild.transform.position.z < rook.transform.position.z)
                    {
                        movesZ.Clear();
                    }
                    if (problemChild.transform.position.z > rook.transform.position.z)
                    {
                        break;
                    }
                }
            }
        }
        List<Vector3> moves = movesX.Concat(movesZ).ToList();
        return moves;
    }
}
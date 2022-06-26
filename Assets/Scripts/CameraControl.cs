using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public TurnManager turnManager;
    public float speed = 100.0f;
    public float sensitivity;
    public int moveLimit;
    public int returnPoint;
    public bool prevTurn = false;
    public bool movingIn = false;
    public bool changingSide = false;
    void Update()
    {
        float dist = Vector3.Distance(new Vector3(3.5f, 0, 3.5f), transform.position);
        if (turnManager.turnWhite != prevTurn)
        {
            if (turnManager.turnWhite)
            {
                transform.position = new Vector3(3.5f, 7, 0);
                transform.rotation = Quaternion.Euler(60, 0, 0);
            }
            else
            {
                transform.position = new Vector3(3.5f, 7, 7);
                transform.rotation = Quaternion.Euler(120, 360, 180);
            }
            changingSide = true;
            prevTurn = turnManager.turnWhite;
        }
        transform.LookAt(new Vector3(3.5f, 0, 3.5f));
        Vector3 p = new Vector3();
        if (Input.GetKey(KeyCode.UpArrow))
        {
            p += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            p += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            p += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            p += new Vector3(1, 0, 0);
        }
        if (dist < moveLimit && !movingIn)
        {
            Vector3 newPosition = transform.position;
            if (p.sqrMagnitude > 0)
            {
                transform.Translate(p * speed * Time.deltaTime);
                newPosition.x = transform.position.x;
                newPosition.z = transform.position.z;
                transform.position = newPosition;
            }
        }
        else
        {
            movingIn = true;
            if (turnManager.turnWhite)
            {
                p = new Vector3(3.5f, 7, 0) - transform.position;
            }
            else
            {
                p = new Vector3(3.5f, 7, 7f) - transform.position;
            }
            Vector3 newPosition = transform.position;
            transform.Translate(p * speed/3 * Time.deltaTime);
            newPosition.x = transform.position.x;
            newPosition.z = transform.position.z;
            transform.position = newPosition;
        }
        if (dist < returnPoint)
        {
            movingIn = false;
        }
    }
}
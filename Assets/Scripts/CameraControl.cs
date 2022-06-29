using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public TurnManager turnManager;
    public float speed;
    public Transform whitePOV;
    public Transform blackPOV;
    public float speedX;
    public float speedY;
    public float sensitivityX = 2F;
    public float sensitivityY = 2F;
    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -90F;
    public float maximumY = 90F;
    public int maxDist;
    float rotationY = -60f;
    Vector3 NewPosition;


    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
        else if (Input.GetMouseButton(2))
        {
            if (Vector3.Distance(new Vector3(3.5f, 0, 3.5f), transform.position) < maxDist)
            {
                Cursor.lockState = CursorLockMode.Locked;
                NewPosition = new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y")) * Time.deltaTime * speed;
                transform.Translate(NewPosition);
            }
            else
            {
                if (turnManager.turnWhite)
                {
                    transform.position = new Vector3(3.5f, 7, 0);
                }
                else
                {
                    transform.position = new Vector3(3.5f, 7, 7);
                }
            }
        }
        else if (Input.GetMouseButtonUp(1))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (Input.GetMouseButtonUp(2))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Vector3 pos = transform.position;
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                pos = pos - transform.forward;
                transform.position = pos;
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                pos = pos + transform.forward;
                transform.position = pos;
            }
        }
        if (Vector3.Distance(new Vector3(3.5f, 0, 3.5f), transform.position) < maxDist)
        {
            NewPosition = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Time.deltaTime * speed;
            transform.Translate(NewPosition);
        }
        else
        {
            if (turnManager.turnWhite)
            {
                transform.position = new Vector3(3.5f, 7, 0);
            }
            else
            {
                transform.position = new Vector3(3.5f, 7, 7);
            }
        }
    }

    /*
        if (turnManager.turnWhite != prevTurn || moving)
        {
            if(turnManager.turnWhite != prevTurn)
            {
                timeCount = 0;
            }
            if (turnManager.turnWhite)
            {
                target = whitePOV;
            }
            else
            {
                target = blackPOV;
            }
            if (transform.position != target.position)
            {
                moving = true;
                Vector3 center = new Vector3(3.5f,0,3.5f);
                Vector3 riseRelCenter = transform.position - center;
                Vector3 setRelCenter = target.position - center;
                float fracComplete = timeCount / speed;
                transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
                transform.position += center;
            }
            else
            {
                moving = false;
            }
            if (transform.rotation != target.rotation)
            {
                moving = true;
                transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, timeCount);
            }
            else
            {
                moving = false;
            }
            timeCount += Time.deltaTime;
            prevTurn = turnManager.turnWhite;
        }
        */
}
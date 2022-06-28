using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{



    public TurnManager turnManager;
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float speedX;
    public float speedY;
    public float sensitivityX = 2F;
    public float sensitivityY = 2F;
    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -90F;
    public float maximumY = 90F;
    public int maxDist;
    float rotationY = -60F;
    bool prevTurn = false;
    void Update()
    {
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
            prevTurn = turnManager.turnWhite;
        }
        if (Input.GetMouseButton(0))
        {
            MouseLeftClick();
        }
        else if (Input.GetMouseButton(2))
        {
            if(Vector3.Distance(new Vector3(3.5f, 0, 3.5f), transform.position) < maxDist)
            { 
                MouseMiddleButtonClicked();
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
        else if (Input.GetMouseButtonUp(0))
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
            MouseWheeling();
        }
    }
    void MouseMiddleButtonClicked()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Vector3 NewPosition = new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y"));
        Vector3 pos = transform.position;
        if (NewPosition.x > 0.0f)
        {
            pos += transform.right*speedX*Time.deltaTime;
        }
        if (NewPosition.x < 0.0f)
        {
            pos -= transform.right*speedX*Time.deltaTime;
        }
        if (NewPosition.z > 0.0f)
        {
            pos += transform.forward*speedY*Time.deltaTime;
        }
        if (NewPosition.z < 0.0f)
        {
            pos -= transform.forward * speedY * Time.deltaTime;
        }
        pos.y = transform.position.y;
        transform.position = pos;
    }
    void MouseLeftClick()
    {
        if (axes == RotationAxes.MouseXAndY)
        {
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
        else if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
        }
        else
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        }
    }
    void MouseWheeling()
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
}
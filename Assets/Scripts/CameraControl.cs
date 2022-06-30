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
    float rotationY;
    Vector3 NewPosition;
    void Start()
    {
        rotationY = -transform.localEulerAngles.x;
    }
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
        if (Vector3.Distance(new Vector3(3.5f, 0, 3.5f), transform.position) < maxDist)
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                transform.position = transform.position - transform.forward * Time.deltaTime * speed;
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                transform.position = transform.position + transform.forward * Time.deltaTime * speed;
            }
            if(Input.GetKey("space"))
            {
                transform.position = transform.position + transform.up * Time.deltaTime * speed;
            }
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                transform.position = transform.position - transform.up * Time.deltaTime * speed;
            }
            if (Input.GetMouseButton(2))
            {
                NewPosition = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0) * Time.deltaTime * speed;
                transform.Translate(NewPosition);
            }
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
            transform.LookAt(new Vector3(3.5f, 0, 3.5f));
        }
    }
}
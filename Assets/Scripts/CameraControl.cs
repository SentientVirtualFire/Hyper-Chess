using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{



    public TurnManager turnManager;
    public float speed;
    public Transform whitePOV;
    public Transform blackPOV;
    public float journeyTime = 1.0f;
    bool prevTurn = false;
    bool moving = false;
    Transform target;
    float startTime;
    float timeCount;
    void Update()
    {
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
    }
}
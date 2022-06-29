using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Camera mainCamera;
    public float timeToFade;
    public Color lights;
    public Color darks;
    bool fading;

    void Start()
    {
        
    }

    void Update()
    {
        /*
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (boards.Contains(hit.transform.gameObject))
            {
                Material target = hit.transform.gameObject.GetComponent<MeshRenderer>().material;
                target.color = Color.Lerp(target.color, faded, timeToFade * Time.deltaTime);
            }
        }
        */
    }

    void OnMouseOver()
    {
        darks.a = 64;
        lights.a = 64;
        gameObject.GetComponent<MeshRenderer>().materials[0].color = darks;
        gameObject.GetComponent<MeshRenderer>().materials[1].color = lights;
    }

    void OnMouseExit()
    {
        darks.a = 255;
        lights.a = 255;
        gameObject.GetComponent<MeshRenderer>().materials[0].color = darks;
        gameObject.GetComponent<MeshRenderer>().materials[1].color = lights;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private float lenght, startPos;
    public GameObject mainCamera;
    public float parallaxEffect;

    void Start()
    {
        startPos = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float temp = (mainCamera.transform.position.x * (1 - parallaxEffect));
        float distance = (mainCamera.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.x);

        if(temp > startPos + lenght)
        {
            startPos += lenght;
        }
        else if(temp < startPos - lenght) 
        {
            startPos -= lenght;
        }
    }
}

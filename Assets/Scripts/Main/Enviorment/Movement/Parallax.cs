using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Camera cam;
    public Transform subject;

    Vector2 startPosition;
    float startY;
    float startZ;

    Vector2 travel =>(Vector2)cam.transform.position-startPosition; // distance camera has moved

    float distanceFromSubject => gameObject.transform.position.z - subject.transform.position.z;
    float clippingPlane => cam.transform.position.z+ ((distanceFromSubject > 0f) ? cam.farClipPlane : cam.nearClipPlane);
    float parallaxFactor => Mathf.Abs((distanceFromSubject) / clippingPlane);
    // Start is called before the first frame update

    void Start()
    {
        startPosition = gameObject.transform.position;
        startY = gameObject.transform.position.y;
        startZ = gameObject.transform.position.z; 
    }

    // Update is called once per frame
    void Update()
    {
        float newPosX = startPosition.x+travel.x*parallaxFactor/100;
        //float newPosY = startPosition.y+travel.y * parallaxFactor/100;
        transform.position = new Vector3(newPosX, startY, startZ);


    }
}

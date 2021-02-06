using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Camera cam;
    public Transform player;

    Vector2 startPosition;
    float startZPos;
    private float length; 

    /* => denotes Property variable
    * Calculated everytime it is called 
    * Read only
    */
    Vector2 travel => (Vector2)cam.transform.position - startPosition;

    float distanceFromSubject => transform.position.z - player.position.z;

    // If farther to the camera than the player use farther clipping plane
    float clippingPlane => (cam.transform.position.z + (distanceFromSubject > 0 ? cam.farClipPlane : cam.nearClipPlane));

    float parallaxFactor => Mathf.Abs(distanceFromSubject) / clippingPlane; 
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startZPos = transform.position.z;

        length = GetComponent<SpriteRenderer>().bounds.size.x;
        
    }

    // Update is called once per frame
    void Update()
    {
        float temp = (cam.transform.position.x) * (1 - parallaxFactor);

        Vector3 newPos = startPosition + travel * parallaxFactor;
        transform.position = new Vector3(newPos.x, newPos.y, startZPos);

        if (temp > startPosition.x + length) startPosition.x += length;
        else if (temp < startPosition.x - length) startPosition.x -= length;
    }
}

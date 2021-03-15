using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Camera cam;
    public Transform player;

    // Instance variables
    Vector2 startPosition;
    float startZPos;

    public SpriteRenderer loopSpriteRenderer;

    float twoAspect => cam.aspect * 2;
    float tileWidth => (twoAspect > 3 ? twoAspect : 3);
    float viewWidth => loopSpriteRenderer.sprite.rect.width / loopSpriteRenderer.sprite.pixelsPerUnit;
    Vector2 travel => (Vector2)cam.transform.position - startPosition;
    float distanceFromSubject => transform.position.z - player.position.z;
    // If farther from the camera than the player use farther clipping plane for parallax factor, else use near clipping plane
    float clippingPlane => (cam.transform.position.z + (distanceFromSubject > 0 ? cam.farClipPlane : cam.nearClipPlane));
    float parallaxFactor => Mathf.Abs(distanceFromSubject) / clippingPlane;

    //User options 
    public bool xAxis = true; // parallax on x?
    public bool yAxis = true; // parallax on y?
    public bool infiniteLoop = false; //do we loop?

 
    void Awake()
    {
        cam = Camera.main;
        startPosition = transform.position;
        startZPos = transform.position.z;

        if(loopSpriteRenderer != null && infiniteLoop)
        {
            float spriteSizeX = loopSpriteRenderer.sprite.rect.width / loopSpriteRenderer.sprite.pixelsPerUnit;
            float spriteSizeY = loopSpriteRenderer.sprite.rect.height / loopSpriteRenderer.sprite.pixelsPerUnit;

            // Change draw mode to titled so sprite can be draw multiple times  
            loopSpriteRenderer.drawMode = SpriteDrawMode.Tiled;
            // Increase sprite renderer size to allow for tiles to be tiled
            loopSpriteRenderer.size = new Vector2(spriteSizeX * tileWidth, spriteSizeY);
            // Ensure scale of sprite remains the same 
            transform.localScale = Vector3.one;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            Vector2 newPos = startPosition + travel * parallaxFactor;
            transform.position = new Vector3(xAxis ? newPos.x : startPosition.x, yAxis ? newPos.y : startPosition.y, startZPos);

            if (infiniteLoop)
            {
                Vector2 totalTravel = cam.transform.position - transform.position;
                float boundsOffset = (viewWidth / 2) * (totalTravel.x > 0 ? 1 : -1);
                float screens = (int)((totalTravel.x + boundsOffset) / viewWidth);
                transform.position += new Vector3(screens * viewWidth, 0);
            }
        }
        
    }
}

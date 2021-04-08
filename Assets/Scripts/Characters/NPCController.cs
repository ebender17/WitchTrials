using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPCController : MonoBehaviour
{
    public Transform player;

    private int facingDirection;

    // Start is called before the first frame update
    void Start()
    {
        facingDirection = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //Check incase player is destroyed and transform is no longer valid
        if(player != null)
        {
            if (facingDirection == 1 && player.gameObject.transform.position.x < transform.position.x)
            {
                facingDirection = -1;

                transform.Rotate(0, 180, 0);

            }
            else if(facingDirection == -1 && player.gameObject.transform.position.x > transform.position.x)
            {
                facingDirection = 1;
                transform.Rotate(0, 180, 0);
            }
        }
        
    }

}

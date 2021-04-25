using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDamage : MonoBehaviour
{
    [SerializeField] private uint _damage;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

            if (playerController != null)
            {
                playerController.TakeDamage(transform.position.x, _damage, true);
            }

        }
    }
}

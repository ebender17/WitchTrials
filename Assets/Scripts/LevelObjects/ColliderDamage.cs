using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDamage : MonoBehaviour
{
    [SerializeField] private uint _damage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collider damage.");
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

        if (playerController != null)
        {
            playerController.TakeDamage(transform.position.x, _damage);
        }

    }
}

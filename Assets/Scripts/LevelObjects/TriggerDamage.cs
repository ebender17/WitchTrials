using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    [SerializeField] private uint _damage;
    public void OnTriggerStay2D(Collider2D collision)
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

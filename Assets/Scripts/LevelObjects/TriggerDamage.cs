using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    [SerializeField] private uint _damage;
    [SerializeField] private float _damageCooldown = 0f;
    private float _lastDamageTime = 0f;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(Time.time >= _lastDamageTime + _damageCooldown)
        {
            if (collision.gameObject.tag == "Player")
            {
                PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

                if (playerController != null)
                {
                    _lastDamageTime = Time.time;
                    playerController.TakeDamage(transform.position.x, _damage, true);
                    Debug.Log("Damage Player!");
                }
            }
        }
        
    }
}

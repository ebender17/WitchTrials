using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10.0f;

    [Header("Jump State")]
    public float jumpVelocity = 10.0f;
    public float jumpHeightMultiplier = 0.5f;
    public int numJumps = 2;

    [Header("In Air State")]
    public float coyoteTime = 0.2f; 

    [Header("Dash State")]
    public float dashCoolDown = 0.5f;
    public float maxHoldTime = 3.0f;
    public float holdTimeScale = 0.25f;
    public float dashTime = 0.2f;
    public float dashVelocity = 25.0f;
    public float drag = 10.0f;
    public float dashEndYMultiplier = 0.2f;

    [Header("Crouch States")]
    public float crouchMovementVelocity = 5.0f;
    public float crouchColliderHeight = 0.8f;
    public float standColliderHeight = 1.8f;

    [Header("Primary Attack State")]
    public float primAtkCoolDown = 0.5f;
    public float knockBackDuration = 0.2f;
    public Vector2 knockBackSpeed = new Vector2(10.0f, 5.0f);


    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public LayerMask whatIsGround;

    [Header("Player Stats")]
    public float score = 0.0f;
    public float health = 100.0f;
}

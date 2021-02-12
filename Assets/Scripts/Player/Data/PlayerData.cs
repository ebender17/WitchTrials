using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10.0f;

    [Header("Jump State")]
    public float jumpVelocity = 5.0f;
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

    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public LayerMask whatIsGround;
}

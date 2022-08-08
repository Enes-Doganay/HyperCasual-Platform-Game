using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="PlayerData")]
public class PlayerData : ScriptableObject
{
    public int score;

    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public float wallCheckDistance = 0.5f;
    public LayerMask whatIsGround;
    public LayerMask whatIsWall;

    [Header("Jump State")]
    public float jumpVelocityX = 4f;
    public float jumpVelocityY = 6f;

    [Header("Wall Jump State")]
    public float wallJumpVelocityX = 3f;
    public float wallJumpVelocityY = 5f;

    [Header("Wall Slide State")]
    public float wallSlideVelocity = 3f;


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    PlayerStateMachine StateMachine;
    ScoreManager ScoreManager;
    public PlayerIdleState IdleState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerDeathState DeathState { get; private set; }



    [SerializeField]
    private PlayerData playerData;
    public Rigidbody2D RB { get; private set; }
    public Animator Anim { get; private set; }
    public bool rightInput = false;
    public bool leftInput = false;

    public int FacingDirection { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }

    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Vector2 workspace;

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "inAir");
        DeathState = new PlayerDeathState(this, StateMachine, playerData, "death");
    }
    private void Start()
    {
        Anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        FacingDirection = 1;
        StateMachine.Initialize(IdleState);
    }


    private void Update()
    {
        CurrentVelocity = RB.velocity;
        StateMachine.CurrentState.LogicUpdate();

        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.position.x < Screen.width / 2)
            {
                Debug.Log("Left click");
            }
            else if (touch.position.x > Screen.width / 2)
            {
                Debug.Log("Right click");
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.x < Screen.width / 2)
            {
                leftInput = true;
            }
            else if (Input.mousePosition.x >= Screen.width / 2)

            {
                rightInput = true;
            }
            CheckIfShouldFlip();
        }

    }
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void CheckIfShouldFlip()
    {
        if (!rightInput && FacingDirection == 1)
        {
            FacingDirection *= -1;
            Flip(FacingDirection);
        }
        else if(!leftInput && FacingDirection == -1)
        {
            FacingDirection *= -1;
            Flip(FacingDirection);
        }

    }
    public void Flip(int facingDirection)
    {
        transform.Rotate(0f, facingDirection * 180f, 0f);
    }

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius,playerData.whatIsGround);
    }
    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsWall);
    }
    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }
    public void Jump()
    {
        workspace.Set(playerData.jumpVelocityX * FacingDirection, playerData.jumpVelocityY);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
        rightInput = false;
        leftInput = false;
        ScoreManager.Instance.IncreaseScore(1);
    }
    public void WallJump()
    {
        transform.DOMove(new Vector3(0f ,transform.position.y + playerData.wallJumpVelocityY),0.4f);
        rightInput = false;
        leftInput = false;
        ScoreManager.Instance.IncreaseScore(1);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "IceThorn")
        {
            StateMachine.ChangeState(DeathState);
        }
    }
}

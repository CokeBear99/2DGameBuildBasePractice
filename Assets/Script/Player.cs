using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{

    private float xInput;

    [Header("Move info")]
    [SerializeField] private float JumpForce;
    [SerializeField] private float movespeed;
        

    [Header("Dash info")]
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;
    [SerializeField] private float dashCooldownTimer;

    [Header("Attack info")]
    [SerializeField]private bool isAttacking;
    [SerializeField]private int comboCounter;
    [SerializeField]private float comboTime = .3f;
    [SerializeField]private float comboTimeCounter;
        

    protected override void Start()
    {


    }

    protected override void Update()
    {
        base.Update();

        MoveMent();
        CheckInput();

        dashTime -= Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime;
        comboTimeCounter -= Time.deltaTime;




        FlipController();
        AnimatorController();

    }

    public void AttackOver()
    {
        isAttacking = false;

        comboCounter++;
        if (comboCounter > 2)
        {
            comboCounter = 0;
        }

    }



    private void FlipController()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && facingDirection == 1)
        {
            Flip();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && facingDirection == -1)
        {
            Flip(); 
        }
    }

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && isGround == true && !isAttacking)
        {
            Jump();
            isGround = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashAbility();
        }

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartAttackEvent();
        }


    }

    private void StartAttackEvent()
    {
        if (!isGround)
            return;

        if (comboTimeCounter < 0)
        {
            comboCounter = 0;
        }

        isAttacking = true;
        comboTimeCounter = comboTime;
    }

    private void DashAbility()
    {
        if(dashCooldownTimer < 0 && !isAttacking)
        {
            dashCooldownTimer = dashCooldown;
            dashTime = dashDuration;
        }
    }


    private void MoveMent()
    {
        if (isAttacking)
        {
            rb.velocity = new Vector2(0, 0);
        }



        else if (dashTime >0)
        {
            rb.velocity = new Vector2(facingDirection * dashSpeed, rb.velocity.y);
        }
        else
        {
            if (xInput == 0)
            {
                rb.velocity = new Vector2(0, rb.velocity.y); // xInputÀÌ 0ÀÏ ¶§ ÀÌµ¿À» ¸ØÃã
            }
            else
            {
                rb.velocity = new Vector2(xInput * movespeed, rb.velocity.y);
            }
        }

    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }

    private void AnimatorController()
    {
        bool isMoving;

        if (isMoving = rb.velocity.x != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGround", isGround);
        anim.SetBool("isDacing", dashTime > 0);
        anim.SetBool("isAttacking", isAttacking);
        anim.SetInteger("comboCounter", comboCounter);
    }







}

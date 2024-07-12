using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    [SerializeField]protected Rigidbody2D rb;
    [SerializeField]protected Animator anim;
    [Header("Move Direction")]
    [SerializeField]protected int facingDirection = 1;

    [Header("Jump info")]
    [SerializeField] protected bool isGround;

    [Header("Collision info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [Space]
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;

    protected bool isWallDetected;
        
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

    }

    protected virtual void Update()
    {
        CollisionChecks();   
    }

        

    protected virtual private void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0, 180, 0);
    }

    protected virtual void CollisionChecks()
    {
        isGround = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
    }


}



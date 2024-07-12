using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Entity
{
    bool isAttacking;

    [Header("Move Info")]
    [SerializeField] private float moveSpeed;

    [Header("Player Detection")]
    [SerializeField] private float playerCheckDistance;
    [SerializeField] private LayerMask whatIsPlayer;

    private RaycastHit2D isPlayerDetected;

    protected override void Start()
    {
        base.Start();


    }

    protected override void Update()
    {
        base.Update();

        if (!isGround || isWallDetected)
        {
            Flip();
        }

        if (!isAttacking)
            rb.velocity = new Vector2(moveSpeed * facingDirection, rb.velocity.y);

        if (isPlayerDetected)
        {
            if(isPlayerDetected.distance > 1)
            {
                rb.velocity = new Vector2(moveSpeed * 5.0f * facingDirection, rb.velocity.y);

                Debug.Log("I see the player");
                isAttacking = false;

            }
            else
            {
                Debug.Log("Attac!" + isPlayerDetected);
                isAttacking = true;
                rb.velocity = Vector2.zero;
            }
        }

    }


    protected override void CollisionChecks()
    {
        base.CollisionChecks();

        isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right, playerCheckDistance * facingDirection, whatIsPlayer);
        isWallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance * facingDirection, whatIsGround);
    }


    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + playerCheckDistance * facingDirection, transform.position.y));

        Gizmos.color = Color.green;
        Gizmos.DrawLine(wallCheck.position,new Vector3(wallCheck.position.x + wallCheckDistance * facingDirection,wallCheck.position.y));
    }


}

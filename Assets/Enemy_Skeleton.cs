using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Entity
{
  bool isAttacking;
  
  [Header("Move info")]
  [SerializeField] private float moveSpeed;
  
  [Header("Player detection")]
  [SerializeField] private float playerCheckDistance;
  [SerializeField] private LayerMask whatIsPlayer;
  
  private RaycastHit2D isPlayerDetected;
  
    protected override void Start()
    {
      base.Start();
    }
    
    override protected void Update()
    {
      base.Update();
      
      if(isPlayerDetected)
      {
        if(isPlayerDetected.distance > 1)
        {
          rb.velocity = new Vector2(moveSpeed * 1.5f * facingDir, rb.velocity.y);
         
          Debug.Log("i c player");
          isAttacking = false;
        }
        else
        {
          Debug.Log("attack! " + isPlayerDetected.collider.gameObject.name);
          isAttacking = true;
        }
      }
      
      if(!isGrounded || isWallDetected)
        Flip();
      
      Movement();
    }
    private void Movement()
    {
      if(!isAttacking)
        rb.velocity = new Vector2(moveSpeed * facingDir, rb.velocity.y);
    }
    
    protected override void CollisionChecks()
    {
      base.CollisionChecks();
      isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right, playerCheckDistance * facingDir, whatIsPlayer);
    }
    
    protected override void OnDrawGizmos() {
      base.OnDrawGizmos();
      
      Gizmos.color = Color.blue;
      Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + playerCheckDistance * facingDir, transform.position.y));
    }
}

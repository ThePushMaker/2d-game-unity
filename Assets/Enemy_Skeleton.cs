using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Entity
{
  [Header("Move info")]
  [SerializeField] private float moveSpeed;
  
    protected override void Start()
    {
      base.Start();
    }
    
    override protected void Update()
    {
      base.Update();
      
      if(!isGrounded || isWallDetected)
        Flip();
      
      rb.velocity = new Vector2(moveSpeed * facingDir, rb.velocity.y);
    }
}

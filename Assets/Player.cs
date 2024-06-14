using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    
    [Header("Dash Info")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    private float dashTime;
    
    [SerializeField] private float dashCooldown;
    private float dashCooldownTimer;
    
    [Header("Attack Info")]
    [SerializeField] private float comboTime;
    private float comboTimeWindow;
    private bool isAttacking;
    private int comboCounter;
    
     
    private float xInput;
    
    // default facing direction is right
    private int facingDirection = 1;
    private bool facingRight = true;
    
    
    [Header("Collision info")]
    [SerializeField] private float groundCheckDistance = 1.4f;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;
    private bool isMoving;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckInput();
        CollisionChecks();
        
        dashTime -= Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime;
        comboTimeWindow -= Time.deltaTime;
        
        FlipController();
        AnimatorControllers();
    }

    //
    public void AttackOver()
    {
        isAttacking = false;
        comboCounter++;
        if(comboCounter > 2)        
        {
            comboCounter = 0;
        }
        
    }
     
    private void CollisionChecks()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }
    
    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
           StartAttackEvent();
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            DashAbility();
        }
    }
    private void StartAttackEvent()
    {
        if(!isGrounded)
            return;
            
        if(comboTimeWindow < 0)
            comboCounter = 0;
        
        isAttacking = true;
        comboTimeWindow = comboTime;
    }
    
    private void DashAbility()
    {
        if (dashCooldownTimer <= 0 && !isAttacking)
        {
            dashCooldownTimer = dashCooldown;
            dashTime = dashDuration;
        }
    }
    
    private void Movement()
    {
        if (isAttacking)
        {
            rb.velocity = new Vector2(0, 0);
        }
        else if(dashTime > 0)
        {
            rb.velocity = new Vector2(facingDirection * dashSpeed, 0);
        } 
        else
        {
            rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        }
    }
    
    private void Jump()
    {
        if(isGrounded)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    
    private void AnimatorControllers()
    {
        isMoving = rb.velocity.x != 0;
        
        anim.SetFloat("yVelocity", rb.velocity.y);
        
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isDashing", dashTime > 0);
        anim.SetBool("isAttacking", isAttacking);
        anim.SetInteger("comboCounter", comboCounter);
    }
    
    private void Flip()
    {
        facingDirection = facingDirection * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    
    private void FlipController()
    {
        if(rb.velocity.x > 0 && !facingRight || rb.velocity.x < 0 && facingRight)
            Flip();
    }
    
    private void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position, new Vector3( transform.position.x, transform.position.y - groundCheckDistance ));
    }
}

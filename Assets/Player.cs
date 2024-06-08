using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    
    private float xInput;
    
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
        
        AnimatorControllers();
    }
    
    
    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
    
    private void Movement()
    {
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
    }
    
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    
    private void AnimatorControllers()
    {
        bool isMoving;
        
        isMoving = rb.velocity.x != 0;
        
        anim.SetBool("isMoving", isMoving);
    }
}

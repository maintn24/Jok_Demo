 using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    ChangeSkin changeSkin;

    public Animator playerAnim;
    private Rigidbody2D rb;

    [Header("Particle System")]
    public ParticleSystem jumpEff;
    public ParticleSystem runEff;
    public ParticleSystem dustEff;

    //basic movement
    [Header("Basic Movement")]
    public float runSpeed;
    private float moveInput;
    private float direction;
   
    private bool facingRight = true;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;


    //climbing
    [Header("Climbing")]
    public float wallSlidingSpeed;
    private bool isTouchingFront;
    public Transform frontCheck;
    bool wallSliding;
    
    bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;

    //jumping
    [Header("Jumping")]
    public float jumpForce;
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;
    private bool doubleJump;

    //Dashing
    [Header("Dashing")]
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private bool dashEnd = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
    }

    private void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * runSpeed, rb.velocity.y);

        //
        if (moveInput != 0)
        {
            playerAnim.SetBool("isRunning", true);
        }
        else
        {
            playerAnim.SetBool("isRunning", false);
        }

        //
        if (isGrounded == true)
        {
            playerAnim.SetBool("isJumping", false);
            doubleJump = false;
        }
        else
        {
            playerAnim.SetBool("isJumping", true);
        }

        //////dashing

        if (changeSkin.whatskin == "Cheetah" )
        {
            Dash();
        }
    }

    private void Update()
    {

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (facingRight == false && (moveInput > 0))
        {
            Flip();
        }else if(facingRight == true && (moveInput < 0))
        {
            Flip();
        }

        ///////////////////
        


        //jumping/////////////////////////////
        if(isGrounded == true && Input.GetKeyDown(KeyCode.W))
        {
            playerAnim.SetTrigger("takeOff");
            DustEffect();
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;

        }
        else if(isGrounded == false && doubleJump == false && Input.GetKeyDown(KeyCode.W))
        {
            playerAnim.SetTrigger("takeOff");
            isJumping = true;
            doubleJump = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKey(KeyCode.W) && isJumping == true)
        {
            JumpEffect();
            if(jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }

            
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            isJumping = false;
        }

        
        
        if(changeSkin.whatskin == "Monkey")
        {
            Climb();
        }
       

    }

    void Climb()  ///////////////Climbing///////////////////////////
    {
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);
        if (isTouchingFront == true && isGrounded == false && moveInput != 0)
        {
            wallSliding = true;
        }
        else
        {
            wallSliding = false;
        }

        if (wallSliding)
        {
            playerAnim.SetBool("isSliding", true);
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            playerAnim.SetBool("isSliding", false);
        }

        if (Input.GetKeyDown(KeyCode.W) && wallSliding == true)
        {
            wallJumping = true;
            Invoke("SetWallJumpingToFalse", wallJumpTime);
            JumpEffect();
        }

        if (wallJumping == true)
        {
            rb.velocity = new Vector2(xWallForce * -moveInput, yWallForce);
        }
    }

    void Dash()
    {
        if (dashEnd == false)
        {
            if ( Input.GetKeyDown(KeyCode.J))
            {
                dashEnd = true;
            }
        }
        else
        {
            if (dashTime <= 0)
            {
                dashEnd = false;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
            }
            else
            {
                dashTime -= Time.deltaTime;
                DustEffect();
                RunEffect();
                
                rb.velocity = new Vector2(direction * dashSpeed, rb.velocity.y);
            }
        }
    }
    void Flip()
    {
        DustEffect();
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

        if (facingRight) direction = 1;
        else direction = -1;
    }

    void SetWallJumpingToFalse()
    {
        wallJumping = false;
    }

    void JumpEffect()
    {
        jumpEff.Play();
    }

    void RunEffect()
    {
        runEff.Play();
    }

    void DustEffect()
    {
        dustEff.Play();
    }
}

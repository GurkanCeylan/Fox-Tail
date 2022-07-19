using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;
    public float jumpForce;
    public Rigidbody2D theRB;

    private bool isGrounded;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    private bool canDoubleJump;
   
    /*
    public bool isCrouching;
    public Collider2D standCollider, crouchCollider;
    */
 
    private Animator anim;
    private SpriteRenderer theSR;

    public float knockbackLength, knockbackForce;
    private float knockbackCounter;

    public float bounceForce;

    public bool stopInput;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        anim = GetComponent<Animator>();            //private but find automatically
        theSR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!PauseMenu.instance.isPaused && !stopInput)
        {
            if (knockbackCounter <= 0)
            {
               
                theRB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), theRB.velocity.y);
              
                isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

                if (isGrounded)
                {
                    canDoubleJump = true; 
                }
                /*
                if (Input.GetButtonDown("Crouch") && isGrounded)
                {
                    isCrouching = true;
                    standCollider.enabled = false;
                    crouchCollider.enabled = true;

                    moveSpeed = 0;

                    anim.SetBool("isCrouched", true);
                }
                else if (Input.GetButtonUp("Crouch") && isGrounded)
                {
                    moveSpeed = 8f;

                    isCrouching = false;
                    standCollider.enabled = true;
                    crouchCollider.enabled = false;

                    anim.SetBool("isCrouched", false);
                }
                */

                if (Input.GetButtonDown("Jump")) //&& !isCrouching
                {
                    if (isGrounded)
                    {
                        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                        AudioManager.instance.PlaySFX(10);
                    }
                    else
                    {
                        if (canDoubleJump)
                        {
                            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                            canDoubleJump = false;
                            AudioManager.instance.PlaySFX(10);
                        }
                    }
                }

                if (theRB.velocity.x < 0 )
                {
                    theSR.flipX = true;
                }
                else if(theRB.velocity.x > 0)
                {
                    theSR.flipX = false;
                }
            }
            else
            {
                knockbackCounter -= Time.deltaTime;

                if (theSR.flipX)
                {
                    theRB.velocity = new Vector2(knockbackForce, theRB.velocity.y);
                }
                else
                {
                    theRB.velocity = new Vector2(-knockbackForce, theRB.velocity.y);
                }
            }
        }

        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
    }

    public void KnockBack()
    {
        knockbackCounter = knockbackLength;
        theRB.velocity = new Vector2(0f, knockbackForce);

        anim.SetTrigger("hurt");
    }

    public void Bounce()
    {
        theRB.velocity = new Vector2(theRB.velocity.x, bounceForce);
        AudioManager.instance.PlaySFX(10);
    }

}

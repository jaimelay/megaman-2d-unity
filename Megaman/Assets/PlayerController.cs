using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Animator animator;
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    private bool isGrounded;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    Transform groundCheckL;

    [SerializeField]
    Transform groundCheckR;

    [SerializeField]
    private float runSpeed = 1.5f;

    [SerializeField]
    private float jumpHeight = 5f;

    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
        isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")) ||
                        Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Ground")) ||
                        Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground"));

        if (isGrounded == false) {
            animator.Play("Player_Jump");
        }

        if (Input.GetKey("a") || Input.GetKey("left")) {
            rb2d.velocity = new Vector2(-runSpeed, rb2d.velocity.y);
            
            if (isGrounded) {
                animator.Play("Player_Run");
            }

            spriteRenderer.flipX = true;
        }
        else if (Input.GetKey("d") || Input.GetKey("right")) {
            rb2d.velocity = new Vector2(runSpeed, rb2d.velocity.y);

            if (isGrounded) {
                animator.Play("Player_Run");
            }

            spriteRenderer.flipX = false;
        }
        else {
            if (isGrounded) {
                animator.Play("Player_Idle");
            }

            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }

        if ((Input.GetKey("space") || Input.GetKey("up")) && isGrounded) {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpHeight);
            animator.Play("Player_Jump");
        }
    }
}

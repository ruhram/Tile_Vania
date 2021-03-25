using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    //config
    [SerializeField] float runSpeed = 100f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float climbSpeed = 5f;

    //state
    bool isAlive = true;

    //cache
    Rigidbody2D myrigidbody;
    Animator myAnimator;
    Collider2D myColllider;

    // Start is called before the first frame update
    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myColllider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        flipSprite();
        Jump();
        climbLadder();
    }

    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal"); // the value is between -1 to 1
        Debug.Log(controlThrow);
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed , myrigidbody.velocity.y);
        myrigidbody.velocity = playerVelocity;

        bool PlayerHasHorizontalSpeed = Mathf.Abs(myrigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", PlayerHasHorizontalSpeed);
    }
    private void climbLadder()
    {
        if (!myColllider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) { return; }
        float controlThrow = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(myrigidbody.velocity.x, controlThrow * climbSpeed);
        myrigidbody.velocity = climbVelocity;

        bool playerhasverticalspeed = Mathf.Abs(myrigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerhasverticalspeed);

    }

    private void Jump()
    {
        if (!myColllider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
            if (Input.GetButtonDown("Jump"))
            {
                Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);
                myrigidbody.velocity += jumpVelocity;
            }
    }
    private void flipSprite()
    {
        bool PlayerHasHorizontalSpeed = Mathf.Abs(myrigidbody.velocity.x) > Mathf.Epsilon;
        if (PlayerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myrigidbody.velocity.x), 1f);
        }
    }
}

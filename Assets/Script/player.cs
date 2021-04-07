using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    //config
    [SerializeField] float runSpeed = 100f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(-25f, 25f);

    //state
    bool isAlive = true;

    //cache
    Rigidbody2D myrigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyColllider;
    BoxCollider2D myFeet;
    float gravityScale;

    // Start is called before the first frame update
    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyColllider = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        gravityScale = myrigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }
        Run();
        flipSprite();
        Jump();
        climbLadder();
        die();
    }

    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal"); // the value is between -1 to 1
        Debug.Log(controlThrow);
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed , myrigidbody.velocity.y);
        myrigidbody.velocity = playerVelocity;

        bool PlayerHasHorizontalSpeed = Mathf.Abs(myrigidbody.velocity.x) >= Mathf.Epsilon;
        myAnimator.SetBool("isRunning", PlayerHasHorizontalSpeed);
    }
    private void climbLadder()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            myAnimator.SetBool("isClimbing", false);
            myrigidbody.gravityScale = 1f;
        }
        else
        {
            float controlThrow = Input.GetAxis("Vertical");
            Vector2 climbVelocity = new Vector2(myrigidbody.velocity.x, controlThrow * climbSpeed);
            myrigidbody.velocity = climbVelocity;
            myrigidbody.gravityScale = 0f;

            bool playerhasverticalspeed = Mathf.Abs(myrigidbody.velocity.y) >= Mathf.Epsilon;
            myAnimator.SetBool("isClimbing", playerhasverticalspeed);
            print(Mathf.Abs(myrigidbody.velocity.y));
        }

    }

    private void Jump()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
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
    private void die()
    {
        if (myBodyColllider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            isAlive = false;
            myAnimator.SetTrigger("isDying");
            GetComponent<Rigidbody2D>().velocity = deathKick;
        }
    }
}

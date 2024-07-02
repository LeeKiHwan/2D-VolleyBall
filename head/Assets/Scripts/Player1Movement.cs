using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Movement : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigidbody;
    public static float Speed = 3;
    public static int doubleJumpTime, jumpLimited=0;
    public float jumpForce = 5;
    public int jumpCount = 0;
    public bool isGrounded = false, doubleJumpReady = false, aBool= true;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody.velocity = new Vector2(-Speed, rigidbody.velocity.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigidbody.velocity = new Vector2(Speed, rigidbody.velocity.y);
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (isGrounded == true && aBool == true)
            {
                jump();
                isGrounded = false;
            }
        }
        if (!isGrounded && aBool)
        {
            if (Input.GetKeyUp(KeyCode.W))
            {
                doubleJumpReady = true;
                aBool = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.W) && doubleJumpReady && jumpLimited > 0)
        {
            jump();
            doubleJumpReady =false;
            jumpLimited--;
        }
    }

    void jump()
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Platform")
        {
            doubleJumpReady = false;
            isGrounded = true;
            aBool = true;
        }
    }
}

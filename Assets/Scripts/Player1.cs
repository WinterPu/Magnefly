using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{

    public float speed = 5.0f;
    Rigidbody2D rb;
    public LayerMask groundLayers;

    public float jumpForce = 7;
    CircleCollider2D col;
    public Transform foot;
    public float footCheckRadius = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal1");

        var movement = new Vector2(moveHorizontal, 0);

        rb.AddForce(movement * speed);

        if (Input.GetKeyDown(KeyCode.W) && IsGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public bool IsGrounded
    {
        get
        {
            return Physics2D.OverlapCircle(foot.position, footCheckRadius, groundLayers);
        }
    }
}

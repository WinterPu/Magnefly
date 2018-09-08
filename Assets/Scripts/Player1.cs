using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour {

    public float speed = 5.0f;
    Rigidbody2D rb;
    public LayerMask groundLayers;

    public float jumpForce = 7;
    public CircleCollider2D col;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        var movement = new Vector2(moveHorizontal, 0);

        rb.AddForce(movement * speed);

        if (Input.GetKeyDown(KeyCode.W))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}

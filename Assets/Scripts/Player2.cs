using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public float speed = 5.0f;
    Rigidbody2D rb;
    public LayerMask groundLayers;

    public float jumpForce = 7;
     CircleCollider2D col;
    public Transform foot;
    public float footCheckRadius = 0.5f;

    public Player1 theOtherPlayer;
    public Status Status = Status.Neutral;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal2");

        var movement = new Vector2(moveHorizontal, 0);

        rb.AddForce(movement * speed);

        if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            // Polarize to South
            Status = Status.South;
        }
        if (Input.GetKeyUp(KeyCode.RightShift))
        {
            Status = Status.Neutral;
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            // Polarize to North
            Status = Status.North;
        }
        if (Input.GetKeyUp(KeyCode.Keypad1))
        {
            Status = Status.Neutral;
        }

        // Apply the megnetic forces to the players
        var theOtherStatus = theOtherPlayer.Status;
        if (Status == Status.North && theOtherStatus == Status.North
            || Status == Status.South && theOtherStatus == Status.South)
        {
            // Push away
            var dir = transform.position - theOtherPlayer.transform.position;
            var dis = Vector2.Distance(transform.position, theOtherPlayer.transform.position);
            AddForce(dir.normalized * 10 / dis);
        }
        else if (Status == Status.North && theOtherStatus == Status.South
            || Status == Status.South && theOtherStatus == Status.North)
        {
            // Pull together
            var dir = theOtherPlayer.transform.position - transform.position;
            var dis = Vector2.Distance(transform.position, theOtherPlayer.transform.position);
            AddForce(dir.normalized * 10 / dis );
        }
        else
        {
            // Do nothing
        }
    }

    public bool IsGrounded
    {
        get
        {
            return Physics2D.OverlapCircle(foot.position, footCheckRadius, groundLayers);
        }
    }

    public void AddForce(Vector2 force)
    {
        rb.AddForce(force);
        print("force: " + force);
    }
}

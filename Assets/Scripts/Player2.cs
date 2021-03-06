﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public bool UserInputEnabled = true;
    public bool Claimed = false;

    public Sprite southSprite;
    public Sprite northSprite;
    public Sprite neuturalSprite;

    public GameObject southChargeFX;
    public GameObject northChargeFX;

    public float speed = 5.0f;
    Rigidbody2D rb;
    SpriteRenderer sr;
    public LayerMask groundLayers;

    public float jumpForce = 7;
    CircleCollider2D col;
    public Transform foot;
    public float footCheckRadius = 0.5f;

    public Player1 theOtherPlayer;
    public Status Status = Status.Neutral;


    public AudioSource jump_sound;
    public AudioSource magnetic_sound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (UserInputEnabled)
        {
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                // Polarize to South
                Status = Status.South;
                sr.sprite = southSprite;
                if (theOtherPlayer.Claimed == false)
                    Claimed = true;
                else
                    Claimed = false;

                magnetic_sound.Play();
                southChargeFX.SetActive(true);
            }
            else if (Input.GetKeyUp(KeyCode.RightShift))
            {
                Status = Status.Neutral;
                sr.sprite = neuturalSprite;
                Claimed = false;

                magnetic_sound.Stop();
                southChargeFX.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                // Polarize to North
                Status = Status.North;
                sr.sprite = northSprite;
                if (theOtherPlayer.Claimed == false)
                    Claimed = true;
                else
                    Claimed = false;

                magnetic_sound.Play();
                northChargeFX.SetActive(true);
            }
            else if (Input.GetKeyUp(KeyCode.Keypad1))
            {
                Status = Status.Neutral;
                sr.sprite = neuturalSprite;
                Claimed = false;

                magnetic_sound.Stop();
                northChargeFX.SetActive(false);
            }


        }
        if (Global.CountOnTheElevator >= 2)
        {
            // Win the level. 
            UserInputEnabled = false;
        }
    }

    void FixedUpdate()
    {
        if (UserInputEnabled)
        {
            float moveHorizontal = Input.GetAxis("Horizontal2");
            var movement = new Vector2(moveHorizontal, 0);
            rb.AddForce(movement * speed);

            if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

                jump_sound.Play();
            }
        }

        // Apply the megnetic forces to the players
        var theOtherStatus = theOtherPlayer.Status;
        if (Status == Status.North && theOtherStatus == Status.North
            || Status == Status.South && theOtherStatus == Status.South)
        {
            // Push away
            var dir = transform.position - theOtherPlayer.transform.position;
            var dis = Vector2.Distance(transform.position, theOtherPlayer.transform.position);
            if (Claimed == false)
                AddForce(dir.normalized * Mathf.Clamp(50 / dis, 0, 15));
        }
        else if (Status == Status.North && theOtherStatus == Status.South
            || Status == Status.South && theOtherStatus == Status.North)
        {
            // Pull together
            var dir = theOtherPlayer.transform.position - transform.position;
            var dis = Vector2.Distance(transform.position, theOtherPlayer.transform.position);
            if (Claimed == false)
                AddForce(dir.normalized * Mathf.Clamp(50 / dis, 0, 15));
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steel : MonoBehaviour
{
    public Vector2 PushDirection = new Vector2(0, 1);
    public float PolarForceAmount = 50;
    public GameObject NorthEffect;

    SpriteRenderer spriteRenderer;

    Status m_status = Status.Neutral;
    public Status Status
    {
        get { return m_status; }
        set
        {
            m_status = value;
            if (m_status == Status.North)
            {
                NorthEffect.SetActive(true);
            }
            else if (m_status == Status.South)
            {
                spriteRenderer.color = Color.blue;
            }
            else
            {
                NorthEffect.SetActive(false);
                spriteRenderer.color = Color.white;
            }
        }
    }

    Player1 p1 = null;
    Player2 p2 = null;

    private void Awake()
    {
        p1 = FindObjectOfType<Player1>();
        p2 = FindObjectOfType<Player2>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (p1 == null || p2 == null) return;
        if (Status == Status.North)
        {
            if (p1.Status == Status.North)
            {
                // Push away
                var dir = PushDirection.normalized;
                var dis = Vector2.Distance(transform.position, p1.transform.position);
                p1.AddForce(dir * Mathf.Clamp(PolarForceAmount / dis, 0, 15));
            }
            else if (p1.Status == Status.South)
            {
                // Pull together
                var dir = -PushDirection.normalized;
                var dis = Vector2.Distance(transform.position, p1.transform.position);
                p1.AddForce(dir * Mathf.Clamp(PolarForceAmount / dis, 0, 15));
            }
            if (p2.Status == Status.North)
            {
                // Push away
                var dir = PushDirection.normalized;
                var dis = Vector2.Distance(transform.position, p2.transform.position);
                p2.AddForce(dir * Mathf.Clamp(PolarForceAmount / dis, 0, 15));
            }
            else if (p2.Status == Status.South)
            {
                // Pull together
                var dir = -PushDirection.normalized;
                var dis = Vector2.Distance(transform.position, p2.transform.position);
                p2.AddForce(dir * Mathf.Clamp(PolarForceAmount / dis, 0, 15));
            }
        }
        else if (Status == Status.South)
        {
            if (p1.Status == Status.South)
            {
                // Push away
                var dir = PushDirection.normalized;
                var dis = Vector2.Distance(transform.position, p1.transform.position);
                p1.AddForce(dir * Mathf.Clamp(PolarForceAmount / dis, 0, 15));
            }
            else if (p1.Status == Status.North)
            {
                // Pull together
                var dir = -PushDirection.normalized;
                var dis = Vector2.Distance(transform.position, p1.transform.position);
                p1.AddForce(dir * Mathf.Clamp(PolarForceAmount / dis, 0, 15));
            }
            if (p2.Status == Status.South)
            {
                // Push away
                var dir = PushDirection.normalized;
                var dis = Vector2.Distance(transform.position, p2.transform.position);
                p2.AddForce(dir * Mathf.Clamp(PolarForceAmount / dis, 0, 15));
            }
            else if (p2.Status == Status.North)
            {
                // Pull together
                var dir = -PushDirection.normalized;
                var dis = Vector2.Distance(transform.position, p2.transform.position);
                p2.AddForce(dir * Mathf.Clamp(PolarForceAmount / dis, 0, 15));
            }
        }
    }
}

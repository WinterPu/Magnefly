using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The supporting stick will break after too much force is added on to it.
/// </summary>
public class SupportingStick : Singleton<SupportingStick>
{
    [SerializeField] float hitPoint = 1000;
    Animator animator;
    public Animator supportedItemAnimator;
    Player1 p1;
    Player2 p2;
    public LayerMask supportedItemMask;

    private void Start()
    {
        animator = GetComponent<Animator>();
        p1 = Player1.Instance;
        p2 = Player2.Instance;
    }

    private void Update()
    {
        // Detect if the supported item is under the magnetic force
        // Cast a ray
        var p1p = p1.transform.position;
        var p2p = p2.transform.position;
        Vector2 dir;
        Vector2 lp;
        if (p1.Claimed)
        {
            dir = p2p - p1p;
            lp = p1p;
        }
        else
        {
            dir = p1p - p2p;
            lp = p2p;
        }
        dir = dir.normalized;

        var dis = Vector2.Distance(p1p, p2p);
        if (p1.Status != Status.Neutral && p2.Status != Status.Neutral && p1.Status != p2.Status)
        {
            if (dir.y > 0)
            {
                var hit = Physics2D.Raycast(lp, dir, dis, supportedItemMask);
                if (hit.collider != null)
                {
                    // The supported item is under the effect
                    Hit(Mathf.Clamp(50 / dis, 0, 15));
                }
            }
        }
    }

    public void Hit(float amount)
    {
        hitPoint -= Mathf.Abs(amount);
        if (hitPoint < 0)
        {
            Break();
        }
    }

    public void Break()
    {
        animator.SetTrigger("Break");
        supportedItemAnimator.SetTrigger("Fall");
    }
}

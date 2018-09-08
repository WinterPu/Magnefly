using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolarButton : MonoBehaviour {

    Animator animator;
    public Steel ControlledGaget;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Press();
        }
    }

    public void Press()
    {
        animator.SetTrigger("Press");
        ControlledGaget.Status = Status.North;

    }

}

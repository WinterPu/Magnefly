using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public AudioSource elevatorSound;
    bool won = false;

    public GameObject upperLight;
    public GameObject lowerLight;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        print(Global.CountOnTheElevator);

        if (Global.CountOnTheElevator == 1)
        {
            lowerLight.SetActive(true);
            upperLight.SetActive(false);
        }
        else if (Global.CountOnTheElevator >= 2)
        {
            // Win the level. 
            // Turn the green light on.
            lowerLight.SetActive(true);
            upperLight.SetActive(true);

            // Move up.
            if (!won)
            {
                animator.SetTrigger("Win");
                elevatorSound.Play();
                won = true;
            }

        }
        else
        {
            lowerLight.SetActive(false);
            upperLight.SetActive(false);
        }
    }

    IEnumerator MoveUpCoroutine()
    {
        yield return new WaitForSeconds(1);

    }
}

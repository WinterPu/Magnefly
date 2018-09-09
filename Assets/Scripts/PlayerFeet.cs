using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeet : MonoBehaviour {

    bool onElevator = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Elevator"))
        {
            if (onElevator) return;
            Global.CountOnTheElevator += 1;
            onElevator = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Elevator"))
        {
            if (!onElevator) return;
            Global.CountOnTheElevator -= 1;
            onElevator = false;
        }
    }
}

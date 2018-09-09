using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Elevator"))
        // Win
        LevelController.Instance.LoadLevel(0);
    }
}

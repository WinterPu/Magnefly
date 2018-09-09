using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject player01;
    public GameObject player02;
    public GameObject dead_side;


    //public GameObject prefab_player01;
    //public GameObject prefab_player02;


    public GameObject respawn_pos01;
    public GameObject respawn_pos02;

    public object GameController { get; private set; }

    // Use this for initialization
    void Start () {
	    	
	}
	
	// Update is called once per frame
	void Update () {
        if (JudgeDeath(player01) == true)
            RespawnPlayers(player01, respawn_pos01);


        if (JudgeDeath(player02) == true)
            RespawnPlayers(player02, respawn_pos02);
    }



    private bool JudgeDeath(GameObject player) {
        if (player.transform.position.y <= dead_side.transform.position.y)
        {
            return true;

        }
        else
            return false;
    }

    float m_timer = 0;
    private void RespawnPlayers(GameObject player,GameObject respawn_pos) {
        //GameObject new_player;
        //new_player = Instantiate(prefab_player01, respawn_pos01.transform.position, Quaternion.identity);
        //Destroy(player01);
        //player01 = new_player;
        Debug.Log("Start");
       // Time.timeScale = 0;
        Debug.Log("nice");
        // yield return new WaitForSeconds(3);
        m_timer += Time.time;
        if (m_timer >= 500)
        {
            Respawn(player, respawn_pos);
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            m_timer = 0;
        }

    }


    void Respawn(GameObject player, GameObject respawn_pos)
    {

        Debug.Log("Oh1");
        player.transform.position = respawn_pos.transform.position;
      //  Time.timeScale = 1;

    }
}

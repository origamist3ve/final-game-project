using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private int players_entered = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // On collision enter if tag is Player load Level 1
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // destroy the player
            Destroy(collision.gameObject);
            players_entered++;
            if (players_entered == 2)
            {
                Application.LoadLevel("Level 1");
            }
        }
    }
}

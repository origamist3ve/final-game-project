using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Behaviour : MonoBehaviour
{
    public Camera CameraPlayer1;
    public GameObject magnetCollider;
    public float speed = 5;
    public float jump_force = 5;
    private bool grounded = true;

    private bool magnetActive = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // WASD movement
        
        // Jump
        if (Input.GetKey(KeyCode.W) && grounded)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jump_force);
        }

        // Down
        if (Input.GetKey(KeyCode.S))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -speed);
        }

        // Left
        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
        }

        // Right
        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
        }

        // Stop
        if (!(Input.GetKey(KeyCode.A) ^ Input.GetKey(KeyCode.D)))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        }
        
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player2Behaviour : MonoBehaviour
{
    public Camera CameraPlayer1;
    public float speed = 5;
    public float jump_force = 5;


    public GameObject magnetCollider;
    private bool magnetActive = true;

    // Controls
    private Dictionary<string, KeyCode> keyCodes = new Dictionary<string, KeyCode>();

    private bool grounded = true;



    // Start is called before the first frame update
    void Start()
    {
        // set up movement controls
        keyCodes.Add("up", KeyCode.UpArrow);
        keyCodes.Add("left", KeyCode.LeftArrow);
        keyCodes.Add("down", KeyCode.DownArrow);
        keyCodes.Add("right", KeyCode.RightArrow);
        keyCodes.Add("magnet", KeyCode.RightShift);

        // Deactivate magnet
        magnetActive = !magnetActive;
        magnetCollider.SetActive(magnetActive);
    }

    // Update is called once per frame
    void Update()
    {
        // Jump
        if (Input.GetKey(keyCodes["up"]) && grounded)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jump_force);
        }

        // Down
        if (Input.GetKey(keyCodes["down"]))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -speed);
        }

        // Left
        if (Input.GetKey(keyCodes["left"]))
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
        }

        // Right
        if (Input.GetKey(keyCodes["right"]))
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
        }

        // Stop
        if (!(Input.GetKey(keyCodes["left"]) ^ Input.GetKey(keyCodes["right"])))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        }


        // if E is pressed, activate/deactivate magnet
        if (Input.GetKeyDown(keyCodes["magnet"]))
        {
            magnetActive = !magnetActive;
            magnetCollider.SetActive(magnetActive);
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

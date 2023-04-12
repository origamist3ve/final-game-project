using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class PressurePlate : MonoBehaviour
{
    public GameObject door;
    public GameObject stopping_point;

    private bool active;
    private Vector2 starting_position;

    // Start is called before the first frame update
    void Start()
    {
        // save starting position
        starting_position = door.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (active)
        {
            // move the door up to the stopping point, then stop
            if (door.transform.position.y < stopping_point.transform.position.y)
            {
                door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y + 0.01f, door.transform.position.z);
            }
        }
        else
        {
            if (door.transform.position.y > starting_position.y+0.1)
            door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y - 0.1f, door.transform.position.z);
        }
    }

    // On trigger enter 2d

    void OnTriggerStay2D(Collider2D collision)
    {
        // if tag is 'Box' active = true;
        if (collision.gameObject.tag == "Box")
        {
            active = true;
        }
    }

    // On trigger exit 2d
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            active = false;
        }
    }
}

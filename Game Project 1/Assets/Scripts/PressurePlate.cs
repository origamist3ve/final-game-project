using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject door;
    public GameObject stopping_point;
    public int direction = 1;

    private bool active;
    private Vector2 starting_position;

    // Start is called before the first frame update
    void Start()
    {
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
            if (door.transform.position.y < stopping_point.transform.position.y)
            {
                door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y + 0.01f*direction, door.transform.position.z);
            }
        }
        else
        {
            if (door.transform.position.y > starting_position.y+0.1)
            door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y - 0.1f*direction, door.transform.position.z);
        }
    }


    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            active = true;
        }
    }


    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            active = false;
        }
    }
}

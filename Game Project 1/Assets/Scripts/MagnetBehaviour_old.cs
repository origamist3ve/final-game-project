using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBehaviour_old : MonoBehaviour
{
    public float charge;
    public float distance_threshhold;

    public float center_x_offset;
    public float center_y_offset;
    public GameObject centerMarker;
    public bool showCenterMarker;

    // Start is called before the first frame update
    void Start()
    {
        if (showCenterMarker && centerMarker != null)
        {
            Vector2 magnetCenter = new Vector2(transform.position.x + center_x_offset, transform.position.y + center_y_offset);
            centerMarker.transform.position = magnetCenter;
            centerMarker.SetActive(true);
        }
        else if (centerMarker != null)
        {
            centerMarker.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // On 2d collision stay
    void OnTriggerStay2D(Collider2D collision)
    {
        // Check if the object is charged
        if (collision.gameObject.GetComponent<Charged>() != null)
        {
            Vector2 magnetCenter = new Vector2(transform.position.x + center_x_offset, transform.position.y + center_y_offset);

            // If distance is less than distance_threshhold, then the force is 0
            if (Vector2.Distance(magnetCenter, collision.gameObject.transform.position) > distance_threshhold)
            {
                // Modify the force calculation to take into account center offset
                Vector2 force = CalculateMagneticForce(magnetCenter, charge, collision.gameObject.transform.position, collision.gameObject.GetComponent<Charged>().charge, 1);

                // Log force
                Debug.Log(force);
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(force);
            }
        }
    }

    public Vector2 CalculateMagneticForce(Vector2 position1, float charge1, Vector2 position2, float charge2, float k)
    {
        Vector2 direction = (position2 - position1).normalized;
        float distance = Vector2.Distance(position1, position2);
        float forceMagnitude = k * (charge1 * charge2) / (distance * distance);
        Vector2 magneticForce = forceMagnitude * direction;
        return magneticForce;
    }
}

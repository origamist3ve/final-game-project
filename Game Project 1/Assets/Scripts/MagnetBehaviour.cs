using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBehaviour : MonoBehaviour
{
    public float charge;
    public float distanceExponent;  // Change this value to modify the rate of change of the force with respect to the distance
    public float distance_threshhold;

    public float center_x_offset;
    public float center_y_offset;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            float halfWidth = spriteRenderer.bounds.size.x / 2;
            float halfHeight = spriteRenderer.bounds.size.y / 2;
            center_x_offset = -halfWidth;
            center_y_offset = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Flip the offset with the player
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            float halfWidth = spriteRenderer.bounds.size.x / 2;
            float halfHeight = spriteRenderer.bounds.size.y / 2;

            if (transform.parent.localScale.x > 0)
            {
                center_x_offset = -halfWidth;
                center_y_offset = 0;
            }
            else
            {
                center_x_offset = halfWidth;
                center_y_offset = 0;
            }
        }
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
                // Modifying the force calculation to take into account center offset
                Vector2 force = CalculateMagneticForce(magnetCenter, charge, collision.gameObject.transform.position, collision.gameObject.GetComponent<Charged>().charge, 1);

                Debug.Log(force);
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(force);
            }
        }
    }

    public Vector2 CalculateMagneticForce(Vector2 position1, float charge1, Vector2 position2, float charge2, float k)
    {
        Vector2 direction = (position2 - position1).normalized;
        float distance = Vector2.Distance(position1, position2);
        float forceMagnitude = k * (charge1 * charge2) / Mathf.Pow(distance, distanceExponent);
        Vector2 magneticForce = forceMagnitude * direction;
        return magneticForce;
    }
}

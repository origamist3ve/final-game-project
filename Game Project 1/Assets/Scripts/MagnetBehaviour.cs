using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBehaviour : MonoBehaviour
{
    
    public float charge;
    public float distance_threshhold;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    // On 2d collision enter
    void OnTriggerStay2D(Collider2D collision)
    {
        // Check if the object is charged
        if (collision.gameObject.GetComponent<Charged>() != null)
        {
            // derive a formula for the magnet force based on the distance between the magnet and the charged object and the charge of the magnet and the charged object (acting like real magnets, so negative charge would attract positive and repel negative, also the value of the charge should affect the strength of the force)
            // if distance is less than distance_threshhold, then the force is 0
            if (Vector2.Distance(transform.position, collision.gameObject.transform.position) > distance_threshhold)
            {
                Vector2 force = CalculateMagneticForce(transform.position, charge, collision.gameObject.transform.position, collision.gameObject.GetComponent<Charged>().charge, 1);
                // log force
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

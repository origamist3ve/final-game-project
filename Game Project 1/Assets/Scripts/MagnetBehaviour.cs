using UnityEngine;

public class MagnetBehaviour : MonoBehaviour
{
    public float charge;
    public float distanceExponent;  // Change this value to modify the rate of change of the force with respect to the distance
    public float distance_threshhold;
    public GameObject target; // The origin of the magnetic force (if it's Player's object then it should be Player)

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            float halfWidth = spriteRenderer.bounds.size.x / 2;
            float halfHeight = spriteRenderer.bounds.size.y / 2;
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

        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        // Check if the object is charged
        if (collision.gameObject.GetComponent<Charged>() != null)
        {
            // detect if target is touching the collision object (if so, don't apply force)

            Collider2D target_collider = target.GetComponent<Collider2D>();
            Collider2D collision_collider = collision.GetComponent<Collider2D>();
            
            if (!target_collider.IsTouching(collision_collider))
            {
                // If distance is less than distance_threshhold, then the force is 0
                if (Vector2.Distance(target.transform.position, collision.gameObject.transform.position) > distance_threshhold)
                {
                    Vector2 force = CalculateMagneticForce(target.transform.position, charge, collision.gameObject.transform.position, collision.gameObject.GetComponent<Charged>().charge, 1);

                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(force);
                }
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

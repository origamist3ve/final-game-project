using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float force;
    private Dictionary<Rigidbody2D, Vector2> previousVelocities = new Dictionary<Rigidbody2D, Vector2>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody != null)
        {
            previousVelocities[collision.attachedRigidbody] = collision.attachedRigidbody.velocity;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        previousVelocities.Remove(collision.attachedRigidbody);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.collider.attachedRigidbody;
        if (previousVelocities.ContainsKey(rb))
        {
            Vector2 forceVector = Vector2.up * force * Mathf.Abs(previousVelocities[rb].y);
            collision.rigidbody.AddForce(forceVector, ForceMode2D.Impulse);
            Debug.Log(forceVector);
        }
        else
        {
            Debug.Log("Nope");
        }
    }
}

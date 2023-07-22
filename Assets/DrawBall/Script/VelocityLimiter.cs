using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityLimiter : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    public float maxVelocity;


    private void FixedUpdate()
    {
        if (AtMaxVelocity()) // Prevents crazy bouncyPath collision interactions
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity); // If velocity is more than maxVelocity, sets velocity to the maximum set value. 
        }
    }

    private bool AtMaxVelocity()
    {
        if (rb.velocity.sqrMagnitude < maxVelocity)
        {
            return false;
        }

        else
        {
            return true;
        }

    }
}

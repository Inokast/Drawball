using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    [Header("Layer Mask")]
    public LayerMask collisionLayer;

    [Header("Collision")]

    public float collisionRadius;
    public Vector2 bottomOffset, rightOffset, leftOffset;
    public bool onGround;
    private Color debugCollisionColor = Color.red;

    
    void OnDrawGizmos() // Gizmo helps vizualize collider
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
    }

    // Update is called once per frame
    void Update() // script accurately checks if player is touching a valid "ground" object to allow DragNShoot mechanic
    {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, collisionLayer) ||
            Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, collisionLayer) ||
            Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, collisionLayer); //Checks if colliding with objects on designated layermask
    }
}

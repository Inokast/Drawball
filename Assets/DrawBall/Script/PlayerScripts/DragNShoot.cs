using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNShoot : MonoBehaviour
{
    private Rigidbody2D rb;

    private Collision playerCol;

    [Header("Drag Launching")]
    public float power = 10f;
    public Vector2 minPower;
    public Vector2 maxPower;
    private Vector2 force;

    private Vector3 startPoint;
    private Vector3 endpoint;

    private LaunchLine launchLine;

    private bool launchReady;

    [Header("Launch & Fall Physics")]


    public float maxVelocity;
    public float fallMultiplier;
    
    [SerializeField] Camera cam;

    private SoundFXController sfx;

    private void Awake()
    {
        sfx = FindObjectOfType<SoundFXController>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCol = GetComponent<Collision>();
        launchLine = GetComponent<LaunchLine>();
        launchReady = false;
    }

    private void Update()
    {

        if (rb.velocity.y < 3 && !playerCol.onGround)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime; // More satisfying falls
        }

        if (Input.GetMouseButtonDown(0))
        {

            startPoint = cam.ScreenToWorldPoint(Input.mousePosition); //Sets start point of the drag
            startPoint.z = 15;

            Vector2 distanceFromPlayer = startPoint - gameObject.transform.position;
            if (distanceFromPlayer.magnitude <= 10)
            {
                launchReady = true;
            }

            
        }

        if (Input.GetMouseButton(0))
        {
            if (launchReady == true) 
            {
                Vector3 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                currentPoint.z = 15;
                launchLine.RenderLine(startPoint, currentPoint);
            }
        }

        if (Input.GetMouseButtonUp(0)) //Sets end point of the drag and calculates how much force to add
        {
            if (launchReady == true) 
            {
                endpoint = cam.ScreenToWorldPoint(Input.mousePosition);
                endpoint.z = 15;

                force = new Vector2(Mathf.Clamp(startPoint.x - endpoint.x, minPower.x, maxPower.x), Mathf.Clamp(startPoint.y - endpoint.y, minPower.y, maxPower.y)); //Calculates launch force

                if (playerCol.onGround) // Only launches if player is on the ground
                {
                    rb.AddForce(force * power, ForceMode2D.Impulse);
                    sfx.PlayLaunch();
                }

                launchLine.EndLine();
                launchReady = false;
            }
            
        }
    }

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

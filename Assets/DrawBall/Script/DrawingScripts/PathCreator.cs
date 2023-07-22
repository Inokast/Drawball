using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreator : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Animator meterAnimator;
    [SerializeField] DragNShoot player;

    [Header("Path Line Prefabs")]
    public GameObject[] pathPrefabs;
    private GameObject pathType;

    [Header("Path Stamina Meters")]
    [SerializeField] PathMeter[] pathMeters;
    private PathMeter activeMeter;

    private Path activePath;

   private Vector2 playerPos;
   private Vector2 distanceFromPlayer;
   private Vector2 mousePos;
   private bool pathStarted;

    Ray ray;
    RaycastHit2D hit;

    private void Start()
    {
        pathStarted = false;
        activeMeter = pathMeters[0];
        pathType = pathPrefabs[0];
        meterAnimator.SetInteger("activeMeter", 0);
        player = FindObjectOfType<DragNShoot>();
    }

    private void Update()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);
        hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

        /// If Playing on PC

        if (Input.GetKeyDown("space") && !Input.GetMouseButton(1))
        {
            ToggleMeter();
        }

        


        if (Input.GetMouseButtonDown(1)) // Starts path at click or tap
        {
            if (pathStarted == false)
            {
                mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                playerPos = new Vector2(player.gameObject.transform.position.x, player.gameObject.transform.position.y);
                distanceFromPlayer = mousePos - playerPos;

                if (distanceFromPlayer.magnitude > 1) // Checks if screen tap was far enough away from player
                {
                    pathStarted = true;
                }
            }

            if (hit.collider == null && pathStarted == true) // If player tapped too close to the ball or an occupied space, the line does not start
            {
                if (activeMeter.currentStamina >= 1)
                {
                    GameObject pathGO = Instantiate(pathType);
                    activePath = pathGO.GetComponent<Path>();
                }                
            }           
        }

        if (Input.GetMouseButtonUp(1)) // Ends path when held left click ends
        {
            EndPath();          
        }

        if (activePath != null) 
        {
            
            if (hit.collider == null || hit.collider.gameObject.tag == "LinePath" || hit.collider.gameObject.tag == "BouncePath") // Ends path early if it collides with invalid object
            {
                if (activeMeter.currentStamina >= 1)
                {
                    mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                    activePath.UpdateLine(mousePos, activeMeter);
                }

                else
                {
                    EndPath();
                }
                
            }

            else
            {
                EndPath();
            }

        }
        
    }

    public void ToggleMeter() // Switch active meters
    {
        if(activeMeter == pathMeters[0])
        {
            activeMeter = pathMeters[1];
            pathType = pathPrefabs[1];
            meterAnimator.SetInteger("activeMeter", 1);
        }

        else if (activeMeter == pathMeters[1])
        {
            activeMeter = pathMeters[0];
            pathType = pathPrefabs[0];
            meterAnimator.SetInteger("activeMeter", 0);
        }
    }

    private void EndPath()
    {
        activePath = null;
        pathStarted = false;
    }
}

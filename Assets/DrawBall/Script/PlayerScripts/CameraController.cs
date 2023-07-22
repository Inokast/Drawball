using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float cameraSpeed = 0.125f;
    public float verticalOffset = 0;
    private Vector3 cameraPos;
    private Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void FixedUpdate() // Smooth camera movement following player
    {
        cameraPos = new Vector3(player.position.x, player.position.y + verticalOffset, -10f);
        transform.position = Vector3.Lerp(gameObject.transform.position, cameraPos, cameraSpeed);
    }



}

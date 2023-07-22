using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player")) 
        {
            bool haspowerup = col.gameObject.GetComponent<PlayerStats>().CheckForPowerup();
            if (haspowerup == true) 
            {
                Destroy(gameObject);
            }

            else
            {
                FindObjectOfType<UIController>().Restart();
            }
        }
        
    }
    
}

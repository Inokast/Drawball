using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class Collectible : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 90) * Time.deltaTime);
    }

    private void Collected()
    {
        if (gameObject.tag == "Powerup") 
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().color = Color.black;
            StartCoroutine(RespawnTimer());
        }
        else
        {
            Destroy(gameObject);
        }      
    }

    IEnumerator RespawnTimer() 
    {
        yield return new WaitForSeconds(8);
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            if (gameObject.tag == "Star")
            {
                col.gameObject.GetComponent<PlayerStats>().CollectStar();
                Collected();
            }

            else if (gameObject.tag == "Powerup") 
            {
                col.gameObject.GetComponent<PlayerStats>().CollectPowerup();
                Collected();
            }

            
        }        
    }
}

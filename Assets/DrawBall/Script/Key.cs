using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 90) * Time.deltaTime);
    }

    private void Collected()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if(col.gameObject.GetComponent<PlayerStats>().CheckForKey() == false)
            {
                col.gameObject.GetComponent<PlayerStats>().CollectKey();
                Collected();
            }           
        }
    }


}

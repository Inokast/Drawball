/// Dan Sanchez
/// Drawball Mobile

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] UIController uiController;
    [SerializeField] Rigidbody2D rb;

    private int starCount = 0;
    [SerializeField] private int starGoal = 5;
    [SerializeField] private float powerupLength = 5;

    private bool collectedStars;
    private bool hasKey;
    private bool hasPowerup;

    private SoundFXController sfx;
    private BGMController bgm;

    private void Awake()
    {
        sfx = FindObjectOfType<SoundFXController>();
        bgm = FindObjectOfType<BGMController>();
    }

    public void CollectStar()
    {
        starCount++;
        sfx.PlayCollectStar();
        uiController.UpdateScore(starCount);
        CheckForGoal();
    }

    public void CollectPowerup()
    {
        // Powerup effects
        sfx.PlayCollectStar();
        hasPowerup = true;
        StartCoroutine(PowerupStart());
    }

    IEnumerator PowerupStart() 
    {
        bgm.PlayPowerupyMusic();
        yield return new WaitForSeconds(powerupLength);
        hasPowerup = false;
        bgm.PlayLevelMusic();
    }

    private void CheckForGoal()
    {
        if (starCount == starGoal)
        {
            collectedStars = true;
            uiController.ColorScore();
        }
    }

    public void CollectKey()
    {
        sfx.PlayCollectKey();
        hasKey = true;
        uiController.ShowKey();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Door")
        {
            if (hasKey == true)
            {
                sfx.PlayUnlock();
                Destroy(col.gameObject);
                hasKey = false;
                uiController.HideKey();
            }

            else
            {
                sfx.PlayThunk();
            }
        }

        if (col.gameObject.tag == "EndDoor")
        {
            if(collectedStars == true)
            {
                sfx.PlayUnlock();
                ClearLevel();
            }

            else
            {
                sfx.PlayThunk();
            }
        }

        if (col.gameObject.tag == "Destructible")
        {
            sfx.PlayThunk();
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "BounceDestructible")
        {
            sfx.PlayBounce();
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "BouncePath")
        {
            sfx.PlayBounce();
        }

        if (col.gameObject.tag == "Ground")
        {
            sfx.PlayThunk();
        }
    }

    public bool CheckForKey()
    {
        if (hasKey == true)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    public bool CheckForPowerup()
    {
        if (hasPowerup == true)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    private void ClearLevel()
    {
        uiController.NextLevel();
    }
}

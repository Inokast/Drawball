using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathMeter : MonoBehaviour
{
    [Header("Slider To Use As Meter")]
    public Slider meter;

    [Header("Meter Values")]
    [SerializeField] int maxStamina = 300;
    public int currentStamina;
    [SerializeField] float rechargeRate = .1f;

    private Coroutine regenerate; // Allows for checking if coroutine is currently running

    private void Start()
    {
        currentStamina = maxStamina;
        meter.maxValue = maxStamina;
        meter.value = maxStamina;

    }

    public void UseStamina(int amount) // Reduces meter's value according to usage amount
    {
        if (currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            meter.value = currentStamina;

            if (regenerate != null)
            {
                StopCoroutine(regenerate);
            }

            regenerate = StartCoroutine(RegenMeter());
        }

        else
        {
            StopCoroutine(regenerate);
            regenerate = StartCoroutine(RegenMeter());
        }
    }

    private IEnumerator RegenMeter() // Recharges meter's value on a timer 
    {
        yield return new WaitForSeconds(3);

        while(currentStamina < maxStamina)
        {
            currentStamina += maxStamina / 100;
            meter.value = currentStamina;
            yield return new WaitForSeconds(rechargeRate);
        }

        regenerate = null;
    }
}

using UnityEngine;

public class SafeZoneLight : MonoBehaviour
{
    public float maxIntensity;
    public float flashSpeedMultiplier;
    public Light thisLight;
    
    private bool dimming;

    void Update()
    {
        if (thisLight.intensity <= 0)
            dimming = false;
        if (thisLight.intensity >= maxIntensity)
            dimming = true;

        if (dimming)
        {
            thisLight.intensity -= Time.deltaTime * flashSpeedMultiplier;
        }
        else
        {
            thisLight.intensity += Time.deltaTime * flashSpeedMultiplier;
        }
    }
}

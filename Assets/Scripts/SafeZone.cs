using UnityEngine;

public class SafeZone : MonoBehaviour
{
    public Light lightStats;
    public float flashSpeed;
    public int charges;
    public bool playerTouching;
    
    private bool dimming;
    private float maxIntensity;

    private void Start()
    {
        maxIntensity = lightStats.intensity;
        playerTouching = false;
    }
    
    private void OnEnable()
    {
        playerTouching = false;
    }

    public void ExpendCharge()
    {
        charges--;
        if (charges <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (dimming)
        {
            lightStats.intensity -= Time.deltaTime * flashSpeed;
            if (lightStats.intensity <= 0)
            {
                dimming = false;
            }
        }
        else
        {
            lightStats.intensity += Time.deltaTime * flashSpeed;
            if (lightStats.intensity >= maxIntensity)
            {
                dimming = true;
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player is touching a safe zone.");
            playerTouching = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player stopped touching a safe zone.");
            playerTouching = false;
        }
    }
}

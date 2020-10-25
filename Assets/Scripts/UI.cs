using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public TMP_Text timer;
    public TMP_Text enemiesKilled;
    public TMP_Text fragmentsGathered;
    public TMP_Text hitPoints;

    void Start()
    {
        UpdateTimer(0);
        UpdateHitPoints(3);
        UpdateFragments(0);
        UpdateEnemiesKilled(0);
    }

    public void UpdateTimer(int value)
    {
        timer.text = value.ToString();
    }
    
    public void UpdateHitPoints(int value)
    {
        hitPoints.text = value.ToString();
    }

    public void UpdateFragments(int value)
    {
        fragmentsGathered.text = value.ToString() + "/50";
    }
    
    public void UpdateEnemiesKilled(int value)
    {
        enemiesKilled.text = value.ToString();
    } 
}

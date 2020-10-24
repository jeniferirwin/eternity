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
        timer.text = "Flood Timer: " + value;
    }
    
    public void UpdateHitPoints(int value)
    {
        hitPoints.text = "HP: " + value;
    }

    public void UpdateFragments(int value)
    {
        fragmentsGathered.text = "Fragments Gathered: " + value;
    }
    
    public void UpdateEnemiesKilled(int value)
    {
        enemiesKilled.text = "Enemies Killed: " + value;
    } 
}

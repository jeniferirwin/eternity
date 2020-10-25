using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject[] tutorialCards;
    public float tickerMax;

    private float ticker;
    private int card = 0;

    void OnEnable()
    {
        ticker = tickerMax;
        DisplayCard(0);
    }

    void Update()
    {
        if (ticker > 0f)
        {
            ticker -= Time.deltaTime;
        }
        else
        {
            ticker = tickerMax;
            card++;
            if (card < tutorialCards.Length)
            {
                HideCard(card - 1);
                DisplayCard(card);
            }
            else
            {
                HideCard(card - 1);
                gameObject.SetActive(false);
            }
        }

        
    }
    
    private void HideCard(int num)
    {
        tutorialCards[num].SetActive(false);    
    }

    private void DisplayCard(int num)
    {
        tutorialCards[num].SetActive(true);
    }
}

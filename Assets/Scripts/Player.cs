using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float varHunger;
    public Image HungerImage;

    public enum playerHungerStats
    {
        Starving,
        Neutral,
        Full
    }
    public playerHungerStats varHungerStats;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HungerTime();
        UpdateUI();
        checkStats();
    }

    public void HungerTime()
    {
        varHunger -= 1 * Time.deltaTime;

        if (varHunger <= 30)
        {
            varHungerStats = playerHungerStats.Starving;
        } else if (varHunger <= 60)
        {
            varHungerStats = playerHungerStats.Neutral;
        } else
        {
            varHungerStats = playerHungerStats.Full;
        }
    }

    public void UpdateUI()
    {
        HungerImage.fillAmount = (varHunger / 100);
    }

    public void checkStats()
    {
        switch (varHungerStats)
        {
            case playerHungerStats.Neutral:
                print("Masih Bertenaga");
                break;
            case playerHungerStats.Starving:
                print("Aku Lapar");
                break;
            default:
                print("Aku Kenyang");
                break;
        }
    }
}

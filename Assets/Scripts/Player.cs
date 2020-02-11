using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    public float varHunger;
    public Image HungerImage;
    public Rigidbody2D myRigidbody2D;
    public Animator myAnimator;

    
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HungerTime();
        UpdateUI();
        checkStats();
        Run();
        FlipSprite();
    }

    public enum playerHungerStats
    {
        Starving,
        Neutral,
        Full
    }
    public playerHungerStats varHungerStats;

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

    public void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidbody2D.velocity.y);
        myRigidbody2D.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    public float runSpeed = 2f;

    public void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody2D.velocity.x), 1f);
        }
    }

    public void lelah()
    {

    }
}

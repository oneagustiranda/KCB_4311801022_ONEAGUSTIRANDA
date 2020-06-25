using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    public float runSpeed = 10f;
    public float jumpSpeed = 10f;
    public float varHunger;
    public Image HungerImage;
    public Text gameOver;
    public static string playerTool;
    public Rigidbody2D myRigidbody2D;
    public Animator myAnimator;
    public Collider2D myCollider2D;
    public GameObject panelGameOver;
    
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HungerTime();
        UpdateUI();
        checkStats();
        Run();
        FlipSprite();
        Jump();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Food")
        {
            print("Ada Daging");
            varHunger += 20;
            Destroy(other.gameObject);
        } else if(other.gameObject.tag == "Poison")
        {
            print("Ada Daging Busuk");
            varHunger -= 20;
            Destroy(other.gameObject);
        }
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

        if (varHunger <= 0)
        {
            Destroy(gameObject);
            panelGameOver.SetActive (true);   
        }
        else if (varHunger <= 30)
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

    public void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody2D.velocity.x), 1f);
        }
    }

    public void Jump()
    {
        if (!myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if (CrossPlatformInputManager.GetButtonDown ("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(5f, jumpSpeed);
            myRigidbody2D.velocity += jumpVelocityToAdd;
        }
    }
}

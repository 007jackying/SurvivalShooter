using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/**
 * class for counting extralives and time interval to give player an extralife.
 * @author Teng Hong Lee (201723459)
 **/
public class ExtraReward : MonoBehaviour
{
   
    public int noOneUps =0; // number of 1UPs (number of extra lives)
    public int reward; //The milestone that player has to hit (score == reward) then gain one extra life
    private float flashTimer; //Timer for blinking text
    private bool flash; // if flash is true, the blinking text is enabled.
    const int rewardInterval =20; 
    const int multiplier = 5;
    //ScoreManager scores;

    PlayerHealth playerHealth; 
    GameObject player; 
    Text noOfLives; //text will be printed on screen to indicate how many extralife (maximum 3)
    AudioSource lifeGain; //sound effect when player gain an extra life
    
    
    // Update is called once per frame


    void Awake()
    {
        noOneUps = 0;
        reward = rewardInterval;
        player = GameObject.FindGameObjectWithTag("Player");
        noOfLives = GetComponent<Text>();
        //scores = GetComponent<ScoreManager>();
        playerHealth = player.GetComponent<PlayerHealth>();
        lifeGain = GetComponent<AudioSource>();
    }

    /*add one extra life to player if player has earned more than certain score, 
     * then increase the next target score that player need to reach to get another extra live.
     * @param none
     * @void
    */
void AddReward()
    {
        lifeGain.Play();// sound effect will be played when player gain an extra life.
        noOneUps++;// extra life increase by one
        reward = reward * multiplier; //the next score player has to reach to gain an extra life.

        
    }
    /*
     *This function will be called by PlayerHealth.cs, when player lose one life while he/she still having at least one extralife.
     * Once this function is called, it will decrement the noOneUps by 1 if the currrent value is >1. 
     * If the value of noOneUps is 0, it will be assign to zero(for easier of tracing in the future).
     @param : none
     @void
         */
    public void Reborn()
    {
        if (noOneUps == 0)
        {
            noOneUps = 0;
        }
        if (noOneUps > 0)
        {
            noOneUps--;
        }

    }

    void Update()
    {
        // blinking text
        if (flash == true) {
            flashTimer += Time.deltaTime;// Timer will start running begining at this point
        }
        //check if the player's score has reach the next reward state.
        if (ScoreManager.score >= reward && noOneUps < 3)//player can only hold 3 extra lives maximum.
        {
            AddReward();
            noOfLives.enabled = false;
            flash = true;
            //print("scores : " + ScoreManager.score);
        }
        if (flashTimer >= 0.5)
        {
            noOfLives.enabled = true;
        }
        if (flashTimer >= 1)
        {
            noOfLives.enabled = false;

        }
        if (flashTimer >= 1.5)
        {
            noOfLives.enabled = true;
            flashTimer = 0;
            flash = false;
        }
        noOfLives.text = "Lives: " + noOneUps; //show number of extra life is the player holding in this current frame.

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/*
 * This class will be showing a hint to player how many left score to achieve the next extra life.
 * @author Teng Hong Lee (201723459)
 */
public class Milestones : MonoBehaviour
{
    GameObject extraLivesCanvas;
    ExtraReward extraLives;
    Text nextMilestone;
    int scoreRequired;

    // Start is called before the first frame update
    void Awake()
    {
        //initializing gameObject from ExtraLives UI entity(shown under HUDCanvas in the Hierarchy)
        extraLivesCanvas = GameObject.FindGameObjectWithTag("ExtraLife");
        extraLives = extraLivesCanvas.GetComponent<ExtraReward>();
        nextMilestone = GetComponent<Text>();
        scoreRequired = extraLives.reward;

    }

    // Update is called once per frame
    void Update()
    {
        scoreRequired = extraLives.reward;
        //if player is already holding 3 extra lives, player the message will not be printed on the screen, until player has lose one life.
        if (extraLives.noOneUps == 3)
        {
            nextMilestone.text = " ";
        }
        else
        {
            nextMilestone.text = "Next extra life at score: " + scoreRequired; //giving hint for player when is the next extra life will be rewarded.
        }
        

    }
}

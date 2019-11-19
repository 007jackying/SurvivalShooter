using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    GameObject extraLivesCanvas;
    ExtraReward extraLives;

    public PlayerHealth playerHealth;
    public TimerManager timerManager;
    public Text goText;
    Animator anim;
    private GameObject go;
    private TimerManager tm;

    void Awake()
    {
        go = GameObject.Find("TimerManager");
        anim = GetComponent<Animator>();
        //populating the object from ExtraReward.cs.
        extraLivesCanvas = GameObject.FindGameObjectWithTag("ExtraLife");
        extraLives = extraLivesCanvas.GetComponent<ExtraReward>();
    }


    void Update()
    {
        tm = (TimerManager)go.GetComponent(typeof(TimerManager));
        //checking if the player have any extra life left while the player 
        //current health is equal to zero.
        if (playerHealth.currentHealth <= 0 && extraLives.noOneUps == 0)
        {
            goText.text = "Game Over";
            anim.SetTrigger("GameOver");
        }
        if (tm.isComplete == true)
        {
            goText.text = "Congratulation ! Level Complete";
            goText.fontSize = 40;
            anim.SetTrigger("GameOver");
        }
    }
}
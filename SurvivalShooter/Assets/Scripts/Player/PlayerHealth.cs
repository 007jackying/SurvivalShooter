using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    private GameObject go;
    private TimerManager tm;

    Animator anim;
    AudioSource playerAudio;
    GameObject extraLivesCanvas;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    ExtraReward extraLives;
    Respawn repawner;
    bool isDead;
    bool damaged;


    void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
        go = GameObject.Find("TimerManager");
        currentHealth = startingHealth;


        //populating Gameobject from ExtraReward.cs and Respawn.cs
        extraLivesCanvas = GameObject.FindGameObjectWithTag("ExtraLife");
        extraLives = extraLivesCanvas.GetComponent<ExtraReward>();
        repawner = GetComponent<Respawn>();
    }


    void Update()
    {

        tm = (TimerManager)go.GetComponent(typeof(TimerManager));
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }


    public void TakeDamage(int amount)
    {
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        playerAudio.Play();

        //if player current health is less than or equal to zero and player still holding any extralife,
        //function DieAndRespawn will be called.
        if (currentHealth <= 0 && !isDead && extraLives.noOneUps >= 1)
        {
            DieAndRespawn();
        }
        else if (currentHealth <= 0 && !isDead && extraLives.noOneUps == 0)
        {
            Death();
        }
    }

    /*
     * This function will be triggering the animation of the Player model.
     * Death animation will be played, and player will be respawned on the origin(player spawn point),
     * which means, Reborn() will be called from the ExtraReward.cs for decrement the current extra life
     * and RespawnPlayer() will also be called from the Respawn.cs for setting player position to the spawn point.
     */
    public void DieAndRespawn()
    {
        isDead = true;
        anim.SetTrigger("ExtraLife");//changing state from anystate to extralife state(please refer Player animator in Unity)
        extraLives.Reborn();
        repawner.RespawnPlayer();
        isDead = false;
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
        anim.SetTrigger("Respawn");//Changing state from Extralfe to Idle state(please refer to Player animator in Unity )
        playerShooting.EnableEffects();
        playerMovement.enabled = true;
        playerShooting.enabled = true;


    }




    void Death()
    {
        tm.Finnish();
        isDead = true;

        playerShooting.DisableEffects();

        anim.SetTrigger("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play();

        playerMovement.enabled = false;
        playerShooting.enabled = false;

    }


    public void RestartLevel()
    {
        tm.levelRestart = true;
        tm.reset();
        SceneManager.LoadScene(0);
    }
}

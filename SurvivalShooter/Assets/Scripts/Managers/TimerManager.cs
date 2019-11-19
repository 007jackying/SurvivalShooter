using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * This class will keep track of timer in the game.
 * @author: Utsav Ashish Koju (201763299)
 **/

public class TimerManager : MonoBehaviour
{
    public Text timerText;
    public Text waveText;
    public Text waveHint;
    public string endTime;
    private GameObject zbu;
    private GameObject zbe;
    private GameObject zh;
    private int waveCount = 0;
    private float startTime;
    private string minutes = "00";
    private bool isFinished = false;
    public bool levelRestart = false;
    private float t;
    private bool currentWave = false;
    private EnemyManager embu;
    private EnemyManager embe;
    private EnemyManager emh;
    private readonly float defaultSpawnTime = 10f;
    private float SpawnTime = 7f;
    private int difference = 40;
    private readonly int interval = 40;
    private readonly int remaining = 20;
    public bool isComplete = false;

    // Start is called before the first frame update
    public void Start()
    {
        zbu = GameObject.Find("EnemyManager");
        zbe = GameObject.Find("ZombearManager");
        zh = GameObject.Find("HellephantManager");
        startTime = Time.time;
    }

    public void reset()
    {
        startTime = Time.time - Time.time;
        difference = 20;
        waveCount = 0;
        levelRestart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFinished)
            return;
        if (levelRestart)
        {
            startTime = Time.time - Time.time;
            levelRestart = false;
        }

        embu = (EnemyManager)zbu.GetComponent(typeof(EnemyManager));
        embe = (EnemyManager)zbe.GetComponent(typeof(EnemyManager));
        emh = (EnemyManager)zh.GetComponent(typeof(EnemyManager));
        t = Time.time - startTime;
        minutes = timeConverter((int)t / 60);
        string seconds = (t % 60).ToString("f2");
        int duration = (int)(t);

        timerText.text = minutes + ":" + seconds;
        if (duration >= difference && duration <= (difference + remaining))
        {
            if (currentWave != true)
            {
                waveCount += 1;
                if(duration >=160)
                {
                    waveText.text = "Final Wave";
                } else
                {
                    waveText.text = "Wave : " + waveCount;
                }
               
                currentWave = true;
                embu.updateSpawner(SpawnTime);
                embe.updateSpawner(SpawnTime);
                emh.updateSpawner(SpawnTime);

                if(SpawnTime > 3)
                {
                    SpawnTime--;
                }
            }
            if(duration <= difference + 5)
            {
                waveHint.text = "Huge Number of zombies are Coming";
            } else
            {
                waveHint.text = "";
            }
        }
        else
        {
            if(currentWave == true)
            {
                waveText.text = "";
                waveHint.text = "";
                embu.updateSpawner(defaultSpawnTime);
                embe.updateSpawner(defaultSpawnTime);
                emh.updateSpawner(25);
                difference = difference + interval + remaining;
                currentWave = false;
            }
        }
        if (duration > 200)
        {
            this.Finnish();
            isComplete = true;
        }

    }

    public void Finnish()
    {
        isFinished = true;
        t = Time.time - startTime;
        minutes = timeConverter((int)t / 60);
        string seconds = (t % 60).ToString("f2");
        int duration = (int)(t % 60);

        timerText.text = minutes + ":" + seconds;
        waveText.fontSize = 25;
        waveText.text = "No. of Wave Survived : " + waveCount;
    }

    /**
    *  Method will convert one of the time component to string and will add 0 if needed 
    */
    string timeConverter(int component)
    {
        return (component < 10) ? '0' + component.ToString() : component.ToString();
    }
}

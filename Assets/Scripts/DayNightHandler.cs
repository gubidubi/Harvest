using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayNightHandler : MonoBehaviour
{
    GameManager gm;


    public GameObject[] enemyPrefabs;
    public GameObject remainingDaytimeUI;
    public GameObject hordeCounterUI;
    public GameObject debugHOD;
    public AudioSource music;

    public enum HourOfDay
    {
        Day,
        Night,
    };

    //stands for current Hour Of the Day
    [SerializeField]
    private HourOfDay currentHOD = HourOfDay.Day;

    //a float representing the delay betweenHordes
    [SerializeField]
    private float dayDuration = 30f;
    private float remainingDayTime;

    [SerializeField]
    private float NightDuration = 15f;
    private float remainingNightTime;

    public int hordeCounter = 0;

    private GameObject player;
    //--------------METHODS--------------
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        gm = GameManager.instance;
        remainingDayTime = dayDuration;
    }

    void Update()
    {
        if (currentHOD == HourOfDay.Day)
        {
            remainingDayTime -= Time.deltaTime;
            if (remainingDayTime <= 0)
                SetNight();
        }
        if (currentHOD == HourOfDay.Night)
        {
            remainingNightTime -= Time.deltaTime;
            if (remainingNightTime <= 0)
                SetDay();
        }
        UpdateUI();
    }


    private void SetDay()
    {
        //getseeds
        currentHOD = HourOfDay.Day;
        StopAllCoroutines();
        remainingDayTime = dayDuration;
        //remainingDaytimeUI.SetActive(true);
        //hordeCounterUI.SetActive(false);
        //do shader effects to look bright
    }

    private void SetNight()
    {
        hordeCounter++;
        StartCoroutine(HordeSpawn());
        currentHOD = HourOfDay.Night;
        remainingNightTime = NightDuration;
        //remainingDaytimeUI.SetActive(false);
        //hordeCounterUI.SetActive(true);
        //do shader effects to look dark
    }

    IEnumerator HordeSpawn()
    {
        while (true)
        {
            for (int i = 0; i < 3 + (hordeCounter + 1)/2; ++i)
            {
                SpawnEnemy();
            }
            yield return new WaitForSeconds((NightDuration + 1)/ (hordeCounter + 1));
        }
    }

    void SpawnEnemy()
    {
        int i = Random.Range(0, enemyPrefabs.Length);
        Vector3 spawnPosition = new Vector3(Random.Range(10.0f, 20.0f) * (Random.Range(-1, 1) * 2 + 1),
        Random.Range(6.0f, 12.0f) * (Random.Range(-1, 1) * 2 + 1), 0);
        Instantiate(enemyPrefabs[i], spawnPosition, Quaternion.identity);
    }

    private void UpdateUI()
    {
        //var text = remainingDaytimeUI.GetComponent;
        remainingDaytimeUI.GetComponent<TextMeshProUGUI>().text = ("Time to Dusk: " + ((int)remainingDayTime + 1));
        hordeCounterUI.GetComponent<TextMeshProUGUI>().text = ("Horde: " + (hordeCounter + 1));
        if (currentHOD == HourOfDay.Day)
            debugHOD.GetComponent<TextMeshProUGUI>().text = ("Day");
        else
            debugHOD.GetComponent<TextMeshProUGUI>().text = ("Night");
    }
}

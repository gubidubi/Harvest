using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayNightHandler : MonoBehaviour
{
    GameManager gm = GameManager.instance;

    public GameObject remainingDaytimeUI;
    public GameObject hordeCounterUI;
    public GameObject debugHOD;

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
    private float dayDuration = 15f;

    private float remainingDayTime;

    public int hordeCounter = 0;

    //--------------METHODS--------------
    void Start()
    {
        remainingDayTime = dayDuration;
        hordeCounterUI.SetActive(false);
    }

    void Update()
    {
        if (currentHOD == HourOfDay.Day)
        {
            remainingDayTime -= Time.deltaTime;
            if (remainingDayTime <= 0)
                SetNight();
        }
        UpdateUI();
    }


    private void SetDay()
    {
        currentHOD = HourOfDay.Day;
        remainingDayTime = dayDuration;
        remainingDaytimeUI.SetActive(true);
        hordeCounterUI.SetActive(false);
        //do shader effects to look bright
    }

    private void SetNight()
    {
        currentHOD = HourOfDay.Night;
        remainingDaytimeUI.SetActive(false);
        hordeCounterUI.SetActive(true);
        ;
        //do shader effects to look dark
    }

    //may be called from another script when all enemies are killed
    [ContextMenu("End Horde")]
    public void EndHorde()
    {
        if (currentHOD == HourOfDay.Day)
        {
            Debug.Log("EndHorde called at daytime");
            return;
        }

        hordeCounter++;
        SetDay();
    }

    private void UpdateUI()
    {
        //var text = remainingDaytimeUI.GetComponent;
        remainingDaytimeUI.GetComponent<TextMeshProUGUI>().text = ("Time to Dusk: " + ((int) remainingDayTime + 1));
        hordeCounterUI.GetComponent<TextMeshProUGUI>().text = ("Horde: " + (hordeCounter + 1));
        if(currentHOD == HourOfDay.Day)
            debugHOD.GetComponent<TextMeshProUGUI>().text = ("Day");
        else
            debugHOD.GetComponent<TextMeshProUGUI>().text = ("Night");
    }
}

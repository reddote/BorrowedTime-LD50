using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTickManager : MonoBehaviour
{
    public static GameTickManager Instance;
    public static event Action OnGameTick;
    [SerializeField]
    float GameTickEverySecond;
    float time;
    [SerializeField]
    TMP_Text timerText;
    float survivetime;
    public string SurviveTime
    {
        get
        {
            return timerText.text;
        }
    }
    public bool Ticking
    {
        get
        {
            return !stopped;
        }
    }
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        StartCoroutine(Tick());
        StartCoroutine(EverySecond());
    }
    IEnumerator EverySecond()
    {
        if (!stopped)
        {
            TimeSpan t = TimeSpan.FromSeconds(survivetime);
            timerText.text = string.Format("{0:D2}:{1:D2}",

                    t.Minutes,
                    t.Seconds
            );
            survivetime++;
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(EverySecond());
    }

    IEnumerator Tick()
    {
        yield return new WaitForSeconds(GameTickEverySecond);
        if (!stopped)
        {

            OnGameTick?.Invoke();
        }
        StartCoroutine(Tick());
    }
    [SerializeField]
    bool stopped = false;
    public void StartTick()
    {
        stopped = false;
    }
    public void StopTick()
    {
        stopped = true;
    }
}

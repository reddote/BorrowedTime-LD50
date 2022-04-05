using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public static PlayerStateManager Instance;
    [SerializeField]
    private bool _isHungry;
    [SerializeField]
    private bool _isNotSleeping;

    [SerializeField]
    GameObject PlayerController;
    [SerializeField]
    GameObject SleepingController;

    [SerializeField]
    AudioSource sleepingAudioSource;
    [SerializeField]
    AudioClip hungryClip;

    public bool IsHungry
    {
        get
        {
            return _isHungry;
        }
    }
    public bool IsNotSleeping
    {
        get
        {
            return _isNotSleeping;
        }
    }

    public float GetHungryAgainIn = 5f;
    float hungryTick = 0f;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Sleep();
        GameTickManager.OnGameTick += GameTickManager_OnGameTick;
    }
    private void OnDestroy()
    {
        GameTickManager.OnGameTick -= GameTickManager_OnGameTick;

    }
    void GetHungry()
    {
        hungryTick++;
        if(hungryTick >= GetHungryAgainIn)
        {
            hungryTick = 0;
            _isHungry = true;
            OneShotAudioScript.Instance.PlayOneShot(hungryClip);
        }
    }
    private void GameTickManager_OnGameTick()
    {
        GetHungry();
    }

    public void Eat()
    {
        hungryTick = 0;
        _isHungry = false;
    }
    public void Sleep()
    {
        _isNotSleeping = false;
        sleepingAudioSource.PlayDelayed(1f);
        PlayerController.gameObject.SetActive(false);
        SleepingController.gameObject.SetActive(true);
    }
    public void WakeUp()
    {
        _isNotSleeping = true;
        sleepingAudioSource.Stop();
        
        PlayerController.gameObject.SetActive(true);
        SleepingController.gameObject.SetActive(false);
    }

}

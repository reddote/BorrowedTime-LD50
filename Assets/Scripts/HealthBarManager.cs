using System;
using TMPro;
using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    public event Action OnHealthDropping;
    public event Action<string> OnDeath;
    public string EnergyDeath;
    public string HungerDeath;
    public string HeatDeath;

    [SerializeField]
    EnergyBarManager barManager;
    [SerializeField]
    private float _health;


    bool healthIsDropping = false;
    [SerializeField]
    float updateHealthEveryTick = 1;
    float _tickTimer;
    string currentReason;

    [SerializeField]
    TMP_Text DeathReason, SurviveTime;
    [SerializeField]
    string surviveTimeText;
    private void Start()
    {
        barManager.OnBarZero += BarManager_OnBarZero;
        barManager.OnBarNoZero += BarManager_OnBarNoZero;
        GameTickManager.OnGameTick += GameTickManager_OnGameTick;
    }
    private void OnDestroy()
    {
        barManager.OnBarZero -= BarManager_OnBarZero;
        barManager.OnBarNoZero -= BarManager_OnBarNoZero;
        GameTickManager.OnGameTick -= GameTickManager_OnGameTick;
    }



    private void GameTickManager_OnGameTick()
    {
        if (healthIsDropping)
        {
            _tickTimer++;
            if (_tickTimer >= updateHealthEveryTick)
            {
                _tickTimer = 0;
                _health--;
                if (_health <= 0)
                {
                    Debug.Log("DEADADA");
                    GameTickManager.Instance.StopTick();
                    OnDeath?.Invoke(currentReason);
                    DeathReason.text = currentReason;
                    SurviveTime.text = string.Format(surviveTimeText, GameTickManager.Instance.SurviveTime);
                    GameStateManager.Instance.EndScene();
                }
                else
                {
                    OnHealthDropping?.Invoke();
                }
            }
        }
        else
        {
            _tickTimer = 0f;
        }
    }

    public float Health
    {
        get
        {
            return _health;
        }
    }
    bool energyDeath, hungerDeath, heatDeath;
    private void BarManager_OnBarZero(EnergyBarManager.BarType obj)
    {
        Debug.Log("dying from " + obj.ToString());
        switch (obj)
        {
            case EnergyBarManager.BarType.Energy:
                currentReason = EnergyDeath;
                energyDeath = true;
                break;
            case EnergyBarManager.BarType.Hunger:
                currentReason = HungerDeath;
                hungerDeath = true;

                break;
            case EnergyBarManager.BarType.Heat:
                currentReason = HeatDeath;
                heatDeath = true;
                
                break;
            default:
                break;
        }
        healthIsDropping = true;
    }

    private void BarManager_OnBarNoZero(EnergyBarManager.BarType obj)
    {
        switch (obj)
        {
            case EnergyBarManager.BarType.Energy:
                energyDeath = false;
                break;
            case EnergyBarManager.BarType.Hunger:
                hungerDeath = false;

                break;
            case EnergyBarManager.BarType.Heat:
                heatDeath = false;

                break;
            default:
                break;
        }
        healthIsDropping = heatDeath | hungerDeath | energyDeath;
    }
}


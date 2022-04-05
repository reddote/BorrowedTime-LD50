using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnergyBarManager : MonoBehaviour
{
    public enum BarType
    {
        Energy,
        Hunger,
        Heat
    }
    [Header("UI Elements")]
    [SerializeField]
    LifeBar _Energy;
    [SerializeField]
    LifeBar _Hunger;
    [SerializeField]
    LifeBar _Heat;
    public LifeBar Energy => _Energy;
    public LifeBar Hunger => _Hunger;
    public LifeBar Heat => _Heat;

    public event Action<BarType> OnBarZero;
    public event Action<BarType> OnBarNoZero;


    [Header("settings")]
    public float EnergyMax = 100f;
    public float HungerMax = 100f;
    public float HeatMax = 100f;


    [Header("Drop Values"), Min(0)]
    public float EnergyDropAmount; public float HungerDropAmount; public float HeatDropAmount;
    public float EnergyGainAmount; public float HungerGainAmount; public float HeatGainAmount;

    [Header("Drop Ticks")]
    public float EnergyDropTick;
    public float HungerDropTick;
    public float HeatDropTick;

    float energyTick, hungerTick, heatTick;

    [Header("spice!")]
    [SerializeField]
    AudioClip tiredClip; AudioClip hungerClip; AudioClip coldClip;


    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _Energy.Setup(0, EnergyMax, EnergyMax);
        _Hunger.Setup(0, HungerMax, HungerMax);
        _Heat.Setup(0, HeatMax, HeatMax);
        _Energy.OnValueChangedEvent += _Energy_OnValueChangedEvent;
        _Hunger.OnValueChangedEvent += _Hunger_OnValueChangedEvent;
        _Heat.OnValueChangedEvent += _Heat_OnValueChangedEvent;
        GameTickManager.OnGameTick += GameTickManager_OnGameTick;

    }
    private void OnDestroy()
    {
        _Energy.OnValueChangedEvent -= _Energy_OnValueChangedEvent;
        _Hunger.OnValueChangedEvent -= _Hunger_OnValueChangedEvent;
        _Heat.OnValueChangedEvent -= _Heat_OnValueChangedEvent;
        GameTickManager.OnGameTick -= GameTickManager_OnGameTick;
    }
    void HandleHeat()
    {
        if (heatTick >= HeatDropTick)
        {

            heatTick = 0;
            if (EnvironmentState.Instance.IsCold)
            {
                Heat.Value -=  HeatDropAmount;
                droppingHeat = true;
            }
            else
            {
                if (droppingHeat)
                {
                    droppingHeat = false;
                    return;
                }
                Heat.Value +=HeatGainAmount;

            }
        }
        else
        {
            heatTick++;
        }

    }
    void HandleEnergy()
    {
        if (energyTick >= EnergyDropTick)
        {

            energyTick = 0;
            if (PlayerStateManager.Instance.IsNotSleeping)
            {
                Energy.Value -= EnergyDropAmount;
                droppingEnergy = true;
            }
            else
            {
                if (droppingEnergy)
                {
                    droppingEnergy = false;
                    return;
                }
                Energy.Value += EnergyGainAmount;
            }
        }
        else
        {
            energyTick++;
        }


    }
    bool droppingHunger, droppingHeat, droppingEnergy;
    internal static EnergyBarManager Instance;

    void HandleHunger()
    {
        if (hungerTick >= HeatDropTick)
        {

            hungerTick = 0;
            if (PlayerStateManager.Instance.IsHungry)
            {
                droppingHunger = true;

                Hunger.Value -=  HungerDropAmount;
            }
            else
            {
                if (droppingHunger)
                {
                    droppingHunger = false;
                    return;
                }
                Hunger.Value +=  HungerGainAmount;

            }
        }
        else
        {
            hungerTick++;
        }

    }
    private void GameTickManager_OnGameTick()
    {
        HandleHeat();
        HandleHunger();
        HandleEnergy();
    }

    private void _Heat_OnValueChangedEvent(LifeBar sender, float newValue, float oldValue, bool isDown)
    {
        if (newValue <= 0)
        {
            OnBarZero?.Invoke(BarType.Heat);
            sender.Value = 0;
            OneShotAudioScript.Instance.PlayOneShot(coldClip);
        }
        else
        {
            OnBarNoZero?.Invoke(BarType.Heat);

        }
    }

    private void _Hunger_OnValueChangedEvent(LifeBar sender, float newValue, float oldValue, bool isDown)
    {
        if (newValue == 0)
        {
            OnBarZero?.Invoke(BarType.Hunger);
            OneShotAudioScript.Instance.PlayOneShot(hungerClip);
            //sender.Value = 0;
        }
        else
        {
            OnBarNoZero?.Invoke(BarType.Hunger);
        }
    }

    private void _Energy_OnValueChangedEvent(LifeBar sender, float newValue, float oldValue, bool isDown)
    {
        if (newValue <= 0)
        {
            OnBarZero?.Invoke(BarType.Energy);
            OneShotAudioScript.Instance.PlayOneShot(tiredClip);

        }
        else
        {
            OnBarNoZero?.Invoke(BarType.Energy);

        }
    }

}


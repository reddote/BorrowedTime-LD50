using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnvironmentState : MonoBehaviour
{
    public static EnvironmentState Instance;
    [SerializeField]
    float GetColdAgainIn = 15f;
    [SerializeField]
    private bool _isCold;
    float coldTick = 0f;

    [SerializeField]
    UnityEvent OnCold;
    [SerializeField]
    UnityEvent OnWarm;

    [SerializeField]
    AudioSource coldAudioS;

    public bool IsCold
    {
        get { return _isCold; }
    }
    public void SetWarm()
    {
        _isCold = false;
        coldTick = 0f;
        coldAudioS.Stop();

        OnWarm?.Invoke();
    }
    private void Awake()
    {
        Instance = this;
        GameTickManager.OnGameTick += GameTickManager_OnGameTick;
    }
    private void OnDestroy()
    {
        GameTickManager.OnGameTick -= GameTickManager_OnGameTick;

    }

    private void GameTickManager_OnGameTick()
    {

        coldTick++;
        if (coldTick >= GetColdAgainIn)
        {
            coldTick = 0;
            if (!_isCold)
                OnCold?.Invoke();
            _isCold = true;
            coldAudioS.PlayDelayed(1f);
        }
    }
}

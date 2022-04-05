using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class LifeBar : MonoBehaviour
{
    public delegate void BarValueChangedEvent(LifeBar sender, float newValue, float oldValue, bool isDown);
    public event BarValueChangedEvent OnValueChangedEvent;

    Slider slider;
    float _value;
    float _max;
    float _min;
    float _sliderValue;
    public float Value
    {
        get
        {
            return _value;
        }
        set
        {
            var v = value;
            if (v < _min)
                v = _min;
            if (v > _max)
                v = _max;
            if (v <= _max && v >= _min)
            {
                var old = _value;
                var @new = v;
                if (old != @new)
                {

                    _value = v;
                    _sliderValue = ConvertValue(v);
                    UpdateUI();
                    SendEventData(@new, old);
                }
            }

        }
    }

    public float Max { get => _max; }
    public float Min { get => _min; }

    void SendEventData(float newValue, float oldValue)
    {
        bool isDown = false;
        if (oldValue > newValue)
        {
            isDown = true;
        }
        OnValueChangedEvent?.Invoke(this, newValue, oldValue, isDown);
    }


    private void Start()
    {
        slider = GetComponent<Slider>();

    }
    private float ConvertValue(float value)
    {
        var convertedValue = value.ConvertToCustomRange(_max, _min, 1f, 0f);
        return convertedValue;
    }


    public void Setup(float min, float max, float startValue)
    {
        this._min = min;
        this._max = max;
        this._value = startValue;
        this._sliderValue = this._value.ConvertToCustomRange(max, min, 1f, 0);
        UpdateUI();
    }

    void UpdateUI()
    {
        if (slider)
            slider.value = _sliderValue;
    }

}

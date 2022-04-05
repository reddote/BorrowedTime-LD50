using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathExtensions
{
    /*
     x / 100 => F-32/180 => C?

    X - DS
    ---------
    KS - DS


    Celcius Aralığından 50 derece ise Fahrenait'te kaçtır

    50 - 0(Ds) / 100(Ks) - 0(Ds) = X(istenen) - 32(Ds) / 212(Ks)- 32(Ds)

    50/100 => X-32/180
    180*(50/100) = X-32
    180*(50/100) + 32 = X

    Bilinen - BilinenMin / BilinenMax - BilinenMin = istenen - istenenMin / istenenMax - istenenMin

     **/

    public static float ConvertToCustomRange(this float self, float currentMax, float currentMin, float newRangeMax, float newRangeMin)
    {
        float converted = (newRangeMax - newRangeMin) * ((self - currentMin) / currentMax) + (newRangeMin);
        return converted;
    }


}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZarTakip : MonoBehaviour
{
    [SerializeField]
    Dice d1;
    [SerializeField]
    Dice d2;

    private void Update()
    {
        var tXZ = Vector3.Lerp(d1.transform.position, d2.transform.position, 0.5f);
        tXZ.y = transform.position.y;
        transform.position = tXZ;
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dice : MonoBehaviour
{
    private bool hasLanded = true;
    private bool thrown = false;
    private int diceValue = 0;
    private float diceScale = 1;
    private Rigidbody rigidBody;
    private float pushPower = 5f;
    public event Action<int> diceCallBack;
    public LayerMask layerMask;

    [SerializeField]
    AudioClip hitWall, hitDice;
    bool startSet;
    float startY;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (!thrown || hasLanded) return;
        if (layerMask == (layerMask | 1 << collision.gameObject.layer))
        {
            Debug.Log("Duvar? ");
            OneShotAudioScript.Instance.PlayOneShot(hitWall, Random.Range(0.9f, 1.1f), Mathf.Clamp(rigidBody.velocity.magnitude,0,1));


        }else if (collision.gameObject.GetComponent<Dice>() != null)
        {
            OneShotAudioScript.Instance.PlayOneShot(hitDice, Random.Range(0.9f, 1.1f), Mathf.Clamp(rigidBody.velocity.magnitude, 0, 1));
        }
    }
    void Update()
    {
        //double jump engelle
        if (rigidBody.IsSleeping())
        {
            if (!startSet)
            {
                startY = transform.position.y;
            }
            hasLanded = true;
        }

        if (hasLanded && thrown)
        {
            diceValue = DiceFaceCheck();
            thrown = false;
            Debug.Log(diceValue);
            //diceCallBack(diceValue);
            if (startY != transform.position.y)
            {
                RollDice(pushPower / 2);
            }
            if (diceValue == 0)
            {
                RollDice(pushPower / 2);
            }
            else
            {
                diceCallBack?.Invoke(diceValue);

            }
        }
    }

    public void RollDice(float power)
    {
        if (hasLanded && !thrown)
        {
            pushPower = power;
            thrown = true;
            hasLanded = false;
            var randomDirection = Random.onUnitSphere;
            randomDirection.y = Mathf.Abs(randomDirection.y);
            var _up = Vector3.up * power;
            var _power = randomDirection * power + _up;

            rigidBody.AddForce(_power, ForceMode.Impulse);
            rigidBody.AddTorque(_power * 10, ForceMode.Impulse);

        }
    }

    public int DiceFaceCheck()
    {
        RaycastHit hit;
        int diceNo = 0;
        int hitSurfaces = 0;
        float dist = 1.01f;
        if (Physics.Raycast(transform.position, transform.up, out hit, dist, layerMask))
        {
            hitSurfaces++;
            diceNo = 6;
        }
        if (Physics.Raycast(transform.position, -transform.up, out hit, dist, layerMask))
        {
            hitSurfaces++;
            diceNo = 1;
        }
        if (Physics.Raycast(transform.position, -transform.right, out hit, dist, layerMask))
        {
            hitSurfaces++;
            diceNo = 2;
        }
        if (Physics.Raycast(transform.position, transform.right, out hit, dist, layerMask))
        {
            hitSurfaces++;
            diceNo = 5;
        }
        if (Physics.Raycast(transform.position, transform.forward, out hit, dist, layerMask))
        {
            hitSurfaces++;
            diceNo = 4;
        }
        if (Physics.Raycast(transform.position, -transform.forward, out hit, dist, layerMask))
        {
            hitSurfaces++;
            diceNo = 3;
        }
        return hitSurfaces == 1 ? diceNo : 0;
    }


}

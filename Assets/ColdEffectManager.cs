using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdEffectManager : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    public void Show()
    {
        animator.SetTrigger("OnStart");
        animator.ResetTrigger("OnStop");
    }
    public void Hide()
    {
        animator.SetTrigger("OnStop");
        animator.ResetTrigger("OnStart");
    }
}

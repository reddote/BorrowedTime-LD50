using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableAnimationHandler : MonoBehaviour
{
    // Start is called before the first frame update
    Interactables.InteractablesObject Interactable;
    [SerializeField]
    Animator animator;
    private void Awake()
    {
        Interactable = GetComponent<Interactables.InteractablesObject>();
    }
    void Start()
    {
        Interactable.OnMouseHover += Interactable_OnMouseHover;
        Interactable.OnMouseHoverOver += Interactable_OnMouseHoverOver;
    }
    private void OnDestroy()
    {
        Interactable.OnMouseHover -= Interactable_OnMouseHover;
        Interactable.OnMouseHoverOver -= Interactable_OnMouseHoverOver;
    }
    private void Interactable_OnMouseHoverOver(Interactables.InteractablesObject sender)
    {
        animator.SetTrigger("unhover");
        animator.ResetTrigger("hover");
    }

    private void Interactable_OnMouseHover(Interactables.InteractablesObject sender)
    {
        animator.SetTrigger("hover");
        animator.ResetTrigger("unhover");

    }


}

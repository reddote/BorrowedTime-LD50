using Interactables;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupResourceManager : MonoBehaviour
{
    Yemek.Resource _resource;
    Interactables.InteractablesObject ınteractable;
    [SerializeField]
    TMP_Text text;
    [SerializeField]
    TMP_Text sadText, hapText;
    [SerializeField]
    Transform popuptransform;
  
    private void Awake()
    {
        _resource = GetComponent<Yemek.Resource>();
        ınteractable = GetComponent<Interactables.InteractablesObject>();
        _resource.OnResourceChange += _resource_OnResourceChange; ;
        //ınteractable.OnMouseHover += Interactable_OnMouseHover;
        //ınteractable.OnMouseHoverOver += Interactable_OnMouseHoverOver;
        popuptransform.gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        _resource.OnResourceChange += _resource_OnResourceChange;
    }

    private void _resource_OnResourceChange(Yemek.Resource self, int change, int oldCount, int newCount)
    {
        text.text = _resource.GetResourceCount().ToString();

        if (change > 0)
        {
            text.gameObject.SetActive(false);
            //happy yazı
            sadText.gameObject.SetActive(false);
            hapText.gameObject.SetActive(true);
            hapText.text = "+" + change;
        }
        else if(change<0)
        {
            text.gameObject.SetActive(false);
            hapText.gameObject.SetActive(false);
            sadText.gameObject.SetActive(true);
            sadText.text = change.ToString();
            //sad yazı
        }
        ShowPopup();
    }

    private void Interactable_OnMouseHoverOver(InteractablesObject sender)
    {
        text.gameObject.SetActive(false);
    }

    private void Interactable_OnMouseHover(Interactables.InteractablesObject sender)
    {
        text.text = _resource.GetResourceCount().ToString();
        text.gameObject.SetActive(true);

    }

    internal void ShowPopup()
    {
        StartCoroutine(ShowPopupForSeconds());
    }
    IEnumerator ShowPopupForSeconds()
    {
        popuptransform.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        popuptransform.gameObject.SetActive(false);
        text.gameObject.SetActive(true);
        hapText.gameObject.SetActive(false);
        sadText.gameObject.SetActive(false);


    }

}

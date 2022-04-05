using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yemek;

public class InteractablesManager : MonoBehaviour
{
    [SerializeField]
    Interactables.InteractablesObject DoorInteractable, KazanInteractable, SobaInteractable, YatakInteractable

        , WoodInteractable
        , FoodInteractable
        ;
    [SerializeField]
    int sobaDiceLimit, kazanDiceLimit, doorDiceLimit;
    Resource WoodResource, FoodResouce;
    PopupResourceManager woodPopup, foodPopup;
    [SerializeField]
    AudioClip doorClip, kazanClip, sobaClip;
    private void Start()
    {
        WoodResource = WoodInteractable.GetComponent<Yemek.Resource>();
        FoodResouce = FoodInteractable.GetComponent<Yemek.Resource>();
        woodPopup = WoodInteractable.GetComponent<PopupResourceManager>();
        foodPopup = FoodInteractable.GetComponent<PopupResourceManager>();
        DoorInteractable.OnMouseClick += DoorInteractable_OnMouseClick;
        KazanInteractable.OnMouseClick += KazanInteractable_OnMouseClick;
        SobaInteractable.OnMouseClick += SobaInteractable_OnMouseClick;
        YatakInteractable.OnMouseClick += YatakInteractable_OnMouseClick;
        WoodInteractable.OnMouseClick += WoodResource_OnMouseClick;
        FoodInteractable.OnMouseClick += FoodResource_OnMouseClick;
        PlayerMovement.Instance.PlayerMoving += Instance_PlayerMoving;
        PlayerMovement.Instance.destinationReached += Instance_destinationReached;
    }
    private void OnDestroy()
    {
        DoorInteractable.OnMouseClick -= DoorInteractable_OnMouseClick;
        KazanInteractable.OnMouseClick -= KazanInteractable_OnMouseClick;
        SobaInteractable.OnMouseClick -= SobaInteractable_OnMouseClick;
        YatakInteractable.OnMouseClick -= YatakInteractable_OnMouseClick;
        WoodInteractable.OnMouseClick -= WoodResource_OnMouseClick;
        FoodInteractable.OnMouseClick -= FoodResource_OnMouseClick;
        PlayerMovement.Instance.PlayerMoving -= Instance_PlayerMoving;
        PlayerMovement.Instance.destinationReached -= Instance_destinationReached;
    }

    private void Instance_PlayerMoving()
    {
        if (DiceButtonController.Instance.ShowingDice && !DiceButtonController.Instance.DiceThrown)
        {

            DiceButtonController.Instance.HideDice();
            DiceManager.instance.OnAllDiceAreStopped -= Kazan_AllDicesAreStopped;
            DiceManager.instance.OnAllDiceAreStopped -= Soba_AllDicesAreStopped;
            DiceManager.instance.OnAllDiceAreStopped -= Door_AllDicesAreStopped;
        }
    }

    void WoodHandle()
    {
        woodPopup.ShowPopup();
    }
    void FoodHandle()
    {
        foodPopup.ShowPopup();

    }
    private void FoodResource_OnMouseClick(Interactables.InteractablesObject sender)
    {
        //if (DiceButtonController.Instance.ShowingDice && !DiceButtonController.Instance.DiceThrown) return;

        PlayerMovement.Instance.SendToInteractable(sender.GetComponent<TargetPositionHolder>().target);
        lastClickAction = FoodHandle;
    }

    private void WoodResource_OnMouseClick(Interactables.InteractablesObject sender)
    {
        PlayerMovement.Instance.SendToInteractable(sender.GetComponent<TargetPositionHolder>().target);
        lastClickAction = WoodHandle;
    }

    Action lastClickAction;
    private void Instance_destinationReached()
    {
        lastClickAction?.Invoke();
    }


    void SleepHandle()
    {

        //set sleeping
        //set animation to sleeping
        //DiceManager.instance.OnAllDiceAreStopped += Yatak_AllDicesAreStopped;
        PlayerStateManager.Instance.Sleep();

    }

    //private void Yatak_AllDicesAreStopped(int obj)
    //{
    //    PlayerStateManager.Instance.Sleep();
    //}

    void SobaHandle()
    {
        //use resource
        //set not cold
        if (WoodResource.GetResourceCount() == 0)
        {
            woodPopup.ShowPopup();
        }
        else
        {

            DiceManager.instance.OnAllDiceAreStopped += Soba_AllDicesAreStopped;
            OneShotAudioScript.Instance.PlayOneShot(sobaClip);
            DiceButtonController.Instance.ShowDice(sobaDiceLimit);
        }
    }

    private void Soba_AllDicesAreStopped(int obj)
    {
        DiceManager.instance.OnAllDiceAreStopped -= Soba_AllDicesAreStopped;
        WoodResource.ResourceSpent(-1);
        if (obj > sobaDiceLimit)
        {
            EnvironmentState.Instance.SetWarm();
            sobaDiceLimit++;
            if (sobaDiceLimit > 11)
                sobaDiceLimit = 11;
        }
    }

    void KazanHandle()
    {
        //Use Resource
        Debug.Log("KazanHandle handle");
        //Set not hungry
        if (FoodResouce.GetResourceCount() == 0)
        {
            foodPopup.ShowPopup();
        }
        else
        {
            DiceManager.instance.OnAllDiceAreStopped += Kazan_AllDicesAreStopped;
            OneShotAudioScript.Instance.PlayOneShot(kazanClip);
            DiceButtonController.Instance.ShowDice(kazanDiceLimit);
        }
    }

    private void Kazan_AllDicesAreStopped(int obj)
    {
        DiceManager.instance.OnAllDiceAreStopped -= Kazan_AllDicesAreStopped;
        FoodResouce.ResourceSpent(-1);
        if (obj > kazanDiceLimit)
        {
            PlayerStateManager.Instance.Eat();
            kazanDiceLimit++;
            if (kazanDiceLimit > 11)
                kazanDiceLimit = 11;
        }
    }

    void DoorHandle()
    {
        //Get Random Resources
        Debug.Log("DoorHandle handle");
        DiceManager.instance.OnAllDiceAreStopped += Door_AllDicesAreStopped;
        OneShotAudioScript.Instance.PlayOneShot(doorClip);
        DiceButtonController.Instance.ShowDice(doorDiceLimit);

        //DiceThrow.OnDiceResult(
        //RandomResourceGenerotor.Instance.GetResources();
        //);
    }

    private void Door_AllDicesAreStopped(int obj)
    {
        DiceManager.instance.OnAllDiceAreStopped -= Door_AllDicesAreStopped;
        EnergyBarManager.Instance.Energy.Value -= EnergyBarManager.Instance.Energy.Max * 0.1f;
        //WoodResource.ResourceSpent(WoodResource.GetResourceCount());
        //FoodResouce.ResourceSpent(FoodResouce.GetResourceCount());
        if (obj > doorDiceLimit)
        {
            Yemek.RandomResourceGenerator.Instance.GetResources();
            doorDiceLimit++;
            if (doorDiceLimit > 11)
                doorDiceLimit = 11;
        }
    }

    private void Update()
    {

        if (!PlayerStateManager.Instance.IsNotSleeping && GameTickManager.Instance.Ticking)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //set not sleeping
                PlayerStateManager.Instance.WakeUp();

            }
        }
    }
    private void YatakInteractable_OnMouseClick(Interactables.InteractablesObject sender)
    {
        if (DiceButtonController.Instance.ShowingDice && DiceButtonController.Instance.DiceThrown) return;

        //PlayerStateManager.Instance.Sleep();
        PlayerMovement.Instance.SendToInteractable(sender.GetComponent<TargetPositionHolder>().target);
        lastClickAction = SleepHandle;
    }


    private void SobaInteractable_OnMouseClick(Interactables.InteractablesObject sender)
    {

        if (DiceButtonController.Instance.ShowingDice && DiceButtonController.Instance.DiceThrown) return;
        PlayerMovement.Instance.SendToInteractable(sender.GetComponent<TargetPositionHolder>().target);
        lastClickAction = SobaHandle;
    }



    private void KazanInteractable_OnMouseClick(Interactables.InteractablesObject sender)
    {

        if (DiceButtonController.Instance.ShowingDice && DiceButtonController.Instance.DiceThrown) return;
        PlayerMovement.Instance.SendToInteractable(sender.GetComponent<TargetPositionHolder>().target);
        lastClickAction = KazanHandle;
    }



    private void DoorInteractable_OnMouseClick(Interactables.InteractablesObject sender)
    {
        if (DiceButtonController.Instance.ShowingDice && DiceButtonController.Instance.DiceThrown) return;
        PlayerMovement.Instance.SendToInteractable(sender.GetComponent<TargetPositionHolder>().target);
        lastClickAction = DoorHandle;
    }


}

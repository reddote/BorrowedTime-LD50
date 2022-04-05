using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiceButtonController : MonoBehaviour{
    [SerializeField] private float delayTimeForRoll;
    [SerializeField] private float delayTimeForHide;
    [SerializeField] private Transform diceTexture;
    //[SerializeField] private GameObject text;
    [SerializeField] private Button diceButton;
    [SerializeField] TMP_Text text;
    [SerializeField] AudioClip openUIAudio;
    public static DiceButtonController Instance;

    public bool DiceThrown { get; private set; }
    public bool ShowingDice { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    public void ShowDice(int buttonLimit)
    {

        text.text = String.Format("Roll {0} or Higher",buttonLimit);
        diceButton.gameObject.SetActive(true);
        diceTexture.gameObject.SetActive(true);
        OneShotAudioScript.Instance.PlayOneShot(openUIAudio);
        ShowingDice = true;
    }
    private void Start(){
        diceButton.onClick.AddListener(OnClickButton);
        DiceManager.instance.OnAllDiceAreStopped += ButtonEnabler;
        HideDice();
    }

    public void OnClickButton(){
        diceButton.gameObject.SetActive(false);
        //GetComponent<Image>().enabled = false;
        //text.SetActive(false);
        //diceCamera.gameObject.SetActive(true);
        DiceThrown = true;
        StartCoroutine(RollDiceWithDelay(delayTimeForRoll));
    }

    private IEnumerator RollDiceWithDelay(float delay){
        yield return new WaitForSeconds(delay);
        DiceManager.instance.RollDices();
    }
    IEnumerator HideDiceWithDelay()
    {
        yield return new WaitForSeconds(delayTimeForHide);
        HideDice();
    }

    public void HideDice()
    {
        diceButton.gameObject.SetActive(false);
        diceTexture.gameObject.SetActive(false);
        ShowingDice = false;
    }

    private void ButtonEnabler(int a){
        //diceButton.gameObject.SetActive(true);
        StartCoroutine(HideDiceWithDelay());
        DiceThrown = false;

        //GetComponent<Image>().enabled = true;
        //text.SetActive(true);
        //diceCamera.gameObject.SetActive(false);
    }

}
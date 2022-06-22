using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PartyChooseLine : MonoBehaviour
{
    public int amountOfMandates;
    public int index;
    public InputField partyName;
    public Text partyMandates;
    public Slider partyMandatesSlider;
    public Button partyPreference;
    void Start()
    {
        partyMandatesSlider.minValue = 0;
        partyMandatesSlider.maxValue = amountOfMandates;
        if (GlobalPartyChoose.mandates[index] <= amountOfMandates)
        {
            partyMandatesSlider.value = GlobalPartyChoose.mandates[index];
        }
        else
        {
            partyMandatesSlider.value = amountOfMandates;
            GlobalPartyChoose.mandates[index] = amountOfMandates;
        }
        partyMandates.text = " " + partyMandatesSlider.value;
        partyName.text = (GlobalPartyChoose.partyNames[index] == null || GlobalPartyChoose.partyNames[index] == "") ? "Party" + index : GlobalPartyChoose.partyNames[index];
    }
    void Update()
    {
        GlobalPartyChoose.partyNames[index] = (partyName.text != null && partyName.text != "" && !(allSpaces(partyName.text))) ? partyName.text : partyName.text;
        partyMandatesSlider.onValueChanged.AddListener(delegate { sliderMoves(); });
    }
    public bool allSpaces(string str)
    {
        return str.Replace(" ", "").Length == 0;
    }
    public void sliderClicked()
    {
        int res = (amountOfMandates - GlobalPartyChoose.summary);
        partyMandatesSlider.maxValue = (GlobalPartyChoose.summary == 0) ? amountOfMandates : partyMandatesSlider.value + res;
        if (partyMandatesSlider.maxValue < 0)
        {
            partyMandatesSlider.maxValue = 0;
        }
    }
    public async void sliderMoves()
    {
        partyMandates.text = " " + partyMandatesSlider.value.ToString();
        GlobalPartyChoose.mandates[index] = (int)partyMandatesSlider.value;
    }
    public void preferenceClicked()
    {
        GlobalPartyChoose.wasClicked = true;
        GlobalPartyChoose.preferenceIndex = index;
    }
}

























































































































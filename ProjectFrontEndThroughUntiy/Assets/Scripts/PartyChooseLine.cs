using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PartyChooseLine : MonoBehaviour
{
    public int amountOfMandates;
    public int index;
    public TMP_Text partyDefaultName;
    public TMP_Text partyName;
    public Text partyMandates;
    public Slider partyMandatesSlider;
    public Button partyPreference;
    public GameObject alert;
    public Text alertText;
    void Start()
    {
        partyMandatesSlider.minValue = 0;
        partyMandatesSlider.maxValue = amountOfMandates;
        partyMandatesSlider.value = GlobalPartyChoose.mandates[index];
        partyName.text = (GlobalPartyChoose.partyNames[index] == null) ? "Party" + index : GlobalPartyChoose.partyNames[index];
        partyDefaultName.text = (GlobalPartyChoose.partyNames[index] == null) ? "Party" + index : GlobalPartyChoose.partyNames[index];
    }
    void Update()
    {
        partyMandatesSlider.onValueChanged.AddListener(delegate { sliderMoves(); });
        GlobalPartyChoose.partyNames[index] = partyName.text;
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
    public void manualChangeOfPartyMandate()
    {
        if (partyMandates.text == "" || (int.TryParse(partyMandates.text, out int partyMandatesInt)))
        {
            alertShow(true, "Can't use something else than a number .", 1.5f);
        }
        else
        {
            if (partyMandatesInt < 0 && partyMandatesInt > amountOfMandates)
            {
                alertShow(true, "Can't use something else than a number between 0 and " + amountOfMandates + " .", 1.5f);
            }
            else
            {
                partyMandatesSlider.value = partyMandatesInt;
            }

        }
    }
    public void alertShow(bool show, string message, float time)
    {
        alert.SetActive(show);
        alertText.text = message;
        StartCoroutine(hideAlert(time));
    }
    IEnumerator hideAlert(float time)
    {
        yield return new WaitForSeconds(time);
        alert.SetActive(false);
    }
}









































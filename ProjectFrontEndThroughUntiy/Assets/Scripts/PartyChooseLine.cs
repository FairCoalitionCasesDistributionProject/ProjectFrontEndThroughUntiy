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
    public TMP_Text partyMandates;
    public Slider partyMandatesSlider;
    public Button partyPreference;
    public GameObject alert;
    public Text alertText;

    void Start()
    {
        partyDefaultName.text = "Party" + ((index == 0) ? "" : " " + index);
        partyMandatesSlider.minValue = 0;
        partyMandatesSlider.maxValue = amountOfMandates;


        partyMandatesSlider.value = 0;
        partyMandates.text = "0";
    }







    void Update()
    {
        partyMandatesSlider.onValueChanged.AddListener(delegate { sliderMoves(); });
    }

    public async void sliderMoves()
    {
        partyMandates.text = " " + partyMandatesSlider.value.ToString();
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

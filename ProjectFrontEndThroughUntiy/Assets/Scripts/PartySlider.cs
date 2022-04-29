using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PartySlider : MonoBehaviour
{
    public int party;
    public int index;
    public Text caseName;
    public Slider slider;
    public Text number;
    void Start()
    {
        caseName.text = GlobalPartyChoose.ministeries[index];
        slider.value = GlobalPartyChoose.partyParameters[party, index];
        number.text = " " + slider.value;
    }
    void Update()
    {
        slider.onValueChanged.AddListener(
            delegate
            {
                GlobalPartyChoose.partyParameters[party, index] = (int)slider.value;
                number.text = " " + slider.value;
            });
    }
}
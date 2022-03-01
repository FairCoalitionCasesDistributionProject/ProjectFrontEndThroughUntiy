using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class NumberWithSlide : MonoBehaviour
{
    public int caseNumber;
    public Text number;
    public Slider slider;
    public TextMeshProUGUI name;
    void Start()
    {
        slider.value=MainControl.partyParameters[MainControl.currentIdentifier,caseNumber];
        number.text=" "+slider.value;
        name.text=MainControl.casesNamesTranslation[caseNumber];
    }
    void Update (){
        MainControl.partyParameters[MainControl.currentIdentifier,caseNumber]=(int)Math.Round(slider.value);
        number.text=" "+slider.value;
    }

}

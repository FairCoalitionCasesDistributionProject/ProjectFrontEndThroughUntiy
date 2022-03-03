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
        switch (MainControl.currentName)
        {
            case "likud":
                MainControl.currentIdentifier = ((int)MainControl.parties.likud);
                break;
            case "yeshAtid":
                MainControl.currentIdentifier = ((int)MainControl.parties.yeshAtid);
                break;
            case "shas":
                MainControl.currentIdentifier = ((int)MainControl.parties.shas);
                break;
            case "kaholLavan":
                MainControl.currentIdentifier = ((int)MainControl.parties.kaholLavan);
                break;
            case "yemina":
                MainControl.currentIdentifier = ((int)MainControl.parties.yemina);
                break;
            case "haavoda":
                MainControl.currentIdentifier = ((int)MainControl.parties.haavoda);
                break;
            case "israelBeitenu":
                MainControl.currentIdentifier = ((int)MainControl.parties.israelBeitenu);
                break;
            case "yahadutHatora":
                MainControl.currentIdentifier = ((int)MainControl.parties.yahadutHatora);
                break;
            case "hareshimaHamshutefet":
                MainControl.currentIdentifier = ((int)MainControl.parties.hareshimaHamshutefet);
                break;
            case "hareshimaHaaravitHameshutefet":
                MainControl.currentIdentifier = ((int)MainControl.parties.hareshimaHaaravitHameshutefet);
                break;
            case "hazionutHadatit":
                MainControl.currentIdentifier = ((int)MainControl.parties.hazionutHadatit);
                break;
            case "tikvaHadasha":
                MainControl.currentIdentifier = ((int)MainControl.parties.tikvaHadasha);
                break;
            case "meretz":
                MainControl.currentIdentifier = ((int)MainControl.parties.meretz);
                break;
        }
        slider.value = MainControl.partyParameters[MainControl.currentIdentifier, caseNumber];
        number.text = " " + slider.value;
        name.text = MainControl.casesNamesTranslation[caseNumber];
    }
    void Update()
    {
        MainControl.partyParameters[MainControl.currentIdentifier, caseNumber] = (int)Math.Round(slider.value);
        number.text = " " + slider.value;
    }

}

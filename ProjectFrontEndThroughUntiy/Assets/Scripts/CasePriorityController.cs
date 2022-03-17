using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CasePriorityController : MonoBehaviour
{
    public Sprite likud, haavoda, hareshimaHamshutefet, hareshimaHaaravitHameshutefet, hazionutHadatit, israelBeitenu, kaholLavan, meretz, shas, tikvaHadasha, yahadutHatora, yemina, yeshAtid;
    public Image current;
    void Start()
    {
        switch (MainControl.currentName)
        {
            case "likud":
                current.sprite = likud;
                MainControl.currentIdentifier = ((int)MainControl.parties.likud);
                break;
            case "yeshAtid":
                current.sprite = yeshAtid;
                MainControl.currentIdentifier = ((int)MainControl.parties.yeshAtid);
                break;
            case "shas":
                current.sprite = shas;
                MainControl.currentIdentifier = ((int)MainControl.parties.shas);
                break;
            case "kaholLavan":
                current.sprite = kaholLavan;
                MainControl.currentIdentifier = ((int)MainControl.parties.kaholLavan);
                break;
            case "yemina":
                current.sprite = yemina;
                MainControl.currentIdentifier = ((int)MainControl.parties.yemina);
                break;
            case "haavoda":
                current.sprite = haavoda;
                MainControl.currentIdentifier = ((int)MainControl.parties.haavoda);
                break;
            case "israelBeitenu":
                current.sprite = israelBeitenu;
                MainControl.currentIdentifier = ((int)MainControl.parties.israelBeitenu);
                break;
            case "yahadutHatora":
                current.sprite = yahadutHatora;
                MainControl.currentIdentifier = ((int)MainControl.parties.yahadutHatora);
                break;
            case "hareshimaHamshutefet":
                current.sprite = hareshimaHamshutefet;
                MainControl.currentIdentifier = ((int)MainControl.parties.hareshimaHamshutefet);
                break;
            case "hareshimaHaaravitHameshutefet":
                current.sprite = hareshimaHaaravitHameshutefet;
                MainControl.currentIdentifier = ((int)MainControl.parties.hareshimaHaaravitHameshutefet);
                break;
            case "hazionutHadatit":
                current.sprite = hazionutHadatit;
                MainControl.currentIdentifier = ((int)MainControl.parties.hazionutHadatit);
                break;
            case "tikvaHadasha":
                current.sprite = tikvaHadasha;
                MainControl.currentIdentifier = ((int)MainControl.parties.tikvaHadasha);
                break;
            case "meretz":
                current.sprite = meretz;
                MainControl.currentIdentifier = ((int)MainControl.parties.meretz);
                break;
        }
        current.SetNativeSize();
    }
}



























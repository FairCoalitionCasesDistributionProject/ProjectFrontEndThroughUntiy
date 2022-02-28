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
        switch (MainControl.current){
            case "likud":
                current.sprite=likud;
                break;
            case "yeshAtid":
                current.sprite=yeshAtid;
                break;
            case "shas":
                current.sprite=shas;
                break;
            case "kaholLavan":
                current.sprite=kaholLavan;
                break;
            case "yemina":
                current.sprite=yemina;
                break;
            case "haavoda":
                current.sprite=haavoda;
                break;
            case "israelBeitenu":
                current.sprite=israelBeitenu;
                break;
            case "yahadutHatora":
                current.sprite=yahadutHatora;
                break;
             case "hareshimaHamshutefet":
                current.sprite=hareshimaHamshutefet;
                break;
            case "hareshimaHaaravitHameshutefet":
                current.sprite=hareshimaHaaravitHameshutefet;
                break;
            case "hazionutHadatit":
                current.sprite=hazionutHadatit;
                break;
            case "tikvaHadasha":
                current.sprite=tikvaHadasha;
                break;
            case "meretz":
                current.sprite=meretz;
                break;
        }
    }
}






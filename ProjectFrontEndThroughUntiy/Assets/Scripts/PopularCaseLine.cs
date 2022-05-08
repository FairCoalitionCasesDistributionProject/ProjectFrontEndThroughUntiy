using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PopularCaseLine : MonoBehaviour
{
    public int index;
    public Text caseDefaultName;
    public Text caseName;
    void Start()
    {
        caseDefaultName.text = (GlobalPartyChoose.ministeries[index] == null || GlobalPartyChoose.ministeries[index] == "") ? "Case" + index : GlobalPartyChoose.ministeries[index];
        caseName.text = (GlobalPartyChoose.ministeries[index] == null || GlobalPartyChoose.ministeries[index] == "") ? "Case" + index : GlobalPartyChoose.ministeries[index];
    }
    void Update()
    {
        GlobalPartyChoose.ministeries[index] = (caseName.text != null && caseName.text != "" && !(allSpaces(caseName.text))) ? caseName.text : caseDefaultName.text;
    }
    public bool allSpaces(string str)
    {
        return str.Replace(" ", "").Length == 0;
    }
}

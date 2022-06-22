using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PopularCaseLine : MonoBehaviour
{
    public int index;
    public InputField caseName;
    void Start()
    {
        caseName.text = (GlobalPartyChoose.ministeries[index] == null || GlobalPartyChoose.ministeries[index] == "") ? "Case" + index : GlobalPartyChoose.ministeries[index];
    }
    void Update()
    {
        GlobalPartyChoose.ministeries[index] = (caseName.text != null && caseName.text != "" && !(allSpaces(caseName.text))) ? caseName.text : caseName.text;
    }
    public bool allSpaces(string str)
    {
        return str.Replace(" ", "").Length == 0;
    }
}



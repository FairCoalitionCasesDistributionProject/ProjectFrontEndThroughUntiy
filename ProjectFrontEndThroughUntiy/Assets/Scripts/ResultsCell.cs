using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResultsCell : MonoBehaviour
{
    public int cIndex;
    public int pIndex;
    public Text party;
    public Text value;
    void Start()
    {
        party.text = (GlobalPartyChoose.partyNames[pIndex] != null && GlobalPartyChoose.partyNames[pIndex] != "" && !(allSpaces(GlobalPartyChoose.partyNames[pIndex]))) ? "Party" + pIndex: GlobalPartyChoose.partyNames[pIndex] ;
        value.text = percentage(GlobalPartyChoose.results[cIndex, pIndex]);
    }
    public bool allSpaces(string str)
    {
        return str.Replace(" ", "").Length == 0;
    }
    public string percentage(float value)
    {
        return (value * 100).ToString("0.00") + "%";
    }
}

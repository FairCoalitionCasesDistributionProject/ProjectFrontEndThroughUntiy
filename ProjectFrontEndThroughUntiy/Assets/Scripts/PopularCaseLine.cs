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
        caseDefaultName.text = "Case" + index;
    }
    void Update()
    {
        GlobalPartyChoose.ministeries[index] = (caseName.text == "") ? caseDefaultName.text : caseName.text;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResultLine1 : MonoBehaviour
{
    public int index;
    public Text caseName;
    public GameObject result;
    void Start()
    {
        caseName.text = GlobalPartyChoose.ministeries[index];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowNumber1 : MonoBehaviour
{
    public Text output;
    void Start()
    {
        output.text = MainControl.serverOutput;
    }
}













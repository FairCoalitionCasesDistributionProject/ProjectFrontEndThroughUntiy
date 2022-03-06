using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowNumber1 : MonoBehaviour
{
    public Text number;
    void Start()
    {
        number.text = MainControl.serverOutput + "";
    }
}













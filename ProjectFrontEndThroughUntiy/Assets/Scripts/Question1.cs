using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Question1 : MonoBehaviour
{
    public TextMeshProUGUI text;
    public bool status;
    void Awake()
    {
        status = false;
    }
    public void WasClicked()
    {
        status = !status;
        text.gameObject.SetActive(status);
    }
}






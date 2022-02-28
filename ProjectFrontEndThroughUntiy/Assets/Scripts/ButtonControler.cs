using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class ButtonControler : MonoBehaviour
{
    public void LoadPartyChoose()
    {
        SceneManager.LoadScene("PartyChoose");
    }
    public void LoadInformation()
    {
        SceneManager.LoadScene("Information");
    }
    public void LoadPartyCaseSettings(string str)
    {
        MainControl.current=str;
        SceneManager.LoadScene("PartyCaseSettings");
    }
}

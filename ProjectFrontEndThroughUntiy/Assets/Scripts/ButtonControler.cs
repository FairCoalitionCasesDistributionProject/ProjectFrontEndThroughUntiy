using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
    public void LoadPartyCaseSettings(string partyName)
    {
        MainControl.currentName = partyName;
        SceneManager.LoadScene("PartyCaseSettings");
    }
    public void Back()
    {
        SceneManager.LoadScene(MainControl.lastPage);
    }
}


















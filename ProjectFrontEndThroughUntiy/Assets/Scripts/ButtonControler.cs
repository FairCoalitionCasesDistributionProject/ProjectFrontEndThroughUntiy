using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonControler : MonoBehaviour
{
    //*Function loads the PartyChoose scene.
    public void LoadPartyChoose()
    {
        SceneManager.LoadScene("PartyChoose");
    }
    //*Function loads the Information scene.
    public void LoadInformation()
    {
        SceneManager.LoadScene("Information");
    }
    //* Function loads InfEn scene .
    public void LoadInfEn()
    {
        SceneManager.LoadScene("InfEn");
    }
    //* Function loads PartyCaseSettings scene and sets the static parameter which represents the current party to the received parameter.
    public void LoadPartyCaseSettings(string partyName)
    {
        MainControl.currentName = partyName;
        SceneManager.LoadScene("PartyCaseSettings");
    }
    //*Function loads the scene which name is known the the lastPage parameter.
    public void Back()
    {
        SceneManager.LoadScene(MainControl.lastPage);
    }
}









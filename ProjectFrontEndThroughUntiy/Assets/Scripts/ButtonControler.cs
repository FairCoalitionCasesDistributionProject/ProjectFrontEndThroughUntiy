using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public void LoadPartyCaseSettings()
    {
        SceneManager.LoadScene("PartyCaseSettings");
    }
}

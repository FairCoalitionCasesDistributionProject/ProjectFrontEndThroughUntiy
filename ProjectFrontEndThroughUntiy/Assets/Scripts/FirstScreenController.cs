using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FirstScreenController : MonoBehaviour
{
    public Text key;
    public bool wasChanged = false;
    void Start()
    {
        MainControl.lastPage = "Screen1";
    }
    public void LoadPartyChoose()
    {
        if (wasChanged)
        {
            wasChanged = false;
            string input = baseConversator64To10(key.text);
            MainControl.key = ((input == "") ? "" : input);
            //* TOOD: Check if key is valid.
        }
        MainControl.lastPage = "PartyChoose";
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
    public void Change()
    {
        wasChanged = true;
    }
    public string baseConversator64To10(string input)
    {
        string[] array = input.Split('_');
        string output = "";
        for (int i = 0; i < array.Length; i++)
        {
            output += ((i == 0) ? "" : ".") + Convert.ToInt32(array[i], 64).ToString();
        }
        return output;
    }
}












































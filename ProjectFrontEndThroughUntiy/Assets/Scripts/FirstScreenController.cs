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
    public string baseConversator64To10(string key)
    {
        if (key.Length == 14)
        {
            string[] keyArray = new string[7] { key[0] + "" + key[1], key[2] + "" + key[3], key[4] + "" + key[5], key[6] + "" + key[7], key[8] + "" + key[9], key[10] + "" + key[11], key[12] + "" + key[13] };
            return keyString(keyArray);
        }
        else
        {
            return "";
        }
    }
    public int decode(string key)
    {
        int keyInt = 0;
        for (int i = 0; i < key.Length; i++)
        {
            keyInt += (int)Math.Pow(64, key.Length - i - 1) * (key[i] - 'A');
        }
        return keyInt;
    }
    public string keyString(string[] key)
    {
        string keyString = "";
        for (int i = 0; i < key.Length; i++)
        {
            keyString += decode(key[i]) + ((i == key.Length - 1) ? "" : ".");
        }
        return keyString;
    }
}












































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
        string[] array = input.Split('|');
        string output = "";
        string pre = "";
        for (int i = 0; i < array.Length; i++)
        {
            int deciNum = toDeci(array[i], 61);
            if (deciNum != -1)
            {
                Debug.Log("Error: Invalid input.");
                return "";
            }
            else
            {
                if (i == 0)
                {
                    pre = "";
                }
                else
                {
                    pre = ".";
                }
                output += pre + deciNum;
            }
        }
        return output;
    }
    public int val(char c)
    {
        if (c >= '0' && c <= '9')
        {
            return (int)c - '0';
        }
        else
        {
            return (int)c - 'A' + 10;
        }
    }
    public int toDeci(string str, int toBase)
    {
        int len = str.Length;
        int power = 1;
        int num = 0;
        int i;
        for (i = len - 1; i >= 0; i--)
        {
            if (val(str[i]) >= toBase)
            {
                return -1;
            }
            num += val(str[i]) * power;
            power = power * toBase;
        }
        return num;
    }


}












































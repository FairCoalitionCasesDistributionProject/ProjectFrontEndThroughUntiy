using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
public class FirstScreenController : MonoBehaviour
{
    public Text key;
    public bool wasChanged = false;
    public GameObject loading;
    void Start()
    {
        MainControl.lastPage = "Screen1";
        loading.SetActive(false);
    }
    public void LoadPartyChoose()
    {
        if (wasChanged || key.text != "")
        {
            wasChanged = false;
            string input = baseConversator64To10(key.text);
            MainControl.key = input;
            ReUse();
        }
        else
        {
            MainControl.lastPage = "PartyChoose";
            SceneManager.LoadScene("PartyChoose");
        }
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
            if (deciNum == -1)
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
        for (int i = len - 1; i >= 0; i--)
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
    public void awake()
    {
        string URL = "http://faircol.herokuapp.com/api/";
        string json = "{WakeUpAndBeReady.}";
        StartCoroutine(Upload(URL, json, true));
    }
    public void ReUse()
    {
        string URL = "http://faircol.herokuapp.com/api/getsave";
        string json = "{\"key\":\"" + MainControl.key + "\"}";
        StartCoroutine(Upload(URL, json, false));
    }
    IEnumerator Upload(string URL, string json, bool forWakingUp)
    {
        if (!forWakingUp)
        {
            loading.SetActive(true);
        }
        var uwr = new UnityWebRequest(URL, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");
        yield return uwr.SendWebRequest();
        if (!forWakingUp)
        {
            if (uwr.isNetworkError)
            {
                Debug.Log("Error While Sending: " + uwr.error);
                MainControl.serverOutput = "" + (-1);
            }
            else
            {
                Debug.Log("Received: " + uwr.downloadHandler.text);
                MainControl.serverOutput = uwr.downloadHandler.text;
            }
            Parse(MainControl.serverOutput);
            loading.SetActive(false);
            MainControl.lastPage = "PartyChoose";
            SceneManager.LoadScene("PartyChoose");
        }
    }
    public void Parse(string input)
    {
        if (input == "-1")
        {
            //*TODO: What to do if the input is broken.
            return;
        }
        var cleanedRows = Regex.Split(input.Replace("\"", "").Replace("[", "{").Replace("]", "}"), @"}\s*,\s*{").Select(r => r.Replace("{", "").Replace("}", "").Trim()).ToList();
        int[,] matrix = new int[cleanedRows.Count, 30];
        for (var i = 0; i < cleanedRows.Count; i++)
        {
            var data = cleanedRows.ElementAt(i).Split(',');
            var matrixHelper = data.Select(c => int.Parse(c.Trim())).ToArray();
            for (var j = 0; j < matrixHelper.Length; j++)
            {
                matrix[i, j] = matrixHelper[j];
            }
        }
        MainControl.partyParameters = matrix;
    }
}


































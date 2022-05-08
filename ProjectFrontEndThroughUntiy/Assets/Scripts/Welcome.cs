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
public class Welcome : MonoBehaviour
{
    public void LoadIsraelMode1()
    {
        SceneManager.LoadScene("Screen1");
    }
    public void LoadPopularMode()
    {
        SceneManager.LoadScene("Popular");
    }

#if !UNITY_EDITOR
    void Start()
    {
        string url = Application.absoluteURL;
        string key = baseConversator64To10(remove1(getUrlWithoutDomain(url)));
        if(key != ""){
            MainControl.key = key;
            ReUse();
        }
    }
#endif
    public string remove1(string str)
    {
        return str.Replace("?", "").Replace("/", "");
    }

    public string getUrlWithoutDomain(string url)
    {
        string[] array = url.Split('/');
        string output = "";
        for (int i = 3; i < array.Length; i++)
        {
            output += array[i] + "/";
        }
        return output;
    }
    public void ReUse()
    {
        string URL = MainControl.url + "/getsave";
        string json = "{\"key\":\"" + MainControl.key + "\"}";
        StartCoroutine(Upload(URL, json));
    }
    IEnumerator Upload(string URL, string json)
    {
        var uwr = new UnityWebRequest(URL, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");
        yield return uwr.SendWebRequest();
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
        MainControl.lastPage = "PartyChoose";
        SceneManager.LoadScene("PartyChoose");
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
    public string baseConversator64To10(string input)
    {
        string[] array = input.Split('.');
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
}










































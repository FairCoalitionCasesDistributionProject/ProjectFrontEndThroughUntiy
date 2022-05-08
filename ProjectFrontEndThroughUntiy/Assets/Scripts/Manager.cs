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
public class Manager : MonoBehaviour
{
    public GameObject mainImage;
    public GameObject loading;
    public GameObject settings1;
    public Text settingsInp;
    private bool recievedAnswer = false;
    void Start()
    {
        settings1.SetActive(false);
        loading.SetActive(false);
    }
    public void Send()
    {

        recievedAnswer = false;
        int[] key = CurrentDateTime();
        string draft = "{\"items\":" + MainControl.numberOfCases + ",\"mandates\":" + mandatesString() + ",\"preferences\":" + preferencesString() + ",\"key\": \"" + keyString(key) + "\"}";
        MainControl.key = EncodeTo64(key);
        MainControl.relevantCases = relevantColumnCheck(MainControl.partyParameters);
        MainControl.relevantParties = relevantRowCheck(MainControl.partyParameters);
        MainControl.serverInput = draft;
        Server();
    }
    public string mandatesString()
    {
        string mandates = "[";
        for (int i = 0; i < MainControl.mandates.GetLength(0); i++)
        {
            mandates += ((i == 0 || i == MainControl.mandates.GetLength(0)) ? "" : ",") + MainControl.mandates[i];
        }
        mandates += "]";
        return mandates;
    }
    public string preferencesString()
    {
        string preferences = "[";
        for (int i = 0; i < MainControl.partyParameters.GetLength(0); i++)
        {
            preferences += "[";
            for (int j = 0; j < MainControl.partyParameters.GetLength(1); j++)
            {
                preferences += MainControl.partyParameters[i, j] + "" + ((j == MainControl.partyParameters.GetLength(1) - 1) ? "" : ",");
            }
            preferences += "]" + ((i == MainControl.partyParameters.GetLength(0) - 1) ? "" : ",");
        }
        preferences += "]";
        return preferences;
    }
    public bool[] relevantRowCheck(int[,] array)
    {
        bool[] check = new bool[array.GetLength(0)];
        for (int i = 0; i < array.GetLength(0); i++)
        {
            int sum = 0;
            for (int j = 0; j < array.GetLength(1); j++)
            {
                sum += array[i, j];
            }
            check[i] = (sum != 0);
        }
        return check;
    }
    public bool[] relevantColumnCheck(int[,] array)
    {
        bool[] check = new bool[array.GetLength(1)];
        for (int i = 0; i < array.GetLength(1); i++)
        {
            int sum = 0;
            for (int j = 0; j < array.GetLength(0); j++)
            {
                sum += array[j, i];
            }
            check[i] = (sum != 0);
        }
        return check;
    }
    public void Server()
    {
        StartCoroutine(Upload());
    }
    IEnumerator Upload()
    {
        mainImage.SetActive(false);
        loading.SetActive(true);
        string URL = MainControl.url;
        string json = MainControl.serverInput;
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
        if (recievedAnswer)
        {
            SceneManager.LoadScene("Results");
        }
    }
    public void Parse(string input)
    {
        if (input == "-1")
        {
            //*TODO: What to do if the input is broken.
            return;
        }
        var cleanedRows = Regex.Split(input.Replace("\"", ""), @"}\s*,\s*{").Select(r => r.Replace("{", "").Replace("}", "").Trim()).ToList();
        float[,] matrix = new float[cleanedRows.Count, 13];
        for (var i = 0; i < cleanedRows.Count; i++)
        {
            var data = cleanedRows.ElementAt(i).Split(',');
            var matrixHelper = data.Select(c => float.Parse(c.Trim())).ToArray();
            for (var j = 0; j < matrixHelper.Length; j++)
            {
                matrix[i, j] = matrixHelper[j];
            }
        }
        MainControl.results = matrix;
        recievedAnswer = true;
    }
    public int[] CurrentDateTime()
    {
        DateTime currentDateTime = DateTime.Now;
        return new int[7] { currentDateTime.Year, currentDateTime.Month, currentDateTime.Day, currentDateTime.Hour, currentDateTime.Minute, currentDateTime.Second, currentDateTime.Millisecond };
    }
    public string keyString(int[] key)
    {
        string output = "";
        for (int i = 0; i < key.Length; i++)
        {
            output += "" + key[i] + ((i < (key.Length - 1)) ? "." : "");
        }
        return output;
    }
    public string EncodeTo64(int[] input)
    {
        string output = "";
        for (int i = 0; i < input.Length; i++)
        {
            output += ((i == 0) ? "" : ".") + fromDeci(61, input[i]);
        }
        return output;
    }
    public char reVal(int num)
    {
        if (num >= 0 && num <= 9)
        {
            return (char)(num + 48);
        }
        else
        {
            return (char)(num - 10 + 65);
        }
    }
    public string fromDeci(int base1, int inputNum)
    {
        string s = "";
        while (inputNum > 0)
        {
            s += reVal(inputNum % base1);
            inputNum /= base1;
        }
        char[] res = s.ToCharArray();
        Array.Reverse(res);
        return new String(res);
    }
    public void settingsPressed()
    {
        settings1.SetActive(true);
    }
    public void save1()
    {
        if (settingsInp.text != "")
        {
            MainControl.url = settingsInp.text;
        }
        settings1.SetActive(false);
    }
    public void cancel1()
    {
        settings1.SetActive(false);
    }
}

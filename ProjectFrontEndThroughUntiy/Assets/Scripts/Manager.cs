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
    void Start()
    {
        loading.SetActive(false);
    }
    public void Send()
    {
        int[] key = CurrentDateTime();
        string draft = "{\"key\":" + keyString(key) + "\"items\":" + MainControl.numberOfCases + ",\"mandates\":" + mandatesString() + ",\"preferences\":" + preferencesString() + "}";
        MainControl.key = EncodeTo64(key);
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
    public void Server()
    {
        StartCoroutine(Upload());
    }
    IEnumerator Upload()
    {
        mainImage.SetActive(false);
        loading.SetActive(true);
        string URL = "http://faircol.herokuapp.com/api/";
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
        SceneManager.LoadScene("Results");
    }
    public void Parse(string input)
    {
        if (input == "-1")
        {
            //*TODO: What to do if the input is broken.
            return;
        }
        var cleanedRows = Regex.Split(input, @"}\s*,\s*{").Select(r => r.Replace("{", "").Replace("}", "").Trim()).ToList();
        float[,] matrix = new float[cleanedRows.Count, 13];
        for (var i = 0; i < cleanedRows.Count; i++)
        {
            var data = cleanedRows.ElementAt(i).Split(',');
            float[] matrixHelper = data.Select(c => float.Parse(c.Trim())).ToArray();
            for (var j = 0; j < matrixHelper.Length; j++)
            {
                matrix[i, j] = matrixHelper[j];
            }
        }
        MainControl.results = matrix;
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
            output += key[i] + ".";
        }
        return output;
    }







    public string EncodeTo64(int[] key)
    {
        string output = "";
        for (int i = 0; i < key.Length; i++)
        {
            string current = baseConversator10To64(key[i]);
            output += (key[i] < 64) ? "A" + current : current;
        }
        return output;
    }
    public string baseConversator10To64(int number)
    {
        string output = Convert.ToBase64String(Encoding.UTF8.GetBytes(number.ToString()));
        return output;
    }


}



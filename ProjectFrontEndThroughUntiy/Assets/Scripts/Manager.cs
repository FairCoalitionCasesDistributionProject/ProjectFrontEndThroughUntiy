using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.IO;

public class Manager : MonoBehaviour
{
    public void Send()
    {
        string draft = "{\"items\":" + MainControl.numberOfCases + ",\"mandates\":" + mandatesString() + ",\"preferences\":" + preferencesString() + "}";
        MainControl.serverInput = draft;
        SceneManager.LoadScene("Loading");
        
        
        
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
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            MainControl.serverOutput = uwr.downloadHandler.text;
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
        var cleanedRows = Regex.Split(input, @"}\s*,\s*{").Select(r => r.Replace("{", "").Replace("}", "").Trim()).ToList();
        var matrix = new float[cleanedRows.Count][];
        for (var i = 0; i < cleanedRows.Count; i++)
        {
            var data = cleanedRows.ElementAt(i).Split(',');
            matrix[i] = data.Select(c => float.Parse(c.Trim())).ToArray();
        }
        //*MainControl.results = matrix;
    }
}



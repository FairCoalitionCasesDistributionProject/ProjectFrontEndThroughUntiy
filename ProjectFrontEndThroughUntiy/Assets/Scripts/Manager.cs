using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
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
        string draft = "{\"items\":" + MainControl.numberOfCases + ",\n\"mandates\":" + mandatesString() + ",\n\"preferences\":" + preferencesString() + "}";
        MainControl.serverInput = draft;
        //*SceneManager.LoadScene("Results");
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
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:8000/api/");
        request.Method = "POST";
        // ... just sending a string
        string data = MainControl.serverInput;
        request.ContentType = "application/json";
        request.ContentLength = data.Length;
        StreamWriter streamWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
        streamWriter.Write(data);
        streamWriter.Close();
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        if (response.StatusCode == HttpStatusCode.OK)
        {
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            MainControl.serverOutput = reader.ReadToEnd();
            Parse(MainControl.serverOutput);
        }
        response.Close();
    }
    public void Parse(string input)
    {
        if (input == "-1")
        {
            //*TODO: What to do if the input is broken.
            return;
        }
        var cleanedRows = Regex.Split(input, @"]\s*,\s*[").Select(r => r.Replace("[", "").Replace("]", "").Trim()).ToList();

        var matrix = new float[cleanedRows.Count][];
        for (var i = 0; i < cleanedRows.Count; i++)
        {
            var data = cleanedRows.ElementAt(i).Split(',');
            matrix[i] = data.Select(c => float.Parse(c.Trim())).ToArray();
        }
        MainControl.results = matrix;
    }
}
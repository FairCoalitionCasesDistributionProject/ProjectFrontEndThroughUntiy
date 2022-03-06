using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Net;
public class Manager : MonoBehaviour
{
    public void Send()
    {
        string draft = "{\"items\":" + MainControl.numberOfCases + ",\n\"mandates\":" + mandatesString() + ",\n\"preferences\":" + preferencesString() + "}";
        MainControl.serverInput = draft;
        //*SceneManager.LoadScene("CasesChoose");
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
                preferences += MainControl.partyParameters[i, j] + "" + ((j == MainControl.partyParameters.GetLength(1)-1) ? "" : ",");
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
            string strResponse = reader.ReadToEnd();
            Debug.Log(strResponse);
        }
        response.Close();
    }
}

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
using TMPro;
public enum typeRun
{
    EMPTY = -1,
    EN = 0,
    IL24 = 1,
}


public enum parce
{
    items = 0,
    mandates = 1,
    preferences = 2,
    key = 3,
    type = 4,
    partynames = 5,
    numberofparties = 6,
    ministeries = 7,
    amountofmandate = 8,

}
public class Welcome : MonoBehaviour
{
    public GameObject question;
    public Text enQuestion;
    public TextMeshProUGUI il24Question;
    public TextMeshProUGUI il24Apply;
    public TextMeshProUGUI il24Cancel;
    public Text enApply;
    public Text enCancel;
    public Image image;
    public typeRun type1;
    public GameObject load1;
    public void LoadIsraelMode1()
    {
        SceneManager.LoadScene("Screen1");
    }
    public void LoadPopularMode()
    {
        SceneManager.LoadScene("Popular");
    }
    void Start()
    {
        load1.SetActive(false);
        question.SetActive(false);
        string url = Application.absoluteURL;
        MainControl.lastPage = "Main1";
        if (url.Contains("?") && url.Length > url.IndexOf("?") + 1)
        {
            string query = remove1(getUrlWithoutDomain(url));
            (query, type1) = il24OrEn(query);
            string key = baseConversator64To10(query);
            if (key != "")
            {
                MainControl.key = (type1 == typeRun.EMPTY) ? "" + key : ((type1 == typeRun.IL24) ? "IL24." + key : "EN." + key);
                question.SetActive(true);
                if (type1 == typeRun.EMPTY || type1 == typeRun.IL24)
                {
                    il24Question.text = "האם ברצונך לשחזר הרצה ?";
                    il24Apply.gameObject.SetActive(true);
                    il24Cancel.gameObject.SetActive(true);
                }
                else
                {
                    enQuestion.text = "Do you want to reload the session ?";
                    enApply.gameObject.SetActive(true);
                    enCancel.gameObject.SetActive(true);
                }
            }
        }
    }
    public (string, typeRun) il24OrEn(string str)
    {
        if (str.Contains("EN."))
        {
            return (str.Replace("EN.", ""), typeRun.EN);
        }
        else
        {
            if (str.Contains("IL24."))
            {
                return (str.Replace("IL24.", ""), typeRun.IL24);
            }
            else
            {
                return (str, typeRun.EMPTY);
            }
        }
    }
    public string remove1(string str)
    {
        return str.Replace("?", "").Replace("/", "");
    }
    public void applyWasClicked()
    {
        load1.SetActive(true);
        image.gameObject.SetActive(false);
        ReUse();
    }
    public void cancelWasClick1()
    {
        question.SetActive(false);
        MainControl.key = "";
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
        string URL = MainControl.url + "getsave";
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
        if (type1 == typeRun.IL24 || type1 == typeRun.EMPTY)
        {
            Parse(MainControl.serverOutput);
            MainControl.lastPage = "PartyChoose";
            SceneManager.LoadScene("PartyChoose");
        }
        else
        {
            ParseVerEn1(MainControl.serverOutput);
            SceneManager.LoadScene("Popular");
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
    public string changeComma(string str, char c)
    {
        string output = "";
        int inBrackets = 0;
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == '[')
            {
                inBrackets++;
            }
            if (str[i] == ']')
            {
                inBrackets--;
            }
            if (str[i] == ',' && inBrackets == 0)
            {
                output += c;
            }
            else
            {
                output += str[i];
            }
        }
        return output;
    }
    public char findUncontainedChar(string str)
    {
        for (int i = 0; i < 65536; i++)
        {
            if (!str.Contains((char)i))
            {
                return (char)i;
            }
        }
        return '|';
    }
    public void ParseVerEn1(string input)
    {
        char c = findUncontainedChar(input);
        input = changeComma(input, c);
        string[] inputArray = input.Split(c);
        MainControl.inputArray = new string[inputArray.Length];
        for (int i = 0; i < inputArray.Length; i++)
        {
            inputArray[i] = inputArray[i].Replace("{", "").Replace("\"", "").Replace("\\", "").Replace("u200b", "").Replace(" ", "").Replace("}", "");
            int index = inputArray[i].IndexOf(':');
            if (index != -1)
            {
                index += 1;
                inputArray[i] = inputArray[i].Substring(index);
            }
            Debug.Log(inputArray[i]);
            MainControl.inputArray[i] = inputArray[i];
        }
        MainControl.session = true;
    }
}













































































using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.Networking;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
public class GlobalPartyChoose : MonoBehaviour
{
    public Text numberOfParties;
    public Text amountOfMandate;
    public Text numberOfMinisteries;
    public Button confirm;
    public GameObject alert;
    public Text alertText;
    public GameObject positions;
    public GameObject settings0;
    public GameObject settings1;
    public GameObject partyChooseLine;
    public GameObject popularCaseLine;
    public int timeConfirm;
    public static string[] ministeries;
    public static string[] partyNames;
    public static int[] mandates;
    public static int[,] partyParameters;
    public static int summary;
    public Button back;
    public GameObject preferences;
    public GameObject partyScreen;
    public GameObject party;
    public Text partyName;
    public static bool wasClicked;
    public static int preferenceIndex;
    public static string serverOutput;
    public static float[,] results;
    public bool recievedAnswer = false;
    public GameObject loading;
    public GameObject resultLine1;
    void Start()
    {
        partyScreen.SetActive(false);
        settings0.SetActive(true);
        settings1.SetActive(false);
        alert.SetActive(false);
        timeConfirm = 0;
        back.interactable = false;
        wasClicked = false;
        preferenceIndex = -1;
        recievedAnswer = false;
        loading.SetActive(false);
    }
    void Update()
    {
        if (timeConfirm == 3)
        {
            int sum = 0;
            foreach (Transform child in positions.transform)
            {
                GameObject now = child.gameObject;
                if (wasClicked)
                {
                    partyScreen.SetActive(true);
                    float height = 41f;
                    float numberOfInstantiation = -5.79f;
                    preferences.GetComponent<RectTransform>().sizeDelta = new Vector2(preferences.GetComponent<RectTransform>().sizeDelta.x, (ministeries.Length * height) - 447.5f);
                    for (int i = 0; i < ministeries.Length; i++)
                    {
                        numberOfInstantiation++;
                        GameObject newPartyChooseLine = Instantiate(party, transform.position, transform.rotation, preferences.transform);
                        newPartyChooseLine.transform.position = new Vector3(480, ((-1) * height * i) + 447.5f, 0);
                        newPartyChooseLine.GetComponent<PartySlider>().party = preferenceIndex;
                        newPartyChooseLine.GetComponent<PartySlider>().index = i;
                    }
                    partyName.text = (partyName.text != null && partyName.text != "" && !(allSpaces(partyName.text))) ? partyNames[preferenceIndex] : "Party" + preferenceIndex;
                }
                wasClicked = false;
                preferenceIndex = -1;
                sum += ((int)(now.GetComponent<PartyChooseLine>().partyMandatesSlider.value));
            }
            GlobalPartyChoose.summary = sum;
        }
    }
    public bool allSpaces(string str)
    {
        return str.Replace(" ", "").Length == 0;
    }
    public void confirmPressed(int control)
    {
        timeConfirm += control;
        switch (timeConfirm)
        {
            case 0:
                foreach (Transform child in positions.transform)
                {
                    Destroy(child.gameObject);
                }
                back.interactable = false;
                settings0.SetActive(true);
                settings1.SetActive(false);
                break;
            case 1:
                foreach (Transform child in positions.transform)
                {
                    Destroy(child.gameObject);
                }
                if ((numberOfMinisteries.text == "" || !int.TryParse(numberOfMinisteries.text, out int numberOfMinisteriesInt)))
                {
                    alertShow(true, "Please enter a number .", 1.5f);
                    timeConfirm--;
                }
                else
                {
                    settings0.SetActive(false);
                    settings1.SetActive(false);
                    numberOfMinisteriesInt = int.Parse(numberOfMinisteries.text);
                    if (numberOfMinisteriesInt <= 0)
                    {
                        alertShow(true, "Please enter a number greater than 0.", 1.5f);
                        timeConfirm--;
                    }
                    else
                    {
                        if (control > 0)
                        {
                            ministeries = new string[numberOfMinisteriesInt];
                        }
                        settings0.SetActive(false);
                        back.interactable = true;
                        float height = 49f;
                        float numberOfInstantiation = -5.79f;
                        positions.GetComponent<RectTransform>().sizeDelta = new Vector2(positions.GetComponent<RectTransform>().sizeDelta.x, (numberOfMinisteriesInt * height) - 447.5f);
                        for (int i = 0; i < numberOfMinisteriesInt; i++)
                        {
                            numberOfInstantiation++;
                            GameObject newPartyChooseLine = Instantiate(popularCaseLine, transform.position, transform.rotation, positions.transform);
                            newPartyChooseLine.transform.position = new Vector3(480, ((-1) * height * i) + 447.5f, 0);
                            newPartyChooseLine.GetComponent<PopularCaseLine>().index = i;
                        }
                    }
                }
                break;
            case 2:
                foreach (Transform child in positions.transform)
                {
                    Destroy(child.gameObject);
                }
                settings0.SetActive(false);
                settings1.SetActive(true);
                break;
            case 3:
                foreach (Transform child in positions.transform)
                {
                    Destroy(child.gameObject);
                }
                if ((numberOfParties.text == "" || !int.TryParse(numberOfParties.text, out int numberOfPartiesInt)) ||
                    (amountOfMandate.text == "" || !int.TryParse(amountOfMandate.text, out int amountOfMandateInt)))
                {
                    alertShow(true, "Please enter a number .", 1.5f);
                    timeConfirm--;
                }
                else
                {
                    amountOfMandateInt = int.Parse(amountOfMandate.text);
                    numberOfPartiesInt = int.Parse(numberOfParties.text);
                    if (amountOfMandateInt <= 0 || numberOfPartiesInt <= 0)
                    {
                        alertShow(true, "Please enter a number greater than 0.", 1.5f);
                        timeConfirm--;
                    }
                    else
                    {
                        if (control > 0)
                        {
                            partyNames = new string[numberOfPartiesInt];
                            mandates = new int[numberOfPartiesInt];
                            partyParameters = new int[numberOfPartiesInt, ministeries.Length];
                            results = new float[ministeries.Length, numberOfPartiesInt];
                        }
                        confirm.GetComponentInChildren<Text>().text = "Calculate";
                        settings1.SetActive(false);
                        float height = -41f;
                        float numberOfInstantiation = -5.79f;
                        positions.GetComponent<RectTransform>().sizeDelta = new Vector2(positions.GetComponent<RectTransform>().sizeDelta.x, (numberOfPartiesInt * 41) - 447.5f);
                        for (int i = 0; i < numberOfPartiesInt; i++)
                        {
                            numberOfInstantiation++;
                            GameObject newPartyChooseLine = Instantiate(partyChooseLine, transform.position, transform.rotation, positions.transform);
                            newPartyChooseLine.transform.position = new Vector3(480, (height * i) + 447.5f, 0);
                            newPartyChooseLine.GetComponent<PartyChooseLine>().index = i;
                            newPartyChooseLine.GetComponent<PartyChooseLine>().amountOfMandates = amountOfMandateInt;
                        }
                    }
                }
                break;
            case 4:
                bool allMandatesFilled = true;
                foreach (Transform child in positions.transform)
                {
                    if (child.gameObject.GetComponent<PartyChooseLine>().partyMandatesSlider.value == 0)
                    {
                        alertShow(true, "All the members of the coalition should have some amount of mandates (not zero) .", 1.5f);
                        timeConfirm--;
                        allMandatesFilled = false;
                        break;
                    }
                }
                if (allMandatesFilled)
                {
                    foreach (Transform child in positions.transform)
                    {
                        Destroy(child.gameObject);
                    }
                    StartCoroutine(Upload());
                    break;
                }
                break;
        }
    }
    public void alertShow(bool show, string message, float time)
    {
        alert.SetActive(show);
        alertText.text = message;
        StartCoroutine(hideAlert(time));
    }
    IEnumerator hideAlert(float time)
    {
        yield return new WaitForSeconds(time);
        alert.SetActive(false);
    }
    public void backFromPartyWasPressed()
    {
        foreach (Transform child in preferences.transform)
        {
            Destroy(child.gameObject);
        }
        partyScreen.SetActive(false);
    }
    IEnumerator Upload()
    {
        loading.SetActive(true);
        foreach (Transform child in preferences.transform)
        {
            Destroy(child.gameObject);
        }
        string URL = "http://faircol.herokuapp.com/api/";
        int[] key = CurrentDateTime();
        string json = "{\"items\":" + ministeries.Length + ",\"mandates\":" + mandatesString() + ",\"preferences\":" + preferencesString() + ",\"key\": \"" + keyString(key) + "\"}";
        var uwr = new UnityWebRequest(URL, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");
        yield return uwr.SendWebRequest();
        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
            serverOutput = "" + (-1);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            serverOutput = uwr.downloadHandler.text;
        }
        Parse(serverOutput);
        if (recievedAnswer)
        {
            loading.SetActive(false);
            Debug.Log("Recieved");
            showResults();
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
        float[,] matrix = new float[cleanedRows.Count, partyNames.Length];
        for (var i = 0; i < cleanedRows.Count; i++)
        {
            var data = cleanedRows.ElementAt(i).Split(',');
            var matrixHelper = data.Select(c => float.Parse(c.Trim())).ToArray();
            for (var j = 0; j < matrixHelper.Length; j++)
            {
                matrix[i, j] = matrixHelper[j];
            }
        }
        results = matrix;
        recievedAnswer = true;
    }

    public int[] CurrentDateTime()
    {
        DateTime currentDateTime = DateTime.Now;
        return new int[7] { currentDateTime.Year, currentDateTime.Month, currentDateTime.Day, currentDateTime.Hour, currentDateTime.Minute, currentDateTime.Second, currentDateTime.Millisecond };
    }
    public string mandatesString()
    {
        string mandates1 = "[";
        for (int i = 0; i < mandates.GetLength(0); i++)
        {
            mandates1 += ((i == 0 || i == mandates.GetLength(0)) ? "" : ",") + mandates[i];
        }
        mandates1 += "]";
        return mandates1;
    }
    public string preferencesString()
    {
        string preference1 = "[";
        for (int i = 0; i < partyParameters.GetLength(0); i++)
        {
            preference1 += "[";
            for (int j = 0; j < partyParameters.GetLength(1); j++)
            {
                preference1 += partyParameters[i, j] + "" + ((j == partyParameters.GetLength(1) - 1) ? "" : ",");
            }
            preference1 += "]" + ((i == partyParameters.GetLength(0) - 1) ? "" : ",");
        }
        preference1 += "]";
        return preference1;
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
    public void showResults()
    {
        float height = 147.89f;
        float numberOfInstantiation = (1/5.79f);
        positions.GetComponent<RectTransform>().sizeDelta = new Vector2(positions.GetComponent<RectTransform>().sizeDelta.x, (ministeries.Length * height) - 447.5f);
        for (int i = 0; i < ministeries.Length; i++)
        {
            numberOfInstantiation++;
            GameObject newPartyChooseLine = Instantiate(resultLine1, transform.position, transform.rotation, positions.transform);
            newPartyChooseLine.transform.position = new Vector3(480, ((-1) * height * numberOfInstantiation) + 567.495f, 0);
            newPartyChooseLine.GetComponent<ResultLine1>().index = i;
        }
    }
}

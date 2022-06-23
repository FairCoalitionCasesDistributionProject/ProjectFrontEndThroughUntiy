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
    public InputField numberOfPartiesPlaceHolders;
    public InputField amountOfMandatePlaceHolders;
    public InputField numberOfMinisteriesPlaceHolder;
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
    public GameObject back1;
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
    public bool numberOfCasesWasChanged = true;
    public bool numberOfPartiesWasChanged = true;
    public bool numberOfMandatesWasChanged = true;
    public GameObject session;
    public Button copy1;
    public Text key01;
    public Scrollbar scrollbar;
    public GameObject info;
    public bool[] session01 = new bool[4];
    public GameObject infoResults;
    public Text infoResultsText;
    public Button infoResultsClose1;
    public static bool infoResultsJump = false;
    public static int infoResultParty;
    public static int infoResultCase1;
    public Scrollbar scrollbarForPreferences;
    public GameObject explainTheResults;
    void Awake()
    {
        partyScreen.SetActive(false);
        settings0.SetActive(true);
        settings1.SetActive(false);
        alert.SetActive(false);
        timeConfirm = 0;
        back1.SetActive(false);
        back.interactable = false;
        back.enabled = false;
        wasClicked = false;
        preferenceIndex = -1;
        recievedAnswer = false;
        loading.SetActive(false);
        MainControl.lastPage = "Popular";
        session.SetActive(false);
        if (MainControl.session)
        {
            for (int i = 0; i < session01.Length; i++)
            {
                session01[i] = true;
            }
            numberOfCasesWasChanged = false;
            numberOfPartiesWasChanged = false;
            numberOfMandatesWasChanged = false;
            confirmPressed(0);
        }
    }
    void Start()
    {
        partyScreen.SetActive(false);
        settings0.SetActive(true);
        settings1.SetActive(false);
        alert.SetActive(false);
        loading.SetActive(false);
        MainControl.lastPage = "Popular";
    }
    void Update()
    {
        if (infoResultsJump)
        {
            infoResultsJump = false;
            showInfoResults();
            int sumOfMandatesParam1 = sumOfMandates();
            float satisfiedParam1 = satisfied(infoResultParty);
            int sumOfChoiceParam1 = sumOfChoice(infoResultParty);
            infoResText("\nThe party " + partyNames[infoResultParty] + " has " + mandates[infoResultParty] + "/" + sumOfMandatesParam1 + "=" + percentage((float)mandates[infoResultParty] / (float)sumOfMandatesParam1) + " of the total mandates and got total value of " + satisfiedParam1 + "/" + sumOfChoiceParam1 + "=" + percentage(satisfiedParam1 / sumOfChoiceParam1) + ".");
        }
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
                    preferences.GetComponent<RectTransform>().sizeDelta = new Vector2(preferences.GetComponent<RectTransform>().sizeDelta.x, (ministeries.Length > 11) ? ((ministeries.Length - 10) * height) : preferences.GetComponent<RectTransform>().sizeDelta.y);
                    for (int i = 0; i < ministeries.Length; i++)
                    {
                        numberOfInstantiation++;
                        GameObject newPartyChooseLine = Instantiate(party, transform.position, transform.rotation, preferences.transform);
                        newPartyChooseLine.transform.position = new Vector3(0, ((-1) * height * i) + 173f, 0);
                        newPartyChooseLine.GetComponent<PartySlider>().party = preferenceIndex;
                        newPartyChooseLine.GetComponent<PartySlider>().index = i;
                    }
                    partyName.text = partyNames[preferenceIndex];
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
                confirm.GetComponentInChildren<Text>().text = "Confirm";
                foreach (Transform child in positions.transform)
                {
                    Destroy(child.gameObject);
                }
                session.SetActive(false);
                explainTheResults.SetActive(false);
                positions.GetComponent<RectTransform>().sizeDelta = new Vector2(positions.GetComponent<RectTransform>().sizeDelta.x, 0);
                scrollbar.value = 1;
                back1.SetActive(false);
                back.interactable = false;
                back.enabled = false;
                settings0.SetActive(true);
                settings1.SetActive(false);
                if (session01[0])
                {
                    numberOfMinisteries.text = MainControl.inputArray[(int)parce.items];
                    numberOfMinisteriesPlaceHolder.text = MainControl.inputArray[(int)parce.items];
                    session01[0] = false;
                }
                break;
            case 1:
                session.SetActive(false);
                confirm.GetComponentInChildren<Text>().text = "Confirm";
                explainTheResults.SetActive(false);
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
                        if (session01[1])
                        {
                            ministeries = stringToStringArray(MainControl.inputArray[(int)parce.ministeries]);
                            session01[1] = false;
                        }
                        else
                        {
                            if (control > 0 && numberOfCasesWasChanged)
                            {
                                ministeries = stringArray(numberOfMinisteriesInt, "Case");
                                numberOfCasesWasChanged = false;
                            }
                        }
                        settings0.SetActive(false);
                        back1.SetActive(true);
                        back.enabled = true;
                        back.interactable = true;
                        float height = 49f;
                        float numberOfInstantiation = -5.79f;
                        positions.GetComponent<RectTransform>().sizeDelta = new Vector2(positions.GetComponent<RectTransform>().sizeDelta.x, (numberOfMinisteriesInt > 9) ? ((numberOfMinisteriesInt - 8) * (height + 0.15f)) : positions.GetComponent<RectTransform>().sizeDelta.y);
                        for (int i = 0; i < numberOfMinisteriesInt; i++)
                        {
                            numberOfInstantiation++;
                            GameObject newPartyChooseLine = Instantiate(popularCaseLine, Vector3.zero, Quaternion.identity, positions.transform);
                            newPartyChooseLine.transform.position = new Vector3(0, ((-1) * height * i) + 172.3859f, 0);
                            newPartyChooseLine.GetComponent<PopularCaseLine>().index = i;
                        }
                    }
                }
                break;
            case 2:
                confirm.GetComponentInChildren<Text>().text = "Confirm";
                foreach (Transform child in positions.transform)
                {
                    Destroy(child.gameObject);
                }
                positions.GetComponent<RectTransform>().sizeDelta = new Vector2(positions.GetComponent<RectTransform>().sizeDelta.x, 0);
                scrollbar.value = 1;
                settings0.SetActive(false);
                session.SetActive(false);
                if (session01[2])
                {
                    numberOfParties.text = MainControl.inputArray[(int)parce.numberofparties];
                    numberOfPartiesPlaceHolders.text = MainControl.inputArray[(int)parce.numberofparties];
                    amountOfMandate.text = MainControl.inputArray[(int)parce.amountofmandate];
                    amountOfMandatePlaceHolders.text = MainControl.inputArray[(int)parce.amountofmandate];
                    session01[2] = false;
                }
                explainTheResults.SetActive(false);
                settings1.SetActive(true);
                break;
            case 3:
                explainTheResults.SetActive(false);
                confirm.GetComponentInChildren<Text>().text = "Confirm";
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
                    session.SetActive(false);
                    numberOfPartiesInt = int.Parse(numberOfParties.text);
                    if (amountOfMandateInt <= 0 || numberOfPartiesInt <= 0)
                    {
                        alertShow(true, "Please enter a number greater than 0.", 1.5f);
                        timeConfirm--;
                    }
                    else
                    {
                        if (numberOfPartiesInt > amountOfMandateInt)
                        {
                            alertShow(true, "Number of parties must be greater than number of mandates .", 1.5f);
                            timeConfirm--;
                        }
                        else
                        {
                            if (session01[3])
                            {
                                partyNames = stringToStringArray(MainControl.inputArray[(int)parce.partynames]);
                                mandates = stringToArray(MainControl.inputArray[(int)parce.mandates]);
                                partyParameters = stringToIntMatrix(MainControl.inputArray[(int)parce.preferences]);
                                results = new float[ministeries.Length, numberOfPartiesInt];
                                session01[3] = false;
                            }
                            else
                            {
                                if (control > 0 && numberOfPartiesWasChanged)
                                {
                                    partyNames = stringArray(numberOfPartiesInt, "Party");
                                    mandates = new int[numberOfPartiesInt];
                                    partyParameters = new int[numberOfPartiesInt, ministeries.Length];
                                    results = new float[ministeries.Length, numberOfPartiesInt];
                                    numberOfPartiesWasChanged = false;
                                }
                            }
                            confirm.GetComponentInChildren<Text>().text = "Calculate";
                            settings1.SetActive(false);
                            float height = -41f;
                            float numberOfInstantiation = -5.79f;
                            positions.GetComponent<RectTransform>().sizeDelta = new Vector2(positions.GetComponent<RectTransform>().sizeDelta.x, (numberOfPartiesInt > 11) ? ((numberOfPartiesInt - 10) * 41) : positions.GetComponent<RectTransform>().sizeDelta.y);
                            for (int i = 0; i < numberOfPartiesInt; i++)
                            {
                                numberOfInstantiation++;
                                GameObject newPartyChooseLine = Instantiate(partyChooseLine, Vector3.zero/*transform.position*/, Quaternion.identity/*transform.rotation*/, positions.transform);
                                newPartyChooseLine.transform.position = new Vector3(0, (height * i) + 172.569f /*+ 447.5f*/, 0);
                                newPartyChooseLine.GetComponent<PartyChooseLine>().index = i;
                                newPartyChooseLine.GetComponent<PartyChooseLine>().amountOfMandates = amountOfMandateInt;
                            }
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
                    back.interactable = false;
                    back.enabled = false;
                    back1.SetActive(false);
                    timeConfirm = -1;
                    confirm.GetComponentInChildren<Text>().text = "Recalculate";
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
        scrollbarForPreferences.value = 1;
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
        string json = "{\"items\":" + ministeries.Length + ",\"mandates\":" + mandatesString() + ",\"preferences\":" + preferencesString() + ",\"key\": \"EN." + keyString(key) + "\",\"type\":0,\"partynames\":" + arrayToString(partyNames) + ",\"numberofparties\":" + numberOfParties.text + ",\"ministeries\":" + arrayToString(ministeries) + ",\"amountofmandate\":" + amountOfMandate.text + "}";
        Debug.Log(json);
        key01.text = "EN." + EncodeTo64(key);
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
        session.SetActive(true);
        float height = 147.89f;
        float numberOfInstantiation = (1 / 5.79f);
        positions.GetComponent<RectTransform>().sizeDelta = new Vector2(positions.GetComponent<RectTransform>().sizeDelta.x, (ministeries.Length > 3) ? ((ministeries.Length - 2) * height) : positions.GetComponent<RectTransform>().sizeDelta.y);
        explainTheResults.SetActive(true);
        for (int i = 0; i < ministeries.Length; i++)
        {
            numberOfInstantiation++;
            GameObject newPartyChooseLine = Instantiate(resultLine1, Vector3.zero, Quaternion.identity, positions.transform);
            newPartyChooseLine.transform.position = new Vector3(0, ((-1) * height * i) + 173f, 0);
            newPartyChooseLine.GetComponent<ResultLine1>().index = i;
        }
    }
    public string[] stringArray(int length, string value)
    {
        string[] output = new string[length];
        for (int i = 0; i < length; i++)
        {
            output[i] = value + "" + i;
        }
        return output;
    }
    public void caseNumberWasChanged()
    {
        numberOfCasesWasChanged = true;
    }
    public void partyNumberWasChanged()
    {
        numberOfPartiesWasChanged = true;
    }
    public void mandatesWasChanged()
    {
        numberOfMandatesWasChanged = true;
    }
    public void copyTheKey()
    {
        Application.ExternalEval("prompt(\"Copy the following link to reload this session later:\",\"" + domain(Application.absoluteURL) + "?" + key01.text + "\")");
    }
    public string domain(string url)
    {
        string[] array = url.Split('/');
        string output = "";
        for (int i = 0; i < 3; i++)
        {
            output += array[i] + "/";
        }
        return output;
    }
    public string arrayToString(string[] array)
    {
        string output = "[";
        for (int i = 0; i < array.Length; i++)
        {
            output += "\"" + array[i] + "\"" + ((i < (array.Length - 1)) ? "," : "");
        }
        output += "]";
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
    public int[] stringToArray(string input)
    {
        string[] array = input.Replace("[", "").Replace("]", "").Split(',');
        int[] output = new int[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            output[i] = int.Parse(array[i]);
        }
        return output;
    }
    public string[] stringToStringArray(string input)
    {
        string[] array = input.Replace("[", "").Replace("]", "").Split(',');
        string[] output = new string[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            output[i] = array[i];
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
    public int[,] stringToIntMatrix(string input)
    {
        char separator = findUncontainedChar(input);
        string[] array = input.Replace("],", "" + separator).Replace("[", "").Replace("]", "").Split(separator);
        int[,] output = new int[array.Length, array[0].Split(',').Length];
        for (int i = 0; i < array.Length; i++)
        {
            string[] array2 = array[i].Split(',');
            for (int j = 0; j < array2.Length; j++)
            {
                output[i, j] = int.Parse(array2[j]);
            }
        }
        return output;
    }
    public void infoClose()
    {
        info.SetActive(false);
    }
    public void infoShow()
    {
        info.SetActive(true);
    }
    public void showInfoResults()
    {
        infoResults.SetActive(true);
    }

    public void closeInfoResults1()
    {
        infoResults.SetActive(false);
    }

    public void infoResText(string input)
    {
        infoResultsText.text = input;
    }
    public int sumOfMandates()
    {
        int output = 0;
        for (int i = 0; i < mandates.Length; i++)
        {
            output += mandates[i];
        }
        return output;
    }
    public float satisfied(int party)
    {
        float output = 0;
        for (int i = 0; i < ministeries.Length; i++)
        {
            output += partyParameters[party, i] * results[i, party];
        }
        return output;
    }
    public int sumOfChoice(int party)
    {
        int output = 0;
        for (int i = 0; i < ministeries.Length; i++)
        {
            output += partyParameters[party, i];
        }
        return output;
    }
    public string percentage(float value)
    {
        return (value * 100).ToString("0.00") + "%";
    }
    public void ExplainTheResults()
    {
        /*
        string output = "";
        for (int infoResultParty = 0; infoResultParty < partyNames.Length; infoResultParty++)
        {
        */
        showInfoResults();
        /*
            int sumOfMandatesParam1 = sumOfMandates();
            float satisfiedParam1 = satisfied(infoResultParty);
            int sumOfChoiceParam1 = sumOfChoice(infoResultParty);
            output += "\nThe party " + partyNames[infoResultParty] + " has " + mandates[infoResultParty] + "/" + sumOfMandatesParam1 + "=" + percentage((float)mandates[infoResultParty] / (float)sumOfMandatesParam1) + " of the total mandates and got total value of " + satisfiedParam1 + "/" + sumOfChoiceParam1 + "=" + percentage(satisfiedParam1 / sumOfChoiceParam1) + ".\n";
        }
        */
        infoResText("Click on any party name you see at the results screen to see the explanation of the results for that party.");
    }
}














using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.UI;

using TMPro;
public class ResultsController : MonoBehaviour
{
    public GameObject caseViewRow;
    public GameObject CaseViewRow;
    public Button reuseKey1;
    public bool filterOn1 = true;
    public Image image;
    public GameObject positions;
    public Scrollbar scrollbar;
    public GameObject infoResults;
    public TextMeshProUGUI text;
    void Start()
    {
        filterOn1 = true;
        image.enabled = true;
        InstantiateCaseViewRows();
        reuseKey1.GetComponentInChildren<Text>().text = MainControl.key;
    }

    void Update()
    {
        if (MainControl.infoResultsJump)
        {
            MainControl.infoResultsJump = false;
            ExplainTheResults(MainControl.infoResultParty);
        }
    }
    public void InstantiateCaseViewRows()
    {
        positions.GetComponent<RectTransform>().sizeDelta = new Vector2(positions.GetComponent<RectTransform>().sizeDelta.x, positions.GetComponent<RectTransform>().sizeDelta.y);
        float height = -105f;
        float numberOfInstantiation = -5.79f;
        for (int i = 0; i < MainControl.relevantCases.Length; i++)
        {
            if (MainControl.relevantCases[i])
            {
                numberOfInstantiation++;
                GameObject newCaseViewRow = Instantiate(CaseViewRow, transform.position, transform.rotation, positions.transform);
                newCaseViewRow.transform.localScale = new Vector3(0.39f, 0.39f, 0.39f);
                newCaseViewRow.transform.position = new Vector3(539, height * numberOfInstantiation, 0);
                newCaseViewRow.GetComponent<Results>().caseNumber = i;
            }
        }
    }
    public void Reload()
    {
        SceneManager.LoadScene("PartyChoose");
    }
    public void Link1()
    {
        Application.ExternalEval("prompt(\"Copy the following link to reload this session later:\",\"" + domain(Application.absoluteURL) + "?" + MainControl.key + "\")");
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
    public void InstantiateCaseViewRow1()
    {
        positions.GetComponent<RectTransform>().sizeDelta = new Vector2(positions.GetComponent<RectTransform>().sizeDelta.x, positions.GetComponent<RectTransform>().sizeDelta.y);
        float height = -105f;
        float numberOfInstantiation = -5.79f;
        int j = -1;
        for (int i = 0; i < MainControl.relevantCases.Length; i++)
        {
            if (MainControl.relevantCases[i])
            {
                j++;
                numberOfInstantiation++;
                GameObject newCaseViewRow = Instantiate(caseViewRow, transform.position, transform.rotation, positions.transform);
                newCaseViewRow.transform.position = new Vector3(0, height * j, 0);
                newCaseViewRow.GetComponent<Results>().caseNumber = i;
                SetRectTransform(newCaseViewRow, 0, 0, -115f * ((2 * j) + 1), 225);
            }
        }
    }
    public void SetRectTransform(GameObject go, float left, float right, float posY, float height)
    {
        RectTransform rect = go.GetComponent<RectTransform>();
        rect.offsetMin = new Vector2(left, rect.offsetMin.y);
        rect.offsetMax = new Vector2(right, rect.offsetMax.y);
        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, posY);
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, height);
    }

    public void filter1()
    {
        scrollbar.value = 1;
        filterOn1 = !filterOn1;
        image.enabled = filterOn1;
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        if (filterOn1)
        {
            InstantiateCaseViewRows();
        }
        else
        {
            InstantiateCaseViewRow1();
        }
    }
    public void ExplainTheResults(int infoResultParty)
    {
        infoResults.SetActive(true);
        int sumOfMandatesParam1 = sumOfMandates();
        float satisfiedParam1 = satisfied(infoResultParty);
        int sumOfChoiceParam1 = sumOfChoice(infoResultParty);
        text.text = "למפלגת " + MainControl.partyHebrewName[infoResultParty] + " יש " + ReverseString(MainControl.mandates[infoResultParty] + "/" + sumOfMandatesParam1 + "=" + percentage((float)MainControl.mandates[infoResultParty] / (float)sumOfMandatesParam1)) + " מסך המנדטים שיש לקואוליציה, והיא בפועל קיבלה " + ReverseString(satisfiedParam1 + "/" + sumOfChoiceParam1 + "=" + percentage(satisfiedParam1 / sumOfChoiceParam1)) + " ממה שביקשה .";
    }
    public string ReverseString(string s)
    {
        char[] arr = s.ToCharArray();
        Array.Reverse(arr);
        return new string(arr);
    }
    public int sumOfMandates()
    {
        int output = 0;
        for (int i = 0; i < MainControl.mandates.Length; i++)
        {
            if (MainControl.relevantParties[i])
            {
                output += MainControl.mandates[i];
            }
        }
        return output;
    }
    public float satisfied(int party)
    {
        float output = 0;
        for (int i = 0; i < MainControl.relevantCases.Length; i++)
        {
            if (MainControl.relevantCases[i])
            {
                output += MainControl.partyParameters[party, i] * MainControl.results[i, party];
            }
        }
        return output;
    }
    public int sumOfChoice(int party)
    {
        int output = 0;
        for (int i = 0; i < MainControl.relevantCases.Length; i++)
        {
            if (MainControl.relevantCases[i])
            {
                output += MainControl.partyParameters[party, i];
            }
        }
        return output;
    }
    public string percentage(float value)
    {
        return (value * 100).ToString("0.00") + "%";
    }
    public void closeExp1()
    {
        infoResults.SetActive(false);
    }
}





























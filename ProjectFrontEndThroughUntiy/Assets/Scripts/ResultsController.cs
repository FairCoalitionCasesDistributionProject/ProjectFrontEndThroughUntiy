using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.UI;
public class ResultsController : MonoBehaviour
{
    public GameObject caseViewRow;
    public GameObject CaseViewRow;
    public Button reuseKey1;
    public bool filterOn1 = true;
    public Image image;
    public GameObject positions;
    public Scrollbar scrollbar;
    void Start()
    {
        filterOn1 = true;
        image.enabled = true;
        InstantiateCaseViewRows();
        reuseKey1.GetComponentInChildren<Text>().text = MainControl.key;
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
        int j = -1 ;
        for (int i = 0; i < MainControl.relevantCases.Length; i++)
        {
            if (MainControl.relevantCases[i])
            {
                j++;
                numberOfInstantiation++;
                GameObject newCaseViewRow = Instantiate(caseViewRow, transform.position, transform.rotation, positions.transform);
                newCaseViewRow.transform.position = new Vector3(0, height * j, 0);
                newCaseViewRow.GetComponent<Results>().caseNumber = i;
                SetRectTransform(newCaseViewRow, 0, 0, (-1) * (115f * ((2 * j) + 1)), 225);
            }
        }
    }
    // Function gets a GameObject and float parameters left, right, posY and height and sets the RectTransform of the GameObject to the given parameters.
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
}

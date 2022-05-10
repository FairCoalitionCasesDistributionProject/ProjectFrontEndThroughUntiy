using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.UI;
public class ResultsController : MonoBehaviour
{
    public GameObject CaseViewRow;
    public InputField InputKey1;
    void Start()
    {
        InstantiateCaseViewRows();
        Application.ExternalEval(Application.absoluteURL + "?" + MainControl.key);
        InputKey1.text = MainControl.key;
        InputKey1.readOnly = true;
    }
    public void InstantiateCaseViewRows()
    {
        float height = -105f;
        float numberOfInstantiation = -5.79f;
        for (int i = 0; i < MainControl.relevantCases.Length; i++)
        {
            if (MainControl.relevantCases[i])
            {
                numberOfInstantiation++;
                GameObject newCaseViewRow = Instantiate(CaseViewRow, transform.position, transform.rotation, transform);
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
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class ResultsController : MonoBehaviour
{
    public GameObject CaseViewRow;
    void Start()
    {
        InstantiateCaseViewRows();
    }
    public void InstantiateCaseViewRows()
    {
        float height = -105f;
        int numberOfInstantiation = -5;
        for (int i = 0; i < MainControl.relevantCases.Length; i++)
        {
            if (MainControl.relevantCases[i])
            {
                numberOfInstantiation++;
                GameObject newCaseViewRow = Instantiate(CaseViewRow,transform.position,transform.rotation,transform);
                newCaseViewRow.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                newCaseViewRow.transform.position = new Vector3(681, height*numberOfInstantiation ,0);
                newCaseViewRow.GetComponent<Results>().caseNumber=i;
            }
        }
    }
}
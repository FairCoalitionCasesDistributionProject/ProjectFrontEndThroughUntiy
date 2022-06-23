using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResultLine1 : MonoBehaviour
{
    public int index;
    public Text caseName;
    public GameObject result;
    public GameObject resultsCell;
    void Start()
    {
        string name = (GlobalPartyChoose.ministeries[index] != null && GlobalPartyChoose.ministeries[index] != "" && !allSpaces(GlobalPartyChoose.ministeries[index])) ? GlobalPartyChoose.ministeries[index] : "Case" + index;
        caseName.text = name;
        float width = 285f;
        float numberOfInstantiation = (-5.79f);
        result.GetComponent<RectTransform>().sizeDelta = new Vector2((GlobalPartyChoose.partyNames.Length > 2) ? ((GlobalPartyChoose.partyNames.Length - 1) * width) : result.GetComponent<RectTransform>().sizeDelta.x, result.GetComponent<RectTransform>().sizeDelta.y);
        for (int i = 0; i < GlobalPartyChoose.partyNames.Length; i++)
        {
            numberOfInstantiation++;
            GameObject newPartyChooseLine = Instantiate(resultsCell, transform.position, transform.rotation, result.transform);
            newPartyChooseLine.transform.position = new Vector3((i * width) - 337.5f, (index * -147f) + 58f - (1.8946f * index), 0);
            (newPartyChooseLine.GetComponent<ResultsCell>().cIndex, newPartyChooseLine.GetComponent<ResultsCell>().pIndex) = (index, i);
            SetRectTransform(newPartyChooseLine, 0, 0);
        }
    }
    public bool allSpaces(string str)
    {
        return str.Replace(" ", "").Length == 0;
    }
    public void SetRectTransform(GameObject go, float bottom, float top)
    {
        RectTransform rect = go.GetComponent<RectTransform>();
        rect.offsetMin = new Vector2(rect.offsetMin.x, bottom);
        rect.offsetMax = new Vector2(rect.offsetMax.x, top);
    }
}








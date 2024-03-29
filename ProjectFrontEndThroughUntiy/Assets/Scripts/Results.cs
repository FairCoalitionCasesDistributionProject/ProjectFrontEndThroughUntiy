using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class Results : MonoBehaviour
{
    public (int, int) param = (0, 0);
    public int mode1;
    public int caseNumber;
    public Text number;
    public Text number1;
    public Slider slider;
    public TextMeshProUGUI name;
    public Image party;
    public Image party1;
    public Sprite likud, haavoda, hareshimaHamshutefet, hareshimaHaaravitHameshutefet, hazionutHadatit, israelBeitenu, kaholLavan, meretz, shas, tikvaHadasha, yahadutHatora, yemina, yeshAtid;
    public GameObject partyResultOnCase;
    public GameObject positions;
    public bool leftImageClick1 = false;
    public bool rightImageClick = false;
    void Start()
    {
        if (mode1 == 0)
        {
            partyResultOnCase = null;
            positions = null;
            (int, int) tuple = partySplitter(caseNumber);
            bool equal = isEqual(tuple);
            int theBigger = bigger(tuple);
            int theSmaller = smaller(tuple);
            slider.value = (equal) ? MainControl.results01[caseNumber, tuple.Item1] : ((1 / (MainControl.results01[caseNumber, theBigger] + MainControl.results01[caseNumber, theSmaller])) * MainControl.results01[caseNumber, theBigger]);
            number.text = "" + percentage(slider.value);
            party.sprite = partyImages(tuple.Item1);
            party.SetNativeSize();
            if (equal || (percentage(1 - slider.value) == "0.00%"))
            {
                party1.enabled = false;
                number1.enabled = false;
            }
            else
            {
                party1.sprite = partyImages(tuple.Item2);
                party1.SetNativeSize();
                number1.text = "" + percentage(1 - slider.value);
            }
            param = (theSmaller, theBigger);
        }
        else
        {
            int[] parties = partiesInCase();
            float width = 285f;
            positions.GetComponent<RectTransform>().sizeDelta = new Vector2((parties.Length > 2) ? ((parties.Length - 2) * width) : positions.GetComponent<RectTransform>().sizeDelta.x, positions.GetComponent<RectTransform>().sizeDelta.y);
            for (int i = 0; i < parties.Length; i++)
            {
                GameObject partyLine = Instantiate(partyResultOnCase, transform.position, transform.rotation, positions.transform);
                partyLine.transform.position = new Vector3((i * width) - 337.5f, (caseNumber * -147f) + 58f - (1.8946f * caseNumber), 0);
                partyLine.GetComponent<ResultsCell1>().cIndex = caseNumber;
                partyLine.GetComponent<ResultsCell1>().pIndex = parties[i];
                partyLine.GetComponent<ResultsCell1>().sprite = partyImages(parties[i]);
                SetRectTransform(partyLine, 0, -5f, 142f * ((2 * i) + 1), 285f);
            }
        }
        name.text = MainControl.casesNameTranslation[caseNumber];
    }
    void Update()
    {
        if (leftImageClick1)
        {
            leftImageClick1 = false;
            MainControl.infoResultParty = param.Item2;
            MainControl.infoResultsJump = true;
        }
        if (rightImageClick)
        {
            rightImageClick = false;
            MainControl.infoResultParty = param.Item1;
            MainControl.infoResultsJump = true;
        }
    }
    public (int, int) partySplitter(int caseNumber)
    {
        int first = 0;
        int second = 0;
        float firstValue = 0;
        float secondValue = 0;
        for (int i = 0; i < MainControl.results01.GetLength(1); i++)
        {
            if (MainControl.results01[caseNumber, i] > firstValue)
            {
                secondValue = firstValue;
                firstValue = MainControl.results01[caseNumber, i];
                second = first;
                first = i;
            }
            else if (MainControl.results01[caseNumber, i] > secondValue)
            {
                secondValue = MainControl.results01[caseNumber, i];
                second = i;
            }
        }
        return (first, second);
    }
    public int bigger((int, int) tuple)
    {
        return (MainControl.results01[caseNumber, tuple.Item1] > MainControl.results01[caseNumber, tuple.Item2]) ? tuple.Item1 : tuple.Item2;
    }
    public int smaller((int, int) tuple)
    {
        return (MainControl.results01[caseNumber, tuple.Item1] < MainControl.results01[caseNumber, tuple.Item2]) ? tuple.Item1 : tuple.Item2;
    }
    public bool isEqual((int, int) tuple)
    {
        return tuple.Item1 == tuple.Item2;
    }
    public Sprite partyImages(int partyNumber)
    {
        switch (partyNumber)
        {
            case 0:
                return likud;
            case 1:
                return haavoda;
            case 2:
                return hareshimaHamshutefet;
            case 3:
                return hareshimaHaaravitHameshutefet;
            case 4:
                return hazionutHadatit;
            case 5:
                return israelBeitenu;
            case 6:
                return kaholLavan;
            case 7:
                return meretz;
            case 8:
                return shas;
            case 9:
                return tikvaHadasha;
            case 10:
                return yahadutHatora;
            case 11:
                return yemina;
            case 12:
                return yeshAtid;
            default:
                return likud;
        }
    }
    public string percentage(float value)
    {
        return (value * 100).ToString("0.00") + "%";
    }
    public int[] partiesInCase()
    {
        ArrayList parties = new ArrayList();
        for (int i = 0; i < MainControl.results.GetLength(1); i++)
        {
            if (MainControl.results[caseNumber, i] > 0)
            {
                parties.Add(i);
            }
        }
        parties.Sort();
        for (int i = 0; i < parties.Count - 1; i++)
        {
            if (parties[i] == parties[i + 1])
            {
                parties.RemoveAt(i);
                i--;
            }
        }
        return (int[])parties.ToArray(typeof(int));
    }
    public void SetRectTransform(GameObject go, float bottom, float top, float posX, float width)
    {
        RectTransform rect = go.GetComponent<RectTransform>();
        rect.offsetMin = new Vector2(rect.offsetMin.x, bottom);
        rect.offsetMax = new Vector2(rect.offsetMax.x, top);
        rect.position = new Vector3(posX, rect.position.y, rect.position.z);
        rect.sizeDelta = new Vector2(width, rect.sizeDelta.y);
    }
    public void leftImageClicked1()
    {
        leftImageClick1 = true;
    }
    public void rightImageClicked()
    {
        rightImageClick = true;
    }
}













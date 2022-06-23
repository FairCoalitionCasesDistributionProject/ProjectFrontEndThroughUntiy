using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResultsCell1 : MonoBehaviour
{
    public int cIndex;
    public int pIndex;
    public Sprite sprite;
    public Image party;
    public Text value;
    void Start()
    {
        value.text = percentage(MainControl.results[cIndex, pIndex]);
        party.sprite = sprite;
        party.SetNativeSize();
        GameObject partyObject = party.gameObject;
        SetPosition(partyObject);

    }
    public bool allSpaces(string str)
    {
        return str.Replace(" ", "").Length == 0 || string.IsNullOrWhiteSpace(str.Replace(" ", ""));
    }
    public string percentage(float value)
    {
        return (value * 100).ToString("0.00") + "%";
    }
    public void SetPosition(GameObject obj)
    {
        RectTransform rectTransform = obj.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0.5f, 1);
        rectTransform.anchorMax = new Vector2(0.5f, 1);
        rectTransform.pivot = new Vector2(0.5f, 1);
        rectTransform.anchoredPosition = new Vector2(0, 0);
    }


}


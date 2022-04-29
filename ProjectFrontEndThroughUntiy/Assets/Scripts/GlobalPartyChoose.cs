using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
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
    void Start()
    {
        partyScreen.SetActive(false);
        settings0.SetActive(true);
        settings1.SetActive(false);
        alert.SetActive(false);
        timeConfirm = 0;
        back.interactable = false;
    }
    void Update()
    {
        if (timeConfirm == 3)
        {
            int sum = 0;
            foreach (Transform child in positions.transform)
            {
                GameObject now = child.gameObject;
                now.GetComponent<PartyChooseLine>().partyPreference.onClick.AddListener(delegate
                {
                    partyScreen.SetActive(true);
                    foreach (Transform child in preferences.transform)
                    {
                        Destroy(child.gameObject);
                    }
                    float height = 41f;
                    float numberOfInstantiation = -5.79f;
                    preferences.GetComponent<RectTransform>().sizeDelta = new Vector2(preferences.GetComponent<RectTransform>().sizeDelta.x, (ministeries.Length * height) - 447.5f);
                    for (int i = 0; i < ministeries.Length; i++)
                    {
                        numberOfInstantiation++;
                        GameObject newPartyChooseLine = Instantiate(party, transform.position, transform.rotation, preferences.transform);
                        //*newPartyChooseLine.transform.localScale = new Vector3(0.39f, 0.39f, 0.39f);
                        newPartyChooseLine.transform.position = new Vector3(480, ((-1) * height * i) + 447.5f, 0);
                        newPartyChooseLine.GetComponent<PartySlider>().party = now.GetComponent<PartyChooseLine>().index;
                        newPartyChooseLine.GetComponent<PartySlider>().index = i;
                    }
                });
                sum += (int)now.GetComponent<PartyChooseLine>().partyMandatesSlider.value;
            }
            GlobalPartyChoose.summary = sum;
        }
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
                            //*newPartyChooseLine.transform.localScale = new Vector3(0.39f, 0.39f, 0.39f);
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
                            //*newPartyChooseLine.transform.localScale = new Vector3(0.39f, 0.39f, 0.39f);
                            newPartyChooseLine.transform.position = new Vector3(480, (height * i) + 447.5f, 0);
                            newPartyChooseLine.GetComponent<PartyChooseLine>().index = i;
                            newPartyChooseLine.GetComponent<PartyChooseLine>().amountOfMandates = amountOfMandateInt;
                        }
                    }
                }
                break;
            case 4:
                foreach (Transform child in positions.transform)
                {
                    Destroy(child.gameObject);
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
        partyScreen.SetActive(false);
    }
}
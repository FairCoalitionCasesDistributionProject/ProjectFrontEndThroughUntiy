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
    public Button confirm;
    public GameObject alert;
    public Text alertText;
    public GameObject positions;
    public GameObject settings;
    public GameObject partyChooseLine;
    public int timeConfirm;

    public static string[] partyNames;
    public static int[] mandates;
    public static int[,] partyParameters;


    public static int summary;
    void Start()
    {
        alert.SetActive(false);
        timeConfirm = 0;
    }
    void Update()
    {
        int sum = 0;
        foreach (Transform child in positions.transform)
        {
            GameObject now = child.gameObject;
            sum += (int)now.GetComponent<PartyChooseLine>().partyMandatesSlider.value;
        }
        GlobalPartyChoose.summary = sum;
    }




    public void confirmPressed()
    {
        timeConfirm++;
        if (timeConfirm == 1)
        {
            if ((numberOfParties.text == "" || !int.TryParse(numberOfParties.text, out int numberOfPartiesInt)) ||
                (amountOfMandate.text == "" || !int.TryParse(amountOfMandate.text, out int amountOfMandateInt)))
            {
                alertShow(true, "Please enter a number .", 1.5f);
            }
            else
            {

                partyNames = new string[numberOfPartiesInt];
                mandates = new int[numberOfPartiesInt];

                confirm.GetComponentInChildren<Text>().text = "Calculate";
                settings.SetActive(false);
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
        else
        {
            foreach (Transform child in positions.transform)
            {
                Destroy(child.gameObject);
            }
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
}

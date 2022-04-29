using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PartyChooseLine : MonoBehaviour
{
    public int amountOfMandates;
    public int index;
    public TMP_Text partyDefaultName;
    public TMP_Text partyName;
    public Text partyMandates;
    public Slider partyMandatesSlider;
    public Button partyPreference;
    public GameObject preferences;
    public GameObject partyScreen;
    public GameObject party;
    void Start()
    {
        partyScreen.SetActive(false);
        partyMandatesSlider.minValue = 0;
        partyMandatesSlider.maxValue = amountOfMandates;
        partyMandatesSlider.value = GlobalPartyChoose.mandates[index];
        partyName.text = (GlobalPartyChoose.partyNames[index] == null) ? "Party" + index : GlobalPartyChoose.partyNames[index];
        partyDefaultName.text = (GlobalPartyChoose.partyNames[index] == null) ? "Party" + index : GlobalPartyChoose.partyNames[index];
    }
    void Update()
    {
        partyMandatesSlider.onValueChanged.AddListener(delegate { sliderMoves(); });
        GlobalPartyChoose.partyNames[index] = partyName.text;
    }
    public void sliderClicked()
    {
        int res = (amountOfMandates - GlobalPartyChoose.summary);
        partyMandatesSlider.maxValue = (GlobalPartyChoose.summary == 0) ? amountOfMandates : partyMandatesSlider.value + res;
        if (partyMandatesSlider.maxValue < 0)
        {
            partyMandatesSlider.maxValue = 0;
        }
    }
    public async void sliderMoves()
    {
        partyMandates.text = " " + partyMandatesSlider.value.ToString();
        GlobalPartyChoose.mandates[index] = (int)partyMandatesSlider.value;
    }
    public void setHeight(GameObject gameObject, float x)
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().sizeDelta.x, x);
    }
    public void preferencesPressed()
    {
        setHeight(this.gameObject, 600);
        partyScreen.SetActive(true);
        setHeight(partyScreen, 600);
        foreach (Transform child in preferences.transform)
        {
            Destroy(child.gameObject);
        }
        float height = 41f;
        float numberOfInstantiation = -5.79f;
        preferences.GetComponent<RectTransform>().sizeDelta = new Vector2(preferences.GetComponent<RectTransform>().sizeDelta.x, (GlobalPartyChoose.ministeries.Length * height) - 447.5f);
        for (int i = 0; i < GlobalPartyChoose.ministeries.Length; i++)
        {
            numberOfInstantiation++;
            GameObject newPartyChooseLine = Instantiate(party, transform.position, transform.rotation, preferences.transform);
            //*newPartyChooseLine.transform.localScale = new Vector3(0.39f, 0.39f, 0.39f);
            newPartyChooseLine.transform.position = new Vector3(480, ((-1) * height * i) + 447.5f, 0);
            newPartyChooseLine.GetComponent<PartySlider>().party = index;
            newPartyChooseLine.GetComponent<PartySlider>().index = i;
        }
    }
}























































































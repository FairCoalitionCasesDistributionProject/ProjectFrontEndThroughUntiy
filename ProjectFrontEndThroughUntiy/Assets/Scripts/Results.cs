using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class Results : MonoBehaviour
{
    public int caseNumber;
    public Text number;
    public Text number1;
    public Slider slider;
    public TextMeshProUGUI name;
    public Image party;
    public Image party1;
    void Start()
    {
        //*TODO.
    }
    /*(int, int) tuple = partySplitter(caseNumber);
    slider.value = (isEqual(tuple)) ? MainControl.results[caseNumber, tuple.Item1] : MainControl.results[caseNumber, bigger(tuple)]; number.text = " " + slider.value;
    name.text = MainControl.casesNameTranslation[caseNumber];
}
public (int, int) partySplitter(int caseNumber)
{
    int counter = 0;
    int first = 0;
    int second = 0;
    for (int i = 0; i < MainControl.results.GetLength(0); i++)
    {
        if (MainControl.results[caseNumber, i] > 0)
        {
            if (counter == 0)
            {
                first = i;
                second = i;
                counter++;
            }
            else
            {
                second = i;
            }
        }
    }
    return (first, second);
}
public int bigger((int, int) tuple)
{
    return (MainControl.results[caseNumber, tuple.Item1] > MainControl.results[caseNumber, tuple.Item2]) ? tuple.Item1 : tuple.Item2;
}
public bool isEqual((int, int) tuple)
{
    return tuple.Item1 == tuple.Item2;
}
*/
}



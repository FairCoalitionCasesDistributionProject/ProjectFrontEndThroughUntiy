using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoControl : MonoBehaviour
{
    public GameObject infoMain;
    public GameObject infoHow;
    public GameObject infoAlgo;
    public GameObject infoTech;
    public GameObject infoAbout;

    void Start()
    {
        infoMain.SetActive(true);
        infoHow.SetActive(false);
        infoAlgo.SetActive(false);
        infoTech.SetActive(false);
        infoAbout.SetActive(false);
    }
    public void Button1()
    {
        infoMain.SetActive(true);
        infoHow.SetActive(false);
        infoAlgo.SetActive(false);
        infoTech.SetActive(false);
        infoAbout.SetActive(false);
    }
    public void Button2()
    {
        infoMain.SetActive(false);
        infoHow.SetActive(true);
        infoAlgo.SetActive(false);
        infoTech.SetActive(false);
        infoAbout.SetActive(false);
    }
    public void Button3()
    {
        infoMain.SetActive(false);
        infoHow.SetActive(false);
        infoAlgo.SetActive(true);
        infoTech.SetActive(false);
        infoAbout.SetActive(false);
    }
    public void Button4()
    {
        infoMain.SetActive(false);
        infoHow.SetActive(false);
        infoAlgo.SetActive(false);
        infoTech.SetActive(true);
        infoAbout.SetActive(false);
    }
    public void Button5()
    {
        infoMain.SetActive(false);
        infoHow.SetActive(false);
        infoAlgo.SetActive(false);
        infoTech.SetActive(false);
        infoAbout.SetActive(true);
    }

}

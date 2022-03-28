using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
public class Welcome : MonoBehaviour
{








    public GameObject loading;
    public bool gotAnswer = false;
    void Start()
    {
        string URL = "http://faircol.herokuapp.com/api/";
        string json = "{WakeUpAndBeReady.}";
        StartCoroutine(Upload(URL, json));
    }

    void Update()
    {
        if (gotAnswer)
        {
            loading.SetActive(false);
        }
    }


    IEnumerator Upload(string URL, string json)
    {
        var uwr = new UnityWebRequest(URL, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");
        yield return uwr.SendWebRequest();
        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("OK! " + uwr.downloadHandler.text);
        }

        gotAnswer = true;
    }



    public void LoadIsraelMode1()
    {
        if (gotAnswer)
        {
            SceneManager.LoadScene("Scene1");
        }
    }
}

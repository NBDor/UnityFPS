using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiStatusManager : MonoBehaviour
{
    public TMP_Text status;
    public static UiStatusManager instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void UpdateStatusText(string text)
    {
        StartCoroutine(refreshText(text));
    }

    IEnumerator refreshText(string text)
    {
        status.text = text;
        yield return new WaitForSeconds(2.5f);
        status.text = "";
    }
}

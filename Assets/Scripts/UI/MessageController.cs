using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MessageController : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;

    void Start()
    {
        // Debug.Log(textMeshPro);
        // textMeshPro.text = "test";
    }

    public void ShowMessage(String message)
    {
        textMeshPro.text += message;
    }
}

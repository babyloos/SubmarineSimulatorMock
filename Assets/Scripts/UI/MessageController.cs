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
    }

    public void ShowMessage(ROLE role, String message)
    {
        var text = role.GetStringValue() + ": " + message + "\n";
        textMeshPro.text += text;
    }

}

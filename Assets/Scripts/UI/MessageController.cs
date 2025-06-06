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

    public void ShowMessage(ROLE role, List<String> messages)
    {
        var resultMessage = "";
        foreach (var message in messages) {
            var text = role.GetStringValue() + ": " + message;
            resultMessage += text + "\n";
        }
        textMeshPro.text += resultMessage;
    }

}

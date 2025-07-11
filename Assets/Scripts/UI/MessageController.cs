using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class MessageController : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    private ScrollRect scrollRect;
    private LocalizationManager locale;

    void Start()
    {
        locale = LocalizationManager.Instance;
        if (scrollRect == null)
        {
            scrollRect = GetComponentInParent<ScrollRect>();
        }
    }

    public void ShowMessage(ROLE role, List<String> messages)
    {
        var resultMessage = "";
        foreach (var message in messages)
        {
            var text = this.locale.GetLocalizedText(role.GetStringValue()) + ": " + message;
            resultMessage += text + "\n";
        }
        textMeshPro.text += resultMessage;
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }

}

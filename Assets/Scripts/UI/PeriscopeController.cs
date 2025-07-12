using UnityEngine;
using UnityEngine.EventSystems;

public class PeriscopeContoller : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject perisocpePanel;

    public void OnClick()
    {
        if (mainPanel.activeSelf)
        {
            mainPanel.SetActive(false);
            perisocpePanel.SetActive(true);
        }
        else
        {
            perisocpePanel.SetActive(false);
            mainPanel.SetActive(true);
        }
    }
}
using UnityEngine;
using UnityEngine.EventSystems;

public class PeriscopeContoller : MonoBehaviour, IPointerClickHandler
{
    public GameObject mainPanel;
    public GameObject perisocpePanel;

    public void OnPointerClick(PointerEventData eventData)
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

    public void Start()
    {

    }
}
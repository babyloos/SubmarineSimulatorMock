using UnityEngine;
using UnityEngine.EventSystems;

public class PeriscopeContoller : MonoBehaviour, IPointerClickHandler
{
    public GameObject mainPanel;
    public GameObject perisocpePanel;

    public void OnPointerClick(PointerEventData eventData)
    {
        mainPanel.SetActive(false);
    }

    public void Start()
    {

    }
}
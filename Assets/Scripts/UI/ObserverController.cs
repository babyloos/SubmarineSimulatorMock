using UnityEngine;
using UnityEngine.EventSystems;

public class ObserverController : MonoBehaviour, IPointerClickHandler
{
    public MessageController messageController;

    public void OnPointerClick(PointerEventData pointerData)
    {
        messageController.ShowMessage(ROLE.Observer, "敵が見えました");
    }
}

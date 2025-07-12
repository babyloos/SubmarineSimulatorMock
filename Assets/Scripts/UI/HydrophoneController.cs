using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class hydrophoneController : MonoBehaviour, IPointerClickHandler
{
    public MessageController messageController;
    private LocalizationManager locale;

    // Start is called before the first frame update
    void Start()
    {
        this.locale = LocalizationManager.Instance;
    }

    public void OnPointerClick(PointerEventData pointerData)
    {
        Debug.Log("click hydrophone");
        var foundShips = this.findships();
        var messages = this.createMessage(foundShips);
        messageController.ShowMessage(ROLE.Hydrophone, messages);
    }

    private List<FoundShipInfo> findships()
    {
        // TODO: 実実する
        return new List<FoundShipInfo>()
        {
            new FoundShipInfo(SHIP_TYPE.MERCHANT, 100f, 200, 10, 200),
        };
    }

    private List<String> createMessage(List<FoundShipInfo> foundShipInfos)
    {
        var messages = new List<String>();
        if (foundShipInfos.Count == 0)
        {
            messages.Add(this.locale.GetLocalizedText("RES_NoShips"));
        }
        else
        {
            messages.Add(this.locale.GetLocalizedText("RES_ObserverDiscover").Replace("xxx", foundShipInfos.Count.ToString()));
            var count = 0;
            foreach (var info in foundShipInfos)
            {
                count += 1;
                var shipName = this.locale.GetLocalizedText("RES_EnemyNum").Replace("xxx", count.ToString());
                var shipMessaage = shipName + " " +
                                   this.locale.GetLocalizedText("RES_Direction") + info.Direction + ", " +
                                   this.locale.GetLocalizedText("RES_Range") + info.Range + "m, " +
                                   this.locale.GetLocalizedText("RES_Course") + info.Course + ", " +
                                   this.locale.GetLocalizedText("RES_ShipSpeed") + info.Speed + "Kn!";
                messages.Add(shipMessaage);
            }
        }

        return messages;
    }
}

internal class FoundShipInfo
{
    public FoundShipInfo(SHIP_TYPE shipType, float direction, float course, int speed, int range)
    {
        this.ShipType = shipType;
        this.Direction = direction;
        this.Course = course;
        this.Speed = speed;
        this.Range = range;
    }

    internal SHIP_TYPE ShipType { get; set; }
    internal float Direction { get; set; }
    internal float Course { get; set; }
    internal int Speed { get; set; }
    internal int Range { get; set; }
}
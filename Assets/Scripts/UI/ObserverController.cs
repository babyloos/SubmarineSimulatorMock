using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObserverController : MonoBehaviour, IPointerClickHandler
{
    public MessageController messageController;
    private LocalizationManager locale;

    public void Start()
    {
        this.locale = LocalizationManager.Instance;
    }

    public void OnPointerClick(PointerEventData pointerData)
    {
        var observationShipInfos = this.observation();
        var messages = this.createMessage(observationShipInfos);
        messageController.ShowMessage(ROLE.Observer, messages);
    }

    private List<String> createMessage(List<ObservationShipInfo> observationShipInfos)
    {
        var messages = new List<String>();
        if (observationShipInfos.Count == 0)
        {
            messages.Add(this.locale.GetLocalizedText("RES_NoShips"));
        }
        else
        {
            messages.Add(this.locale.GetLocalizedText("RES_ObserverDiscover").Replace("xxx", observationShipInfos.Count.ToString()));
            var count = 0;
            foreach (var info in observationShipInfos)
            {
                count += 1;
                var shipName = this.locale.GetLocalizedText("RES_EnemyNum").Replace("xxx", count.ToString());
                var shipMessaage = shipName + " " +
                                   this.locale.GetLocalizedText("RES_Direction") + info.Direction + ", " +
                                   this.locale.GetLocalizedText("RES_Range") + info.Range + ", " +
                                   this.locale.GetLocalizedText("RES_Course") + info.Course + ", " +
                                   this.locale.GetLocalizedText("RES_ShipSpeed") + info.Speed;
                messages.Add(shipMessaage);
            }
        }

        return messages;
    }

    private List<ObservationShipInfo> observation()
    {
        return new List<ObservationShipInfo>
        {
            new ObservationShipInfo(SHIP_TYPE.MERCHANT, 300f, 300f, 100, 100)
        };
    }

    internal class ObservationShipInfo
    {
        public ObservationShipInfo(SHIP_TYPE shipType, float direction, float course, int speed, int range)
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
}

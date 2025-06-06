using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObserverController : MonoBehaviour, IPointerClickHandler
{
    public MessageController messageController;

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
            messages.Add("船影無し！");
        }
        else
        {
            messages.Add("船影を" + observationShipInfos.Count + "隻確認！");
            var count = 0;
            foreach (var info in observationShipInfos)
            {
                count += 1;
                var shipName = "敵船" + count;
                var shipMessaage = shipName + " " + "方位" + info.Direction + ", " + "距離" + info.Range + ", " + "針路" + info.Course + ", " + "船速" + info.Speed;
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

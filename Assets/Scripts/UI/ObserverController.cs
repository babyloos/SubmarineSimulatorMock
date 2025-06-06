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
        // var message = this.createMessage(observation);
        messageController.ShowMessage(ROLE.Observer, "敵が見えました");
    }

    private void createMessage(Func<List<ObservationShipInfo>> observation)
    {
        throw new NotImplementedException();
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

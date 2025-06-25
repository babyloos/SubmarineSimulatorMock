using System;
using System.Collections.Generic;
using System.Linq;
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
        // TODO: 潜航中は呼べない

        var ships = new List<GameObject>();
        var cargoShips = GameObject.FindGameObjectsWithTag("CargoShip").ToList();
        var destroyers = GameObject.FindGameObjectsWithTag("Destroyer").ToList();
        ships = cargoShips;
        ships.AddRange(destroyers);

        var player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log("自分の位置: " + player.transform.position);

        var foundShips = new List<GameObject>();

        // 目視可能範囲は半径7マイル(12.964KM)
        var searchRange = 12.964 * 1000;
        foreach (GameObject ship in ships)
        {
            float distance = Vector3.Distance(player.transform.position, ship.transform.position);
            if (distance < searchRange)
            {
                Debug.Log(distance);
                foundShips.Add(ship);
            }
        }

        Debug.Log(foundShips);

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

using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObserverController : MonoBehaviour, IPointerClickHandler
{
    public MessageController messageController;
    private LocalizationManager locale;
    private GameObject player;

    public void Start()
    {
        this.locale = LocalizationManager.Instance;
        this.player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnPointerClick(PointerEventData pointerData)
    {
        var observationShipInfos = this.observation();
        var messages = this.createMessage(observationShipInfos);
        messageController.ShowMessage(ROLE.Observer, messages);
    }

    private List<String> createMessage(List<FoundShipInfo> observationShipInfos)
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
                                   this.locale.GetLocalizedText("RES_Range") + info.Range + "m, " +
                                   this.locale.GetLocalizedText("RES_Course") + info.Course + ", " +
                                   this.locale.GetLocalizedText("RES_ShipSpeed") + info.Speed + "Kn!";
                messages.Add(shipMessaage);
            }
        }

        return messages;
    }

    private List<FoundShipInfo> observation()
    {
        if (this.player.GetComponent<UBoatController>().DepthState() != SURFACE_STATUS.SURFACE)
        {
            throw new Exception("潜航中に観測を行った");
        }

        var ships = new List<GameObject>();
        var cargoShips = GameObject.FindGameObjectsWithTag("CargoShip").ToList();
        var destroyers = GameObject.FindGameObjectsWithTag("Destroyer").ToList();
        ships = cargoShips;
        ships.AddRange(destroyers);

        var foundShips = new List<GameObject>();

        // 目視可能範囲は半径7マイル(12.964KM)
        var searchRange = 12.964 * 1000;
        foreach (GameObject ship in ships)
        {
            float distance = Vector3.Distance(this.player.transform.position, ship.transform.position);
            if (distance < searchRange)
            {
                // Debug.Log(distance);
                foundShips.Add(ship);
            }
        }

        var shipInfos = new List<FoundShipInfo>();
        foreach (var foundShip in foundShips)
        {
            var shipType = foundShip.tag == "CargoShip" ? SHIP_TYPE.MERCHANT : SHIP_TYPE.DESTROYER;
            var direction = this.calcDirection(this.player, foundShip);
            var cource = Mathf.RoundToInt(foundShip.transform.eulerAngles.y);
            var speed = Mathf.RoundToInt(foundShip.GetComponent<Rigidbody>().velocity.magnitude);
            var distance = Mathf.RoundToInt(Vector3.Distance(this.player.transform.position, foundShip.transform.position));

            var shipInfo = new FoundShipInfo(shipType, direction, cource, speed, distance);
            shipInfos.Add(shipInfo);
        }

        return shipInfos;
    }


    // 自分から見た絶対方位を360度で返す
    private float calcDirection(GameObject mine, GameObject target)
    {
        var toTarget = target.transform.position - mine.transform.position;
        var direction = Mathf.RoundToInt(Mathf.Atan2(toTarget.x, toTarget.z) * Mathf.Rad2Deg) + 180;
        if (direction < 0) direction += 360;
        return direction;
    }
}

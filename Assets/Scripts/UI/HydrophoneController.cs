using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class hydrophoneController : MonoBehaviour
{
    public MessageController messageController;
    private LocalizationManager locale;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        this.locale = LocalizationManager.Instance;
        this.player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        var button = this.gameObject.GetComponent<Button>();
        if (button.interactable && !this.CanButtonClick())
        {
            button.interactable = false;
        }

        if (!button.interactable && this.CanButtonClick())
        {
            button.interactable = true;
        }
    }

    private bool CanButtonClick()
    {
        return player.GetComponent<UBoatController>().DepthState() != SURFACE_STATUS.SURFACE;
    }

    public void OnClick()
    {
        var foundShips = this.findships();
        var messages = this.createMessage(foundShips);
        messageController.ShowMessage(ROLE.Hydrophone, messages);
    }
    private List<FoundShipInfo> findships()
    {
        // ※浮上中はボタン押下できず呼ばれないはず
        if (player.GetComponent<UBoatController>().DepthState() == SURFACE_STATUS.SURFACE)
        {
            throw new Exception("浮上中に聴音を行った");
        }

        var ships = new List<GameObject>();
        var cargoShips = GameObject.FindGameObjectsWithTag("CargoShip").ToList();
        var destroyers = GameObject.FindGameObjectsWithTag("Destroyer").ToList();
        ships = cargoShips;
        ships.AddRange(destroyers);

        var foundShips = new List<GameObject>();

        // 聴音可能範囲は半径54マイル(100KM)
        var searchRange = 100 * 1000;
        foreach (GameObject ship in ships)
        {
            float distance = Vector3.Distance(player.transform.position, ship.transform.position);
            if (distance < searchRange)
            {
                foundShips.Add(ship);
            }
        }

        var shipInfos = new List<FoundShipInfo>();
        foreach (var foundShip in foundShips)
        {
            var shipType = foundShip.tag == "CargoShip" ? SHIP_TYPE.MERCHANT : SHIP_TYPE.DESTROYER;
            var direction = this.calcDirection(player, foundShip);
            var cource = Mathf.RoundToInt(foundShip.transform.eulerAngles.y);
            var speed = Mathf.RoundToInt(foundShip.GetComponent<Rigidbody>().velocity.magnitude);
            var distance = Mathf.RoundToInt(Vector3.Distance(player.transform.position, foundShip.transform.position));

            var shipInfo = new FoundShipInfo(shipType, direction, cource, speed, distance);
            shipInfos.Add(shipInfo);
        }

        return shipInfos;
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
            messages.Add(this.locale.GetLocalizedText("RES_HydrophoneDiscover").Replace("xxx", foundShipInfos.Count.ToString()));
            var count = 0;
            foreach (var info in foundShipInfos)
            {
                count += 1;
                var shipName = this.locale.GetLocalizedText("RES_EnemyNum").Replace("xxx", count.ToString());
                var loudMessage = this.locale.GetLocalizedText(this.SoundLoudness(info.Range).GetStringValue());
                var shipMessaage = shipName + " " +
                                   this.locale.GetLocalizedText("RES_Direction") + info.Direction + ", " +
                                   loudMessage;
                messages.Add(shipMessaage);
            }
        }

        return messages;
    }

    // TODO: 共通化する
    // 自分から見た絶対方位を360度で返す
    private float calcDirection(GameObject mine, GameObject target)
    {
        var toTarget = target.transform.position - mine.transform.position;
        var direction = Mathf.RoundToInt(Mathf.Atan2(toTarget.x, toTarget.z) * Mathf.Rad2Deg) + 180;
        if (direction < 0) direction += 360;
        return direction;
    }

    private SOUND_LOUDNESS SoundLoudness(int range)
    {
        if (range >= 50000)
        {
            return SOUND_LOUDNESS.LOW;
        }
        else if (range >= 30000)
        {
            return SOUND_LOUDNESS.MIDDLE;
        }
        else if (range >= 20000)
        {
            return SOUND_LOUDNESS.SOMEWHAT_LARGE;
        }
        else
        {
            return SOUND_LOUDNESS.LARGE;
        }
    }
}

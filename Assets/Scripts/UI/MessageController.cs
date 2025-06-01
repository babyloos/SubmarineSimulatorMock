using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MessageController : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;

    void Start()
    {
    }

    public void ShowMessage(ROLE role, String message)
    {
        var text = role.GetStringValue() + ": " + message + "\n";
        textMeshPro.text += text;
    }

    private List<ObservationShipInfo> observation()
    {
        return new List<ObservationShipInfo>
        {
            new ObservationShipInfo("a", 300f, 300f, 100, 100)
        };
    }

    internal class ObservationShipInfo
    {
        private string shipType;
        private float direction;
        private float course;
        private int speed;
        private int range;

        public ObservationShipInfo(string shipType, float direction, float course, int speed, int range)
        {
            this.shipType = shipType;
            this.direction = direction;
            this.course = course;
            this.speed = speed;
            this.range = range;
        }

        internal string ShipType
        {
            get { return this.shipType; }
        }
        internal float Direction
        {
            get { return this.direction; }
        }
        internal float Course
        {
            get { return this.course; }
        }
        internal int Speed
        {
            get { return this.speed; }
        }
        internal int Range
        {
            get { return this.range; }
        }
    }
}

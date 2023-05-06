using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class EnemyShipController : ShipControllerBase
{
  void Start()
  {
    base.Start();
    this.ChangeEngineOut(EngineOut.AheadSlow);
    this.ChangeCourse(90.0f);
  }

  void Update()
  {
    base.Update();
  }
}

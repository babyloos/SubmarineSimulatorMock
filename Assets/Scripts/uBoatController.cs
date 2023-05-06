using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class UBoatController : ShipControllerBase
{
  public CompassBackController CompassBackController;

  void Start()
  {
    base.Start();
  }

  void Update()
  {
    base.Update();
  }

  protected override void UpdateDirection()
  {
    base.UpdateDirection();
    this.CompassBackController.SetRotate(this._course);
  }
}

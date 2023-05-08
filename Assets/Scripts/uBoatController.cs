using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class UBoatController : ShipControllerBase
{
  public CompassBackController CompassBackController;
  public GameObject TorpedoPrefab;

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

  public void FireTorpedo() {
    var position = (this._transform.position + new Vector3(0, 0, 0)) + this._transform.forward * -38f;
    Instantiate(this.TorpedoPrefab, position, Quaternion.Euler(this._transform.eulerAngles));
  }
}

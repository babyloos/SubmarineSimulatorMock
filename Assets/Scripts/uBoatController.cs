using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using UnityEngine.Rendering;

public class UBoatController : ShipControllerBase
{
    public CompassBackController CompassBackController;
    public GameObject TorpedoPrefab;

    protected override void UpdateDirection()
    {
        this.CompassBackController.SetRotate(this._course);

        // if (this._boatProbes == null)
        // {
        //     return;
        // }

        this._course = this._transform.eulerAngles.y;
        var rotateDiff = this._distCourse - this._course;
        rotateDiff -= (float)(Math.Floor(rotateDiff / 360.0) * 360.0); // 角度差を 0～360に丸める
        if (rotateDiff > 180.0) rotateDiff -= 360.0f;                  // 角度差を-180~180に丸める
        rotateDiff = (float)Math.Floor(rotateDiff);
        if (rotateDiff < 0)
        {
            this._distTurnPower = -MAX_TURN_POWER;
        }
        else if (rotateDiff > 0)
        {
            this._distTurnPower = MAX_TURN_POWER;
        }
        else
        {
            this._distTurnPower = 0f;
        }

        // Debug.Log("distTurnPower: " + this._distTurnPower);

        // if (this._boatProbes._turnPower == this._distTurnPower)
        // {
        //     return;
        // }

        // 現在の速度を取得
        Vector3 currentVelocity = this._rigidbody.velocity;

        // 速度をスカラー値（速さ）として計算
        float msecond = currentVelocity.magnitude;
        float khour = msecond * 60 * 60 / 1000;
        float kt = khour / 1.852f;

        // Debug.Log("速度 (ベクトル): " + currentVelocity);
        // Debug.Log("速さ (スカラー値): " + khour + " km/h");
        // Debug.Log("速さ (スカラー値): " + kt + " kt/h");

        var changeRotateSpeedByAFrame = kt * this._rotateSpeed * Time.deltaTime;
        // this._boatProbes._turnPower = this._boatProbes._turnPower < this._distTurnPower ?
        //                              this._boatProbes._turnPower + changeRotateSpeedByAFrame : this._boatProbes._turnPower - changeRotateSpeedByAFrame;
    }

    public void FireTorpedo()
    {
        var position = (this._transform.position + new Vector3(0, 0, 0)) + this._transform.forward * -38f;
        Instantiate(this.TorpedoPrefab, position, Quaternion.Euler(this._transform.eulerAngles));
    }

    public SURFACE_STATUS DepthState()
    {
        var uboatDepth = this._transform.position.y;
        if (uboatDepth >= -5) {
            return SURFACE_STATUS.SURFACE;
        } else if (uboatDepth >= -14) {
            return SURFACE_STATUS.PERISCOPE;
        } else {
            return SURFACE_STATUS.SUBMERGED;
        }
    }
}

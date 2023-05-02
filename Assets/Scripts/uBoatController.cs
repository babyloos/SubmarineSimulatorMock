using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class UBoatController : MonoBehaviour
{
  public CompassBackController CompassBackController;

  private Rigidbody _rigidbody;
  private Transform _transform;
  private Animator _animator;
  private Vector4 _velocity;

  private float _distSpeed = 0f;  // mater/sec
  private float _speed = 0f;
  private float _course = 0f;
  private float _distCourse = 0f;
  private float _rotateAngle = 0f;

  void Start()
  {
    this._rigidbody = GetComponent<Rigidbody>();
    this._transform = GetComponent<Transform>();
    // Debug.Log(this._transform.forward);
    // _animator = GetComponent<Animator>();
  }

  void Update()
  {
    this.UpdateSpeed();
    this._rigidbody.AddForce(this._transform.forward * (this._speed * 10000) * -1, ForceMode.Force);
    this.UpdateDirection();
  }

  public void ChangeEngineOut(EngineOut engineOut) {
    switch(engineOut) {
      case EngineOut.AheadFull:
        this._distSpeed = 20.0f;
        break;
      case EngineOut.AheadHalf:
        this._distSpeed = 10.0f;
        break;
      case EngineOut.AheadSlow:
        this._distSpeed = 5.0f;
        break;
      case EngineOut.AllStop:
        this._distSpeed = 0.0f;
        break;
      case EngineOut.AsternSlow:
        this._distSpeed = -3.0f;
        break;
      case EngineOut.AsternHalf:
        this._distSpeed = -7.0f;
        break;
      case EngineOut.AsternFull:
        this._distSpeed = -15.0f;
        break;
      default:
        break;
    }
  }

  public void ChangeCourse(float newCourse)
  {
    this._distCourse = newCourse;
  }

  private void UpdateDirection()
  {
    this._course = this._transform.eulerAngles.y;
    var rotateDiff = this._distCourse - this._course;
    rotateDiff -= (float)(Math.Floor(rotateDiff / 360.0) * 360.0); // 角度差を 0～360に丸める
    if (rotateDiff > 180.0) rotateDiff -= 360.0f;          // 角度差を-180~180に丸める
    rotateDiff = (float)(Math.Floor(rotateDiff));
    if (rotateDiff < 0) {
      this._rotateAngle = -90f;
    } else if (rotateDiff > 0) {
      this._rotateAngle = 90f;
    } else {
      this._rotateAngle = 0f;
    }

    var rotateSpeed = this._speed / 10;
    var rotation = Quaternion.Euler(0f, this._course + this._rotateAngle, 0f);
    this._transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed * Time.deltaTime);

    this.CompassBackController.SetRotate(this._course);
  }
  
  private void UpdateSpeed() {
    // 1フレームで進む距離を計算
    this._speed += (_distSpeed - _speed) * Time.deltaTime;
  }
}

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
  private float _depth = 0f;
  private float _distDepth = 0.0f;

  void Start()
  {
    this._rigidbody = GetComponent<Rigidbody>();
    this._transform = GetComponent<Transform>();
  }

  void Update()
  {
    this.UpdateSpeed();
    this._rigidbody.AddForce(this._transform.forward * (this._speed * 10000) * -1, ForceMode.Force);
    this.UpdateDirection();
    this.UpdateDepth();
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

  public void ChangeCourse(float diff)
  {
    this._distCourse = this._course + diff;
    if (this._distCourse < 0) this._distCourse += 360f;
    this._distCourse %= 360;
  }

  public void ChangeDepth(float depth)
  {
    this._distDepth = depth;
  }

  private void UpdateDepth()
  {
    this._depth = this._transform.position.y;
    var depthSpeed = 10f + this._speed / 2;
    if (this._depth != this._distDepth) {
      var direction = new Vector3(0f, 0f, 0f);
      if (this._depth > this._distDepth) {
        direction = -this._transform.up;
      } else if (this._depth < this._distDepth) {
        direction = this._transform.up;
      }

      var force = Math.Abs(this._depth - this._distDepth) * 100;
      this._rigidbody.AddForce(direction * (depthSpeed * force), ForceMode.Force);
    }
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

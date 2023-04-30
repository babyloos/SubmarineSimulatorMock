using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class uBoatController : MonoBehaviour
{
  private Rigidbody _rigidbody;
  private Transform _transform;
  private Animator _animator;
  private Vector4 _velocity;

  private float _distSpeed = 0f; // mater/sec
  private float _speed = 0f;
  private float _course = 0f;
  private float _distCourse = 0f;

  void Start()
  {
    this._rigidbody = GetComponent<Rigidbody>();
    // _transform = GetComponent<Transform>();
    // _animator = GetComponent<Animator>();
  }

  void Update()
  {
    this.UpdateSpeed();
    this._velocity = new Vector4(this._rigidbody.velocity.x, this._rigidbody.velocity.y, 1).normalized;
    this._rigidbody.velocity = this._velocity * this._speed;
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
  
  private void UpdateSpeed() {
    // 1フレームで進む距離を計算
    _speed += (_distSpeed - _speed) * Time.deltaTime;
    // Debug.Log(_speed);
  }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class ShipControllerBase : MonoBehaviour
{
    protected const float DEFAULT_WEIGHT = 10000;
    protected const float MAX_WEIGHT = (float)(DEFAULT_WEIGHT * 10);

    private Rigidbody _rigidbody;
    protected Transform _transform;
    private Animator _animator;
    private Vector4 _velocity;

    private float _distSpeed = 0f;  // mater/sec
    private float _speed = 0f;
    protected float _course = 0f;
    private float _distCourse = 0f;
    private float _rotateAngle = 0f;
    private float _depth = 0f;
    private float _distDepth = 0.0f;
    private float _hitpoint = 1000.0f;
    private bool _isAlive = true;

    protected void Start()
    {
        this._rigidbody = GetComponent<Rigidbody>();
        this._transform = GetComponent<Transform>();
    }

    protected void Update()
    {
        if (this._isAlive)
        {
            this.UpdateSpeed();
            this._rigidbody.AddForce(this._transform.forward * (this._speed * 10000) * -1, ForceMode.Force);
            this.UpdateDirection();
            this.UpdateDepth();
        }
        else
        {
            this._rigidbody.AddForce(-this._transform.up * 10000, ForceMode.Force);
        }
    }

    public void ChangeEngineOut(EngineOut engineOut)
    {
        switch (engineOut)
        {
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

    public void AddDamage(float damage)
    {
        this._hitpoint -= damage;
        if (this._hitpoint <= 0)
        {
            this._isAlive = false;
        }
    }

    private void UpdateDepth()
    {
        if (this._rigidbody == null)
        {
            return;
        }

        this._depth = this._transform.position.y;

        if (this._depth == this._distDepth)
        {
            return;
        }

        if (this._depth > this._distDepth)
        {
            if (this._rigidbody.mass + 10 < MAX_WEIGHT)
            {
                // 潜水
                this._rigidbody.mass += 10;
            }
        }
        else if (this._depth < this._distDepth)
        {
            if (this._rigidbody.mass - 10 > DEFAULT_WEIGHT)
            {
                // 浮上
                this._rigidbody.mass -= 10;
            }
        }

        // Debug.Log("this._depth: " + this._depth);
        // Debug.Log("this._distDepth: " + this._distDepth);
    }

    protected virtual void UpdateDirection()
    {
        this._course = this._transform.eulerAngles.y;
        var rotateDiff = this._distCourse - this._course;
        rotateDiff -= (float)(Math.Floor(rotateDiff / 360.0) * 360.0); // 角度差を 0～360に丸める
        if (rotateDiff > 180.0) rotateDiff -= 360.0f;                  // 角度差を-180~180に丸める
        rotateDiff = (float)(Math.Floor(rotateDiff));
        if (rotateDiff < 0)
        {
            this._rotateAngle = -90f;
        }
        else if (rotateDiff > 0)
        {
            this._rotateAngle = 90f;
        }
        else
        {
            this._rotateAngle = 0f;
        }

        var rotateSpeed = 3f;
        var rotation = Quaternion.Euler(0f, this._course + this._rotateAngle, 0f);
        this._transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
    }

    private void UpdateSpeed()
    {
        // 1フレームで進む距離を計算
        this._speed += (_distSpeed - _speed) * Time.deltaTime;
    }
}

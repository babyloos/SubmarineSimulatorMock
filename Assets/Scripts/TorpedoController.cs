using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoController : MonoBehaviour
{
  public ParticleSystem Particle;
  private Transform _transform;
  private Rigidbody _rigidbody;
  private float _speed;

  void Start()
  {
    this._transform = gameObject.GetComponent<Transform>();
    this._rigidbody = GetComponent<Rigidbody>();
    this._speed = 40f;
  }

  void Update()
  {
    this._rigidbody.AddForce(-this._transform.forward * this._speed * 50, ForceMode.Force);
  }

  void OnCollisionEnter(Collision collision)
  {
    var hitObject = collision.gameObject;
    if (hitObject.tag.Equals("Player")) return;

    Destroy(this.gameObject);
    ParticleSystem newParticle = Instantiate(this.Particle);
    newParticle.transform.position = this._transform.position;
    var shipController = hitObject.GetComponent<ShipControllerBase>();
    if (!(shipController is null)) {
      shipController.AddDamage(1000f);
    }
  }
}

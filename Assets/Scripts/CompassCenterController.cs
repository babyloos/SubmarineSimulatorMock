using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassCenterController : MonoBehaviour
{

  private Vector3 _def;

  void Awake()
  {
    this._def = transform.localRotation.eulerAngles;
    this._def.z += 90f;
  }

  void Start()
  {
  }

  void Update()
  {
    Vector3 _parent = transform.parent.transform.localRotation.eulerAngles;
    transform.localRotation = Quaternion.Euler(this._def - _parent);
  }
}

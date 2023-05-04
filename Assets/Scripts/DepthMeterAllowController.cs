using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthMeterAllowController : MonoBehaviour
{
  void Start()
  {
  }

  void Update()
  {
  }

  public void SetRotate(float degree)
  {
    gameObject.transform.eulerAngles = new Vector3( 0f, 0f, degree);
  }
}

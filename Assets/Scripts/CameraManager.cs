using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
  public GameObject playerObject;
  public Vector2 rotationSpeed;
  public float zoomSpeed;
  public bool reverse;

  private Camera mainCamera;
  private Vector2 lastMousePosition;

  void Start()
  {
    mainCamera = Camera.main;
  }

  void Update()
  {
    var scroll = Input.mouseScrollDelta.y;
    mainCamera.transform.position += mainCamera.transform.forward * scroll * zoomSpeed;

    if (Input.GetMouseButtonDown(0))
    {
      lastMousePosition = Input.mousePosition;
    }
    else if (Input.GetMouseButton(0))
    {
      var speed = rotationSpeed;
      if (reverse) {
        speed *= -1;
      }

      var x = (lastMousePosition.x - Input.mousePosition.x);
      var y = (lastMousePosition.y - Input.mousePosition.y);

      if (Mathf.Abs(x) < Mathf.Abs(y))
        x = 0;
      else
        y = 0;

      var newAngle = Vector3.zero;
      newAngle.x = x * speed.x;
      newAngle.y = y * speed.y;

      mainCamera.transform.RotateAround(playerObject.transform.position, Vector3.up, newAngle.x);
      mainCamera.transform.RotateAround(playerObject.transform.position, mainCamera.transform.right, -newAngle.y);
      lastMousePosition = Input.mousePosition;
    }
  }
}

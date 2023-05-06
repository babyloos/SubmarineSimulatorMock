using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipManager : MonoBehaviour
{
  public GameObject CargoShipPrefab;

  void Start () {
    Instantiate(this.CargoShipPrefab, new Vector3(-10.0f,0.0f,-200.0f), Quaternion.identity);
  }

  void Update()
  {
  }
}

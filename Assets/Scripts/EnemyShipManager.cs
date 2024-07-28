using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipManager : MonoBehaviour
{
  public GameObject CargoShipPrefab;
  public GameObject DestroyerPrefab;

  void Start () {
    Instantiate(this.CargoShipPrefab, new Vector3(-10.0f,0.0f,-200.0f), Quaternion.Euler(0, 90f, 0));
    Instantiate(this.CargoShipPrefab, new Vector3(-10.0f,0.0f,-400.0f), Quaternion.Euler(0, 90f, 0));
    Instantiate(this.CargoShipPrefab, new Vector3(-10.0f,0.0f,-600.0f), Quaternion.Euler(0, 90f, 0));
    Instantiate(this.DestroyerPrefab, new Vector3(-10.0f,0.0f,-800.0f), Quaternion.Euler(0, 90f, 0));
  }

  void Update()
  {
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FireTorpedoButtonController : MonoBehaviour, IPointerClickHandler
{
  public UBoatController UboatController;

  void Start()
  {
  }

  void Update()
  {
  }

  public void OnPointerClick(PointerEventData pointerData)
  {
    this.UboatController.FireTorpedo();
  }

}

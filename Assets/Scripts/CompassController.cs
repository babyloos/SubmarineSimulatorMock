using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Common;

public class CompassController : MonoBehaviour, IPointerClickHandler
{
  public GameObject UboatObject;
  private UBoatController _uboatController;

  private RectTransform _rectTransform;

  void Start()
  {
    this._rectTransform = gameObject.GetComponent<RectTransform>();
    this._uboatController = this.UboatObject.GetComponent<UBoatController>();
  }

  void Update()
  {
  }

  public void OnPointerClick(PointerEventData pointerData)
  {
    var clickedPoint = new Vector2(pointerData.position.x - this._rectTransform.position.x, pointerData.position.y - this._rectTransform.position.y);
    var angle = Utility.GetClickDeg(new Vector2(0.0f, 0.0f), clickedPoint);
    if (angle < 0) angle += 360;
    this._uboatController.ChangeCourse((float)angle);
  }
}

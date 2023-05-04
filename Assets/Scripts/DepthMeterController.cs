using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Common;

public class DepthMeterController : MonoBehaviour, IPointerClickHandler
{
  public GameObject UboatObject;
  public DepthMeterAllowController DepthMeterAllow;

  private RectTransform _rectTransform;
  private UBoatController _uboatController;

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
    var resultDepth = 0f;
    var clickedPoint = new Vector2(pointerData.position.x - this._rectTransform.position.x, pointerData.position.y - this._rectTransform.position.y);
    var angle = Utility.GetClickDeg(new Vector2(0.0f, 0.0f), clickedPoint);
    if (angle >= -180 && angle <= -152) {
      resultDepth = 0f;
      this.DepthMeterAllow.SetRotate(152);
    } else if (angle <= 180 && angle >= 152) {
      resultDepth = 260f;
      this.DepthMeterAllow.SetRotate(-152);
    } else {
      resultDepth = (float)((angle + 152) / 1.16923 * -1);
      this.DepthMeterAllow.SetRotate((float)angle * -1);
    }

    this._uboatController.ChangeDepth(resultDepth);
  }
}

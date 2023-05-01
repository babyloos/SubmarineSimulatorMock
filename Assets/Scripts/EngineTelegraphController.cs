using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Common;

public class EngineTelegraphController : MonoBehaviour, IPointerClickHandler
{
  public GameObject TargetObject;
  public EngineTelegraphAllowController AllowController;

  private Vector2 _center;
  private UBoatController _uboatController;
  private RectTransform _rectTransform;

  void Start()
  {
    this._uboatController = this.TargetObject.GetComponent<UBoatController>();
    this._rectTransform = gameObject.GetComponent<RectTransform>();
  }

  void Update()
  {
  }

  public void OnPointerClick(PointerEventData pointerData)
  {
    var clickedPoint = new Vector2(pointerData.position.x - this._rectTransform.position.x, pointerData.position.y - this._rectTransform.position.y);
    var angle = Utility.GetClickDeg(new Vector2(0.0f, 0.0f), clickedPoint);
    if (angle <= -45 || angle >= 45) 
    {
      this.AllowController.SetRotate((float)angle * -1 + 180);
      var engineOut = GetEngineOutFromDeg(angle);
      this._uboatController.ChangeEngineOut(engineOut);
    }
	}

  private EngineOut GetEngineOutFromDeg(double deg)
  {
    var result = EngineOut.AllStop;

    if (deg <= -160 && deg >= 160)
    {
      result = EngineOut.AllStop;
    }
    else if (deg >= -161 && deg <= -125)
    {
      result = EngineOut.AheadSlow;
    }
    else if (deg >= -126 && deg <= -85)
    {
      result = EngineOut.AheadHalf;
    }
    else if (deg >= -86 && deg <= -45)
    {
      result = EngineOut.AheadFull;
    }
    else if (deg <= 160 && deg >= 125)
    {
      result = EngineOut.AsternSlow;
    }
    else if (deg <= 124 && deg >= 85)
    {
      result = EngineOut.AsternHalf;
    }
    else if (deg <= 84 && deg >= 45)
    {
      result = EngineOut.AsternFull;
    }

    return result;
  }
}

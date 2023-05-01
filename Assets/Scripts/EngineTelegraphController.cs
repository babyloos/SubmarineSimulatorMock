using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Common;

public class UIController : MonoBehaviour, IPointerClickHandler
{
  public GameObject TargetObject;
  public EngineTelegraphAllowController AllowController;

  private Vector2 _center;

  void Start()
  {
    var rect = gameObject.GetComponent<RectTransform>();
    var width = rect.sizeDelta.x;
    var height = rect.sizeDelta.y;
    this._center = new Vector2(width / 2, height / 2);
  }

  void Update()
  {
  }

  public void OnPointerClick(PointerEventData pointerData)
  {
    var angle = Utility.GetClickDeg(this._center, pointerData.position);
    if (angle <= -45 || angle >= 45) 
    {
      this.AllowController.SetRotate((float)angle * -1 + 180);
      Debug.Log("EngineOut: " + GetEngineOutFromDeg(angle));
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

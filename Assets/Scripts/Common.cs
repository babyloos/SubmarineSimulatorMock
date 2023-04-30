using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common {
  public enum EngineOut {
    AheadFull,
    AheadHalf,
    AheadSlow,
    AllStop,
    AsternSlow,
    AsternHalf,
    AsternFull,
  }

  public static class Utility
  {
    /**
     * クリックした位置の中心からの角度を求める
     */
    public static double GetClickDeg(Vector2 center, Vector2 clickPoint)
    {
      var radian = Math.Atan2(clickPoint.x - center.x, clickPoint.y - center.y);
      var angle = radian * 180 / Math.PI;

      return angle;
    }
  }
}

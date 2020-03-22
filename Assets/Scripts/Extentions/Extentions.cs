using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public static class Extentions
{
    public static float WorldWidth(this Camera camera)
    {
        return 2 * camera.orthographicSize * Screen.width / Screen.height;
    }

    public static float LeftPosition(this Camera camera)
    {
        if (camera == null)
            return 0f;
           
        return camera.transform.position.x - camera.WorldWidth() / 2;
    }

    public static float RightPosition(this Camera camera)
    {
        if (camera == null)
            return 0f;

        return camera.transform.position.x + camera.WorldWidth() / 2;
    }

    public static int LastPointInd(this Spline spline)
    {
        return spline.GetPointCount() - 1;
    }
}

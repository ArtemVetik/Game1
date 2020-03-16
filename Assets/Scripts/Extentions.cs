using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extentions
{
    public static float WorldWidth(this Camera camera)
    {
        return 2 * camera.orthographicSize * Screen.width / Screen.height;
    }

    public static float LeftPosition(this Camera camera)
    {
        return camera.transform.position.x - camera.WorldWidth() / 2;
    }

    public static float RightPosition(this Camera camera)
    {
        return camera.transform.position.x + camera.WorldWidth() / 2;
    }
}

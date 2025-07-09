using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanTouchTesting : MonoBehaviour
{
    public void MethodToCall(Vector2 delta)
    {
        Debug.Log(delta);

        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            if (delta.x < 0)
                Debug.Log("LEFT");
            else
                Debug.Log("RIGHT");
        }
        else
        {
            if (delta.y < 0)
                Debug.Log("DOWN");
            else
                Debug.Log("UP");
        }
    }
}

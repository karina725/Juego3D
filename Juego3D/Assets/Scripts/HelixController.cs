using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixController : MonoBehaviour
{
    private Vector2 lastTapPosition;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.localEulerAngles;
    }

    void Update()
    {
       if(Input.GetMouseButton(0))
       {
        Vector2 currentTapPosition = Input.mousePosition;
        if(lastTapPosition == Vector2.zero)
        {
            lastTapPosition = currentTapPosition;
        }
        float distance = lastTapPosition.x - currentTapPosition.x;
        lastTapPosition = currentTapPosition;

        transform.Rotate(Vector3.up * distance);

       }

       if(Input.GetMouseButtonUp(0))
       {
        lastTapPosition = Vector2.zero;
       }

    }
}

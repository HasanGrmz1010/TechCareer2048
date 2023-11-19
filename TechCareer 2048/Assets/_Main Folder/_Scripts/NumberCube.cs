using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberCube : MonoBehaviour
{
    int number;
    void Start()
    {
        
    }

    void Update()
    {
        if (MouseActionsManager.instance.isDragging && MouseActionsManager.instance.isHoldingCube)
        {
            transform.Rotate(0f, 50f * Time.deltaTime, 0f);
            transform.position = Vector3.Lerp(transform.position, 
                MouseActionsManager.instance.mouse_debug.transform.position, 
                10f * Time.deltaTime);
        }

        if (!MouseActionsManager.instance.isHoldingCube)
        {
            CheckAdditionValid();
        }
    }

    void CheckAdditionValid()
    {

    }

    public int GetNumber()
    {
        return number;
    }

    public void SetNumber(int _val)
    {
        number = _val;
    }
}

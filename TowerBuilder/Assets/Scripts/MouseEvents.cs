using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public event EventHandler OnMouseEnter;
    public event EventHandler OnMouseExit;

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Enter");
        OnMouseEnter?.Invoke(this, EventArgs.Empty);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Exit");
        OnMouseExit?.Invoke(this, EventArgs.Empty);
    }
}

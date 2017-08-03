using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string btnName;
    public GameMenu gameMenu;

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameMenu.SetButtonState(btnName, true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameMenu.SetButtonState(btnName, false);
    }
}
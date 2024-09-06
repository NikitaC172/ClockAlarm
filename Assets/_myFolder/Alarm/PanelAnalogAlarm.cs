using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanelAnalogAlarm : Button
{
    public Action<bool> Pressed;

    public override void OnPointerDown(PointerEventData eventData)
    {
        Pressed?.Invoke(true);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        Pressed?.Invoke(false);
    }
}

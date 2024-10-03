using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressTextMovement : MonoBehaviour
{
    public RectTransform buttonTextRectTransform;
    private Vector3 originalPosition;


    private void Start()
    {
        originalPosition = buttonTextRectTransform.localPosition;
    }

    public void OnButtonPressed()
    {
        buttonTextRectTransform.localPosition = originalPosition + new Vector3(0, -6, 0);
    }

    public void OnButtonReleased()
    {
        buttonTextRectTransform.localPosition = originalPosition;
    }

}

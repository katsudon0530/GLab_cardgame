using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour
{
    private Image _image;
    
    /// <summary>
    ///　クリックされたときに発火するAction
    /// </summary>
    public Action OnClick;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
       Debug.Log(Clicked()); 
    }

    private bool Clicked()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 localPoint = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform, Input.mousePosition, Camera.main, out localPoint);
        return rectTransform.rect.Contains(localPoint);
    } 
}

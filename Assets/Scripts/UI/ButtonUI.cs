using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour
{
    private Image _image;
    
    /// <summary>
    ///　クリックされたときに発火するAction
    /// </summary>
    private Action _onClick;
    public Action OnClick{get => _onClick; set => _onClick = value;}
    
    [FormerlySerializedAs("_color")] [SerializeField]
    Color _OnCursorColor;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _onClick += ForDebug;
    }
    void ForDebug()
    {
        Debug.Log("click");
    }

    private void Update()
    {
        if (OnMouseCursor())
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                _onClick?.Invoke();　//　Actionを発火（Actionの中身がnullの場合を考慮）
            }
            _image.color = Color.Lerp(_image.color, _OnCursorColor, Time.deltaTime);
            
        }
       
    }

    private bool OnMouseCursor()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 localPoint = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform, Input.mousePosition, Camera.main, out localPoint); // 第一引数に入れたRectTransformに対する第二引数のローカル座標をRectTransform基準で返す
        return rectTransform.rect.Contains(localPoint);　//　カーソルのローカル座標が範囲内にあるかどうかを返す
    }

  
}

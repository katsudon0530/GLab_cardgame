using System;
using System.Collections;
using Unity.VisualScripting;
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
    
    /// <summary>
    ///　このActionにクリック時に発火させたい処理を追加する
    /// </summary>
    public Action OnClick{get => _onClick; set => _onClick = value;}
    
    [FormerlySerializedAs("_OnCursorColor")] [SerializeField]
    Color _onCursorColor;
    
    [FormerlySerializedAs("_OnCursorColor")] [SerializeField]
    Color _defaultColor;
    
    private bool _onCursor = false;
    
    private Coroutine _colorCoroutine;
    
    [SerializeField]
    private float _colorChangeTime = 0.5f;  

    public bool OnCursor
    {
        get => _onCursor;
        set
        {
            if (_onCursor != value)
            {
                _onCursor = value;
                if (_colorCoroutine != null)
                {
                    StopCoroutine(_colorCoroutine);
                }
                _colorCoroutine = StartCoroutine(ChangeColor(_onCursor ? _onCursorColor : _defaultColor));
            }
        }
    }

    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.color = _defaultColor;
        _onClick += ForDebug;
    }
    void ForDebug()
    {
        Debug.Log("click");
    }

    private void Update()
    {
        OnMouseCursor();
        if (OnCursor)
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                if (_onClick == null)
                {
                    Debug.Log("クリック時の処理が一つも登録されていません");
                }
                else
                {
                    _onClick.Invoke();　
                    //　Actionを発火（Actionの中身がnullの場合を考慮）
                }
              
            }
            
            
        }
       
    }

    private void OnMouseCursor()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 localPoint = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform, Input.mousePosition, Camera.main, out localPoint); // 第一引数に入れたRectTransformに対する第二引数のローカル座標をRectTransform基準で返す
        OnCursor = rectTransform.rect.Contains(localPoint);　//　カーソルのローカル座標がimageの範囲内にあるかどうかを返す
    }

    IEnumerator ChangeColor(Color targetColor)
    {
        Color startColor = _image.color;
        float elapsedTime = 0;
        while (elapsedTime < _colorChangeTime)
        {
            elapsedTime += Time.deltaTime;
            _image.color = Color.Lerp(startColor, targetColor, elapsedTime /_colorChangeTime);
            yield return null;
        }
        _image.color = targetColor;
        _colorCoroutine = null;
    }

  
}

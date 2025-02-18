using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
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

        #region ColorSettings

        [FormerlySerializedAs("_onCursorColor")]
        [Header("カーソルがボタン上にある時の色")]
        [SerializeField]
        private　Color onCursorColor;
        [FormerlySerializedAs("_onClickedColor")]
        [Header("クリック時の色")]
        [SerializeField]
        private　Color onClickedColor;
        [Header("選択済みの時の色")]
        [SerializeField]
        private　Color selectedColor;
        [FormerlySerializedAs("_defaultColor")]
        [Header("ボタンのデフォルトの色")]
        [SerializeField]
        private Color defaultColor;
        [FormerlySerializedAs("_colorChangeTime")]
        [Header("色が切り替わるための遷移時間")]
        [SerializeField]
        private float colorChangeTime = 0.5f;  
        [Header("クリック時の色の持続時間")]
        [SerializeField]
        private float clickedColorDuration = 0.5f;  

        #endregion  
        private bool _onCursor;
    
        /// <summary>
        ///　遷移中のコルーチンがこの変数に都度保存される
        /// </summary>
        private Coroutine _colorCoroutine;
        
        private bool _selected;
    
        
        private bool OnCursor
        {
            get => _onCursor;
            set
            {
                if (_onCursor != value )　// カーソルがボタン上にあるか判定しているboolの値が切り替わったとき
                {
                    _onCursor = value;　// 変更を適用
                    if (_colorCoroutine != null)　
                    {
                        StopCoroutine(_colorCoroutine);
                        // 色の変更の遷移中であるときに_onCursorが切り替わった時に現在の遷移を止める　
                    }

                    if (!_selected)
                    {
                        _colorCoroutine = StartCoroutine(ChangeColor(_onCursor ? onCursorColor : defaultColor));
                        //色の切り替え開始　遷移中のコルーチンを変数に保存(遷移中であるかどうかの判定のため)
                    }
                    
                }
            }
        }

        private void Awake()
        {
            _selected = false;
            _image = GetComponent<Image>();
            _image.color = defaultColor;　
        }
       
        private void Update()
        {
            OnMouseCursor();
            if (OnCursor)
            {
                OnClicked();

                OnPressDown();
            }

        
        }

        private void OnClicked()
        {
            if (Input.GetMouseButtonUp(0)) //左クリック時に
            {
                if (_colorCoroutine != null)　
                {
                    StopCoroutine(_colorCoroutine);
                    // 色の変更の遷移中であるときに_onCursorが切り替わった時に現在の遷移を止める　
                }
                _selected = true;
                _image.color = selectedColor;
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

        private void OnPressDown()
        {
            if (Input.GetMouseButton(0))
            {
                if (_colorCoroutine != null)　
                {
                    StopCoroutine(_colorCoroutine);
                    // 色の変更の遷移中であるときに_onCursorが切り替わった時に現在の遷移を止める　
                }
                _image.color = onClickedColor;
            }
        }


        private void OnMouseCursor()
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform, Input.mousePosition, Camera.main, out Vector2 localPoint); // 第一引数に入れたRectTransformに対する第二引数のローカル座標をRectTransform基準で返す
            OnCursor = rectTransform.rect.Contains(localPoint);　//　カーソルのローカル座標がimageの範囲内にあるかどうかを返す
        }

        IEnumerator ChangeColor(Color targetColor)
        {
            Color startColor = _image.color;　//Colorの初期値を保存
            float elapsedTime = 0;　// 現在の遷移時間
            while (elapsedTime < colorChangeTime)　// 決められた遷移時間になるまで繰り返す
            {
                elapsedTime += Time.deltaTime;　// 遷移時間を計算していく 
                _image.color = Color.Lerp(startColor, targetColor, elapsedTime /colorChangeTime); //色を徐々にtargetColorに切り替える（Lerpの第三引数は0~1の間で遷移するように）
                yield return null;　
            }
            _image.color = targetColor;　// 最終的な色の変更
            _colorCoroutine = null;　// 遷移が終わったので中身を空にする
        }

    }
}

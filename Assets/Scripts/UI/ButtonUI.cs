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
        
        [SerializeField]
        private string text;

        #region ColorSettings

        [FormerlySerializedAs("_onCursorColor")]
        [Header("カーソルがボタン上にある時の色")]
        public　Color onCursorColor;
        [FormerlySerializedAs("_onClickedColor")]
        [Header("クリック時の色")]
        public　Color onClickedColor;
        [Header("選択済みの時の色")]
        public　Color selectedColor;
        [FormerlySerializedAs("_defaultColor")]
        [Header("ボタンのデフォルトの色")]
        public Color defaultColor;
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
        
        /// <summary>
        ///　選択済みであるかどうかのフラグ
        /// </summary>
        private bool _selected;

        /// <summary>
        ///　クリックしてからの時間を計測する変数
        /// </summary>
        private float _currentTime;
        
        /// <summary>
        ///　キーが押されているかどうかのフラグ
        /// </summary>
        private bool _onPress;
        
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

                    if (!_selected && !_onPress)
                    {
                        _colorCoroutine = StartCoroutine(ChangeColor(_onCursor ? onCursorColor : defaultColor));
                        //色の切り替え開始　遷移中のコルーチンを変数に保存(遷移中であるかどうかの判定のため)
                    }
                    
                }
            }
        }
        
      
        private void Awake()
        {
            Init();
            
        }
        
        
        /// <summary>
        ///　初期化処理
        /// </summary>
        private void Init()
        {
            _selected = false;　// 選択されていない状態
            _image = GetComponent<Image>();
            _image.color = defaultColor;
            _onClick += ForDebug;

            var buttonText = transform.GetComponentInChildren<Text>();
            if (buttonText == null)
            {
                Debug.Log("Textコンポーネントが見つかりません。追加する場合はボタンの子オブジェクトにTextコンポーネントを配置してください");
            }
        }

        void ForDebug()
        {
            Debug.Log($"{gameObject.name}がクリックされました");
        }
       
        private void Update()
        {
                OnMouseCursor();
                OnClicked(); 
                PressDown();
        }
        

        /// <summary>
        ///　クリックされたときの処理
        /// </summary>
        private void OnClicked()
        {
            if (Input.GetMouseButtonUp(0)) //左クリック時に
            {
                if (_onCursor)
                {
                       
                    if (_colorCoroutine != null)
                    {
                        StopCoroutine(_colorCoroutine);
                        // 色の変更の遷移中であるときに_onCursorが切り替わった時に現在の遷移を止める　
                    }

                    _selected = true;　// 選択済みに
                    ChangeToSelectedColor();　// ボタンの色を選択済みに
                    ActionInvoke();
                }
                else
                {
                    if (!_selected)
                    {
                        _colorCoroutine = StartCoroutine(ChangeColor(defaultColor)); 
                        //カーゾルがクリックしたままボタン外に行った場合デフォルトの色に
                    }
                    else
                    {
                        _colorCoroutine = StartCoroutine(ChangeColor(selectedColor));
                        //　既に選択済みである場合はいかなる場合でも選択済みの色に戻る
                    }
                   
                }

            }
            
        }

        private void ActionInvoke()
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
        
        /// <summary>
        ///　選択済みの色に遷移するメソッド
        /// </summary>
        private void ChangeToSelectedColor()
        {
            if (_currentTime < clickedColorDuration)
            {
                float time  = clickedColorDuration - colorChangeTime;
                StartCoroutine(WaitChangeColor(time));
                // 押し始めから押し切るまでの時間が短い場合に最低でも clickedColorDuration分待ってから色の遷移を始める

            }
            else
            {
                _image.color = selectedColor;
            }
        }

        IEnumerator WaitChangeColor(float time)
        {
            yield return new WaitForSeconds(time);
            _image.color = selectedColor;
        }

        private void PressDown()
        {
            if (Input.GetMouseButton(0))
            {
                _onPress = true;
                if (_onCursor)
                {
                    _currentTime += Time.deltaTime;　// 押し始めからの時間を計測
                    if (_colorCoroutine != null)　
                    {
                        StopCoroutine(_colorCoroutine);
                        // 色の変更の遷移中であるときに_onCursorが切り替わった時に現在の遷移を止める　
                    }
                    _image.color = onClickedColor;
                }
               
            }
            else
            {
                _onPress = false;
                _currentTime = 0;　
            }
        }


        
        /// <summary>
        ///　マウスカーソルがボタン上にあるかどうかを判定する
        /// </summary>
        private void OnMouseCursor()
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform, Input.mousePosition, Camera.main, out Vector2 localPoint); // 第一引数に入れたRectTransformに対する第二引数のローカル座標をRectTransform基準で返す
            OnCursor = rectTransform.rect.Contains(localPoint);　//　カーソルのローカル座標がimageの範囲内にあるかどうかを返す
        }
        
        /// <summary>
        ///　色を徐々に変化させる
        /// </summary>
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


using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] buttons;
    
    GameObject _currentButton;
    
    [SerializeField]
    private bool navigationMode;


    private void Start()
    {
        if (navigationMode)
        {
            Init();
        }
    }

    private void Update()
    {
        if (navigationMode)
        {
            ChangeButton();
            SelectButton();
        }
       
    }

    private void Init()
    {
            var image = buttons[0].GetComponent<Image>();
            var buttonUi = buttons[0].GetComponent<ButtonUI>();
            image.color = buttonUi.onCursorColor; //　ボタンの色をハイライトの色に
            _currentButton = buttons[0];　// 現在選択されているボタンを保存
    }

       
    private void ChangeButton()
    {
            float minDiff =float.MaxValue;　　// 選択中のボタンからの誤差(一番小さいもの)を求めるための変数
            GameObject nextButton = null;　// 次に選択するボタン
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                foreach (var button in buttons)
                {
                    if (button != _currentButton && button.transform.position.x > _currentButton.transform.position.x )
                    {
                        float diff = button.transform.position.x - _currentButton.transform.position.x;
                        if (diff < minDiff)
                        {
                            minDiff = diff;
                            nextButton = button;
                        }
                        //　→の入力の時に現在のボタンのｘよりも値が大きく、かつ誤差が一番小さいボタンをnextButtonとする
                    }
                }
                if (nextButton != null)
                {
                    var buttonUi = _currentButton.GetComponent<ButtonUI>();
                    var image = _currentButton.GetComponent<Image>();
                    image.color = buttonUi.defaultColor;　// 現在の選択済みのボタンをdefaultColorに
                    var nextImage = nextButton.GetComponent<Image>();
                    var nextButtonUi = nextButton.GetComponent<ButtonUI>();
                    nextImage.color = nextButtonUi.onCursorColor;　// 次に選択するボタンをハイライトColorに
                    _currentButton = nextButton;　// 変更を適用
                  
                }
              
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                foreach (var button in buttons)
                {
                    if (button != _currentButton && button.transform.position.y > _currentButton.transform.position.y )
                    {
                        float diff = button.transform.position.y - _currentButton.transform.position.y;
                        if (diff < minDiff)
                        {
                            minDiff = diff;
                            nextButton = button;
                        }
                        //　↑の入力の時に現在のボタンのｙよりも値が大きく、かつ誤差が一番小さいボタンをnextButtonとする
                        
                    }
                }

                if (nextButton != null)
                {
                    var buttonUi = _currentButton.GetComponent<ButtonUI>();
                    var image = _currentButton.GetComponent<Image>();
                    image.color = buttonUi.defaultColor;
                    var nextImage = nextButton.GetComponent<Image>();
                    var nextButtonUi = nextButton.GetComponent<ButtonUI>();
                    nextImage.color = nextButtonUi.onCursorColor;
                    _currentButton = nextButton;
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                foreach (var button in buttons)
                {
                    if (button != _currentButton && button.transform.position.x < _currentButton.transform.position.x )
                    {
                        float diff = _currentButton.transform.position.x - button.transform.position.x;
                        if (diff < minDiff)
                        {
                            minDiff = diff;
                            nextButton = button;
                        }
                        //　←の入力の時に現在のボタンのｘよりも値が小さく、かつ誤差が一番小さいボタンをnextButtonとする
                    }
                }

                if (nextButton != null)
                {
                    var buttonUi = _currentButton.GetComponent<ButtonUI>();
                    var image = _currentButton.GetComponent<Image>();
                    image.color = buttonUi.defaultColor;
                    var nextImage = nextButton.GetComponent<Image>();
                    var nextButtonUi = nextButton.GetComponent<ButtonUI>();
                    nextImage.color = nextButtonUi.onCursorColor;
                    _currentButton = nextButton;
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                foreach (var button in buttons)
                {
                    if (button != _currentButton && button.transform.position.y < _currentButton.transform.position.y )
                    {
                        float diff = _currentButton.transform.position.y - button.transform.position.y;
                        if (diff < minDiff)
                        {
                            minDiff = diff;
                            nextButton = button;
                        }
                        //　↓の入力の時に現在のボタンのｙよりも値が小さく、かつ誤差が一番小さいボタンをnextButtonとする
                    }
                }

                if (nextButton != null)
                {
                    var buttonUi = _currentButton.GetComponent<ButtonUI>();
                    var image = _currentButton.GetComponent<Image>();
                    image.color = buttonUi.defaultColor;
                    var nextImage = nextButton.GetComponent<Image>();
                    var nextButtonUi = nextButton.GetComponent<ButtonUI>();
                    nextImage.color = nextButtonUi.onCursorColor;
                    _currentButton = nextButton;
                }
            }
           
            
    }

    private void SelectButton()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            var image = _currentButton.GetComponent<Image>();
            var buttonUi = _currentButton.GetComponent<ButtonUI>();
            image.color = buttonUi.selectedColor;
            buttonUi.OnClick?.Invoke();
            // Enterキーが押されたときにOnClickを発火
        }
    }
}
}


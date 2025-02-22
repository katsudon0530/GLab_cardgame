using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ButtonManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] _buttons;

        private GameObject _currentButton;

        [SerializeField] private bool _navigationMode;

        private void Start()
        {
            if (_navigationMode)
            {
                Init();
            }
        }

        private void Update()
        {
            if (_navigationMode)
            {
                ChangeButton();
                SelectButton();
            }
        }

        private void Init()
        {
            var image = _buttons[0].GetComponent<Image>();
            var buttonUi = _buttons[0].GetComponent<ButtonUI>();
            image.color = buttonUi.OnCursorColor; //　ボタンの色をハイライトの色に
            _currentButton = _buttons[0];　// 現在選択されているボタンを保存
        }

        private void ChangeButton()
        {
            float minDiff = float.MaxValue; // 選択中のボタンからの誤差(一番小さいもの)を求めるための変数
            GameObject nextButton = null;　// 次に選択するボタン
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                foreach (var button in _buttons)
                {
                    if (button != _currentButton && button.transform.position.x > _currentButton.transform.position.x)
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
                    image.color = buttonUi.DefaultColor;　// 現在の選択済みのボタンをdefaultColorに
                    var nextImage = nextButton.GetComponent<Image>();
                    var nextButtonUi = nextButton.GetComponent<ButtonUI>();
                    nextImage.color = nextButtonUi.OnCursorColor;　// 次に選択するボタンをハイライトColorに
                    _currentButton = nextButton;　// 変更を適用
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                foreach (var button in _buttons)
                {
                    if (button != _currentButton && button.transform.position.y > _currentButton.transform.position.y)
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
                    image.color = buttonUi.DefaultColor;
                    var nextImage = nextButton.GetComponent<Image>();
                    var nextButtonUi = nextButton.GetComponent<ButtonUI>();
                    nextImage.color = nextButtonUi.OnCursorColor;
                    _currentButton = nextButton;
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                foreach (var button in _buttons)
                {
                    if (button != _currentButton && button.transform.position.x < _currentButton.transform.position.x)
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
                    image.color = buttonUi.DefaultColor;
                    var nextImage = nextButton.GetComponent<Image>();
                    var nextButtonUi = nextButton.GetComponent<ButtonUI>();
                    nextImage.color = nextButtonUi.OnCursorColor;
                    _currentButton = nextButton;
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                foreach (var button in _buttons)
                {
                    if (button != _currentButton && button.transform.position.y < _currentButton.transform.position.y)
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
                    image.color = buttonUi.DefaultColor;
                    var nextImage = nextButton.GetComponent<Image>();
                    var nextButtonUi = nextButton.GetComponent<ButtonUI>();
                    nextImage.color = nextButtonUi.OnCursorColor;
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
                image.color = buttonUi.SelectedColor;
                buttonUi.Onclick?.Invoke();
                // Enterキーが押されたときにOnClickを発火
            }
        }
    }
}
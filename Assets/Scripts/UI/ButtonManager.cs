
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] buttons;
    
    GameObject _currentButton ;
    
    [SerializeField]
    private bool navigationMode;


    private void Start()
    {
        if (navigationMode)
        {
            Navigation();
        }
    }

    private void Update()
    {
        ChangeButton();
        SelectButton();
    }

    private void Navigation()
    {
            var image = buttons[0].GetComponent<Image>();
            var buttonUi = buttons[0].GetComponent<ButtonUI>();
            image.color = buttonUi.onCursorColor;
            _currentButton = buttons[0];
            
    }

       
    private void ChangeButton()
    {
            float minDiff =float.MaxValue;
            GameObject nextButton = null;
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
        }
    }
}
}


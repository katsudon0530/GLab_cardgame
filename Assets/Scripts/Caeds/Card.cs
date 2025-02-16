using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Image Frame;
    [SerializeField] Image nameFrame;
    [SerializeField] Text nameText;
    [SerializeField] Text numberText;
    [SerializeField] Image icon;
    [SerializeField] Text descriotionText;
    [SerializeField] GameObject hidePanel;
    [SerializeField] GameObject SynthesisPanel;
    [SerializeField] GameObject effectUp;
    [SerializeField] GameObject effectDown;
    public CardBase Base { get; private set; }
    public GameObject EffectUp { get => effectUp; set => effectUp = value; }
    public GameObject EffectDown { get => effectDown; set => effectDown = value; }

    public UnityAction<Card> OnClickCard;

    public Vector3 originalSize;
    public Vector3 originalPosition;

    //カード内容の定義
    public void Set(CardBase cardBase)
    {
        Base = cardBase;
        nameText.text = cardBase.Name1;
        icon.sprite = cardBase.Icon;
        descriotionText.text = cardBase.Description;
        nameFrame.color = cardBase.Color;
        Frame.color = cardBase.Color;
        SynthesisPanel.SetActive(false);

    }

    //カードクリック時のリアクション先の参照
    public void OnClick()
    {
        OnClickCard?.Invoke(this);
    }
    //カードにをクリックした後の位置補正
    public void PosReset()
    {
        transform.position += Vector3.up * 0.2f;
    }
    //カードにマウスカーソルが入った時の反応
    public void PointerEnter()
    {
        originalSize = transform.localScale;
        originalPosition = transform.position;
        transform.position += Vector3.up * 0.2f;
        transform.localScale = originalSize * 1.1f;
        GetComponentInChildren<Canvas>().sortingLayerName = "overLay";
    }

    //カードからマウスカーソルが出た時のリアクション
    public void PointerExit()
    {
        transform.position -= Vector3.up * 0.2f;
        transform.localScale = originalSize;
        GetComponentInChildren<Canvas>().sortingLayerName = "Default";
    }

    //カードのハイドパネルを非表示にする
    public void Open()
    {
        SynthesisPanel.SetActive(false);
    }

    public void Close()
    {
        SynthesisPanel.SetActive(true);
    }

}

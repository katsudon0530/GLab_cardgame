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

    //�J�[�h���e�̒�`
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

    //�J�[�h�N���b�N���̃��A�N�V������̎Q��
    public void OnClick()
    {
        OnClickCard?.Invoke(this);
    }
    //�J�[�h�ɂ��N���b�N������̈ʒu�␳
    public void PosReset()
    {
        transform.position += Vector3.up * 0.2f;
    }
    //�J�[�h�Ƀ}�E�X�J�[�\�������������̔���
    public void PointerEnter()
    {
        originalSize = transform.localScale;
        originalPosition = transform.position;
        transform.position += Vector3.up * 0.2f;
        transform.localScale = originalSize * 1.1f;
        GetComponentInChildren<Canvas>().sortingLayerName = "overLay";
    }

    //�J�[�h����}�E�X�J�[�\�����o�����̃��A�N�V����
    public void PointerExit()
    {
        transform.position -= Vector3.up * 0.2f;
        transform.localScale = originalSize;
        GetComponentInChildren<Canvas>().sortingLayerName = "Default";
    }

    //�J�[�h�̃n�C�h�p�l�����\���ɂ���
    public void Open()
    {
        SynthesisPanel.SetActive(false);
    }

    public void Close()
    {
        SynthesisPanel.SetActive(true);
    }

}

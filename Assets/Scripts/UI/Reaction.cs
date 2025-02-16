using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Reaction : MonoBehaviour
{
    [SerializeField]Color colorOn = Color.grey;
    [SerializeField] bool Chage;
    Image imege;
    public void ButtonPointerEnter()
    {
        transform.localScale = Vector3.one * 1.1f;
        if (Chage == true)
        {
            ChageColor();
        }
    }

    //�J�[�h����}�E�X�J�[�\�����o�����̃��A�N�V����
    public void ButtonPointerExit()
    {
        ButtonReSet();
    }

    //�{�^���̏�Ԃ����Ƃɖ߂�
    public void ButtonReSet()
    {
        Image imege;
        transform.localScale = Vector3.one;
        if (Chage == true)
        {
            imege = GetComponent<Image>();
            imege.color = Color.white;
        }
    }
    
    //�F��ω�������
    public void ChageColor()
    {
        imege = GetComponent<Image>();
        imege.color = colorOn;
    }
}

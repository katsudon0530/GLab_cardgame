using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlerHand : MonoBehaviour
{
    List<Card> list = new();
    public float cardInterval;

    public List<Card> List { get => list; set => list = value; }


    //�J�[�h�����X�g�ɒǉ�
    public void Add(Card card)
    {
        list.Add(card);
        card.transform.SetParent(this.transform);
    }

    //�J�[�h�����X�g����폜
    public void RemoveList(Card card)
    {
        list.Remove(card);
    }

    //�J�[�h�̃f�B���N�g����ύX�E�J�[�h���Z�b�g�̌Ăяo��
    public void RePosition(Card card)
    {
        card.transform.SetParent(this.transform);
        ResetPosition();
    }

    //���X�g�͈̔͂��Ƃ��ăJ�[�h���ꂢ�ɕ��ׂ�
    public void ResetPosition()
    {
        list.Sort((card0,card1) => card0.Base.Number - card1.Base.Number);
        for (int i = 0; i < list.Count; i++)
        {
            if(list.Count % 2 == 0)
            {
                float posX = (i - list.Count / 2) * cardInterval + cardInterval / 2;
                list[i].transform.localPosition = new Vector3(posX, 0);
            }
            else
            {
                float posX = (i - list.Count / 2) * cardInterval;
                list[i].transform.localPosition = new Vector3(posX, 0);
            }
            Transform childTransform = List[i].transform.GetChild(0);
            Canvas canvas = childTransform.GetComponent<Canvas>();
            canvas.sortingOrder = i;

        }
    }


}

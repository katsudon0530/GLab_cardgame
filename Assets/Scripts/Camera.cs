using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAAA : MonoBehaviour
{
    [SerializeField] Camera cameraWH;

    public float targetAspectRatio = 16f / 9f; // �Œ肷��A�X�y�N�g��i��: 16:9�j

    void Update()
    {
        SetFixedAspectRatio();
    }

    void SetFixedAspectRatio()
    {
        // ���݂̉�ʃA�X�y�N�g��
        float windowAspect = (float)Screen.width / Screen.height;

        // �^�[�Q�b�g�̃A�X�y�N�g��Ɋ�Â��X�P�[��
        float scaleHeight = windowAspect / targetAspectRatio;

        // �J������Viewport��ύX
        if (scaleHeight < 1.0f)
        {
            Rect rect = cameraWH.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            cameraWH.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = cameraWH.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            cameraWH.rect = rect;
        }
    }
}
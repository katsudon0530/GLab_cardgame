using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAAA : MonoBehaviour
{
    [SerializeField] Camera cameraWH;

    public float targetAspectRatio = 16f / 9f; // 固定するアスペクト比（例: 16:9）

    void Update()
    {
        SetFixedAspectRatio();
    }

    void SetFixedAspectRatio()
    {
        // 現在の画面アスペクト比
        float windowAspect = (float)Screen.width / Screen.height;

        // ターゲットのアスペクト比に基づくスケール
        float scaleHeight = windowAspect / targetAspectRatio;

        // カメラのViewportを変更
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
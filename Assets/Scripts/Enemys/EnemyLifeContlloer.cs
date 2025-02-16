using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeContlloer : MonoBehaviour
{
    [SerializeField] GameObject life;

    public void lifeReflection(Enemy enemy)
    {
        RectTransform rectTransform = life.GetComponent<RectTransform>();
        Vector2 size = rectTransform.sizeDelta;
        float proportion = (float)enemy.Base.EnemyLife / (float)enemy.Base.EnemyLifeMax;
        size.x = 300 * proportion;
        rectTransform.sizeDelta = size;
    }
}

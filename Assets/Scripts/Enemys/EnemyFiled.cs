using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFiled : MonoBehaviour
{
    public void AddEnemy(Enemy enemy)
    {
        enemy.transform.SetParent(this.transform);
        ResetEnemy(enemy);
    }

    public void ResetEnemy(Enemy enemy)
    {
        enemy.transform.localPosition = new Vector3(0, 0, 0);
    }
}

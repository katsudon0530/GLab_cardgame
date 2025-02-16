using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] Enemy enemyPrefab;
    [SerializeField] EnemyBase[] enemyBases;


    public Enemy SpawnEnemy(int number)
    {
        Enemy enemy = Instantiate(enemyPrefab);
        enemy.SetEnemy(enemyBases[number]);
        return enemy;
    }
}

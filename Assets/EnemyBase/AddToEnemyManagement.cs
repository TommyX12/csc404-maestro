using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToEnemyManagement : MonoBehaviour
{
    private void Start()
    {
        EnemyManagementSystem.current.AddEnemy(gameObject);
    }

    private void OnDestroy()
    {
        EnemyManagementSystem.current.RemoveEnemy(gameObject);
    }
}

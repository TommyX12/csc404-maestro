using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// actually this is fine for now.
public class EnemyManagementSystem : MonoBehaviour
{
    public static EnemyManagementSystem current;

    public List<GameObject> enemies;
    int idx = 0;

    private void Awake()
    {
        current = this;
    }

    public GameObject GetNextEnemy() {
        if (enemies.Count > 0) { 
            idx = (idx + 1) % enemies.Count;
            return enemies[idx];
        }
        return null;
    }

    public void AddEnemy(GameObject enemy) {
        enemies.Add(enemy);
    }

    public void RemoveEnemy(GameObject enemy) {
        enemies.Remove(enemy);
    }

}

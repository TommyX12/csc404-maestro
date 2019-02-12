using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyGroup : MonoBehaviour
{
    public UnityEvent OnAllDied;

    public List<GameObject> obj;

    private void Update()
    {
        List<GameObject> obj2 = new List<GameObject>();
        foreach (GameObject o in obj) {
            if (o) {
                obj2.Add(o);
            }
        }
        obj = obj2;
        if (obj.Count == 0) {
            OnAllDied.Invoke();
            Destroy(this);
        }
    }
}

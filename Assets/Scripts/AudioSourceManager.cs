using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager : MonoBehaviour
{
    public Dictionary<string, ObjectPool<PoolableAudioSource>> pools = new Dictionary<string, ObjectPool<PoolableAudioSource>>();

    public static AudioSourceManager current;
    private void Awake()
    {
        current = this;
    }
    public PoolableAudioSource SpawnAudioSource(PoolableAudioSource obj) {
        if (!pools.ContainsKey(obj.name))
        {
            pools.Add(obj.name, new ObjectPool<PoolableAudioSource>(obj.CreateNew, 100, ReleaseDelegate, RequestDelegate));
        }
        return pools[obj.name].Request();
    }
    public void KillAudioSource(PoolableAudioSource obj)
    {
        if (!pools.ContainsKey(obj.name))
        {
            Debug.LogError("No live source for: " + obj.name);
            return;
        }
        pools[obj.name].Release(obj);
    }

    public void RequestDelegate(PoolableAudioSource obj)
    {
        obj.gameObject.SetActive(true);
    }

    public void ReleaseDelegate(PoolableAudioSource obj)
    {
        obj.gameObject.SetActive(false);
    }
}

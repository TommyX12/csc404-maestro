using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableAudioSource : MonoBehaviour, ObjectPoolable<PoolableAudioSource>
{

    int poolID;
    public AudioSource source;


    public PoolableAudioSource CreateNew()
    {
        PoolableAudioSource component = Instantiate(this);
        component.GetComponent<AudioSource>().playOnAwake = false;
        component.gameObject.SetActive(false);
        component.name = this.name;
        component.gameObject.transform.SetParent(AudioSourceManager.current.transform);
        return component;
    }

    public int GetPoolID()
    {
        return poolID;
    }

    public void SetPoolID(int id)
    {
        this.poolID = id;
    }

    public IEnumerator Play() {
        source.Play();
        yield return new WaitForSeconds(source.clip.length);
        AudioSourceManager.current.KillAudioSource(this);
    }

}

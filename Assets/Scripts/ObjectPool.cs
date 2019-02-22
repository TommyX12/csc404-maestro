using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// for now only growing arrays
public class ObjectPool<T> where T : ObjectPoolable<T>
{
    // Create Object Delegate
    // On Object Requested Delegate
    // On Object Released Delegate

    public delegate T ObjectPoolObjectConstructor();
    public delegate void OnObjectRequested(T obj);
    public delegate void OnObjectReleased(T obj);

    private T[] pool;
    private int poolSize;

    private int free;
    private int firstFree;
    private int lastFree;

    private int minSize = 0;
    private int maxSize = int.MaxValue;

    OnObjectReleased releaseDelegate;
    OnObjectRequested requestDelegate;
    ObjectPoolObjectConstructor constructorDelegate;

    public ObjectPool(ObjectPoolObjectConstructor constructorDelegate, int size = 100, OnObjectReleased releaseDelegate = null, OnObjectRequested requestDelegate = null) {
        this.constructorDelegate = constructorDelegate;
        this.releaseDelegate = releaseDelegate;
        this.requestDelegate = requestDelegate;

        pool = new T[size];
        for (int i = 0; i < size; i++) {
            pool[i] = constructorDelegate.Invoke();
            pool[i].SetPoolID(i);
        }

        poolSize = pool.Length;
        free = pool.Length;
        firstFree = 0;
        lastFree = pool.Length - 1;
    }

    public T Request() {
        if (free < 1)
        {
            // resize
            Resize(poolSize * 2);
            T obj = pool[firstFree];
            firstFree = (firstFree + 1) % poolSize;
            free--;
            requestDelegate(obj);
            return obj;
        }
        else {
            T obj = pool[firstFree];
            firstFree = (firstFree + 1) % poolSize;
            free--;
            requestDelegate(obj);
            return obj;
        }
    }

    public void Release(T obj) {
        if (free >= poolSize) {
            throw new System.Exception("Freeing to a full pool");
        }

        int nextSpot = (lastFree + 1) % poolSize;
        T swap = pool[nextSpot];
        swap.SetPoolID(obj.GetPoolID());
        pool[obj.GetPoolID()] = swap;
        pool[nextSpot] = obj;
        obj.SetPoolID(nextSpot);
        lastFree = nextSpot;
        free++;
        if (releaseDelegate!=null) {
            releaseDelegate.Invoke(obj);
        }
    }

    private void Resize(int newSize) {
        if (newSize <= poolSize)
        {
            throw new NotImplementedException("downsizing pools not implemented");
        }
        else {
            T[] newPool = new T[newSize];
            Array.Copy(pool, newPool, poolSize);
            for (int i = poolSize; i < newSize; i++) {
                newPool[i] = constructorDelegate();
                newPool[i].SetPoolID(i);
            }
            pool = newPool;
            free = newSize - poolSize;
            firstFree = poolSize;
            lastFree = newSize - 1;
            poolSize = newSize;
        }
    }
}


public interface ObjectPoolable<T> {
    int GetPoolID();
    void SetPoolID(int id);
    T CreateNew();
}
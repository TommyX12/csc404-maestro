using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformSerializationExtensions
{
    [Serializable]
    public struct TransformInfo
    {
        public Vector3 position;
        public Vector3 scale;
        public Quaternion rotation;
    }

    public static TransformInfo StoreTransform(this Transform transform)
    {
        TransformInfo info;
        info.position = transform.position;
        info.rotation = transform.rotation;
        info.scale = transform.localScale;
        return info;
    }

    public static void LoadTransform(this Transform transform, TransformInfo toLoad)
    {
        transform.position = toLoad.position;
        transform.rotation = toLoad.rotation;
        transform.localScale = toLoad.scale;
    }
}
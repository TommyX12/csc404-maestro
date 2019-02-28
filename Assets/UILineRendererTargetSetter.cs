using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
public class UILineRendererTargetSetter : MonoBehaviour
{
    public List<GameObject> objects;
    public UILineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<UILineRenderer>();
    }

    [ExecuteAlways]
    private void Update()
    {
        List<Vector2> points = new List<Vector2> ();
        foreach(GameObject obj in objects) {
            points.Add(obj.GetComponent<RectTransform>().position);
        }
        lineRenderer.Points = points.ToArray();
        lineRenderer.SetAllDirty();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GridObject : MonoBehaviour
{
    // expects a pivot in the center

    public static int GridSize = 1;
    public Vector3Int GridPos;
    public Vector3Int Sizes = new Vector3Int(1, 1, 1);

#if UNITY_EDITOR
    protected void PlaceInGrid()
    {
        GridPos = GetGridPosition();
        transform.localPosition = GetPosition();
    }
#endif

    public void Update()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying && (!Scrubber.instance || !Scrubber.instance.replay))
        {
            PlaceInGrid();
        }
#endif
    }

    public Vector3Int GetGridPosition() {
        Vector3 vec = transform.localPosition;
        Vector3 displ = (Sizes - new Vector3(1, 1, 1)) * GridSize;
        vec += GetPivotDisplacement() - GetSizeDisplacement();
        return new Vector3Int(Mathf.FloorToInt(vec.x / GridSize), Mathf.FloorToInt(vec.y / GridSize), Mathf.FloorToInt(vec.z / GridSize));
    }

    public Vector3 GetSizePivotDisplacement() {
        return GetSizeDisplacement() + GetPivotDisplacement();
    }

    public Vector3 GetPivotDisplacement() {
        return new Vector3(Sizes.x % 2 == 0 ? GridSize / 2f : 0, Sizes.y % 2 == 0 ? GridSize / 2f : 0, Sizes.z % 2 == 0 ? GridSize / 2f : 0);
    } 

    public Vector3 GetSizeDisplacement() {
        return (Sizes - new Vector3(1, 1, 1)) * GridSize;
    }

    public Vector3 GetPosition() {
        return GetGridPosition() * GridSize + GetSizeDisplacement() - GetPivotDisplacement();
    }

}

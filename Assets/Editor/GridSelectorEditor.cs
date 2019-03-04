using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridSelector))]
public class GridSelectorEditor : Editor
{
    public Texture2D onA;
    public Texture2D offA;
    public Texture2D onB;
    public Texture2D offB;

    public static int TILE_WIDTH = 15;
    public static int TILE_HEIGHT = 15;

    public Vector2 scrollPosition = Vector2.zero;
    public List<int> selectedGridIndices = new List<int>();
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        // draw the shit
        GridSelector sequencer = (GridSelector)target;
        GUIStyle style = GUI.skin.scrollView;
        style.padding = new RectOffset(5, 5, 5, 5);        
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Width((sequencer.width+1)*TILE_WIDTH), GUILayout.Height((sequencer.height+1)*TILE_HEIGHT));
        bool changed = false;
        bool[] grid = GridDisplay(new Rect(0, 0, sequencer.width * TILE_WIDTH, sequencer.height * TILE_HEIGHT), 
            sequencer.width, sequencer.height, sequencer.gridData, out changed);
        if (changed) {
            sequencer.gridData = grid;
        }

        EditorGUILayout.EndScrollView();
    }

    public bool[] GridDisplay(Rect rect, int gridWidth, int gridHeight, bool[] grid, out bool changed) {
        changed = false;

        if (grid==null) {
            grid = new bool[gridWidth * gridHeight];
            changed = true;
        }

        int controlID = GUIUtility.GetControlID(FocusType.Passive);

        if (grid.Length != gridWidth * gridHeight) {
            grid = new bool[gridWidth * gridHeight];
            changed = true;
        }

        switch (Event.current.GetTypeForControl(controlID))
        {
            case EventType.Repaint:

                GUI.color = Color.black;
                GUI.DrawTexture(rect, GUI.skin.box.normal.background);
                GUI.color = Color.white;

                for (int i = 0; i < gridWidth; i++) {
                    for (int j = 0; j < gridHeight; j++) {
                        Rect gridSquare = new Rect(rect.x + i * TILE_WIDTH, rect.y + j * TILE_HEIGHT, TILE_WIDTH, TILE_HEIGHT);
                        GUI.DrawTexture(gridSquare, grid[j * gridWidth + i] ? onA : offA);
                    }
                }

                // draw the smaller rects too
                break;
            case EventType.MouseDown:
                // if in my rect and moust zero
                if (rect.Contains(Event.current.mousePosition) && Event.current.button == 0)
                {
                    GUIUtility.hotControl = controlID;
                }
                selectedGridIndices.Clear();
                // check if we're also over any rects. Flip the value at that bool
                Vector2 pos = Event.current.mousePosition - rect.position;
                int x = (int) (pos.x / TILE_WIDTH);
                int y = (int) (pos.y / TILE_HEIGHT);
                int idx = y * gridWidth + x;
                if (!selectedGridIndices.Contains(idx)) {
                    selectedGridIndices.Add(idx);
                    grid[y * gridWidth + x] = !grid[y * gridWidth + x];
                }
                changed = true;
                break;
            case EventType.MouseUp:
                if (GUIUtility.hotControl == controlID && Event.current.button == 0)
                {
                    GUIUtility.hotControl = 0;
                }
                break;
        }

        if (GUIUtility.hotControl == controlID)
        {

            if (Event.current.isMouse && rect.Contains(Event.current.mousePosition))
            {
                // check if we're also over any rects. Flip the value at that bool
                Vector2 pos = Event.current.mousePosition - rect.position;
                int x = (int)(pos.x / TILE_WIDTH);
                int y = (int)(pos.y / TILE_WIDTH);
                int idx = y * gridWidth + x;
                if (!selectedGridIndices.Contains(idx))
                {
                    selectedGridIndices.Add(idx);
                    grid[y * gridWidth + x] = !grid[y * gridWidth + x];
                }
            }
            changed = true;
        }

        return grid;
    }
}

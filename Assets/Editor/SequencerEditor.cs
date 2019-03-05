using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(Sequencer))]
public class SequencerEditor : Editor
{
    public Texture2D onA;
    public Texture2D offA;
    public Texture2D onB;
    public Texture2D offB;

    public int TILE_WIDTH = 15;
    public int TILE_HEIGHT = 15;

    private Vector2 scrollPos;
    private List<int> changed = new List<int>();
    public int lastBeatDiv = 1;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (!Scrubber.instance || !Scrubber.instance.source || !Scrubber.instance.source.clip)
        {
            EditorGUILayout.LabelField("No active scrubber. Or scrubber is missing a valid audio source");
            return;
        }
        Sequencer sequence = (Sequencer)target;
        if (GUILayout.Button("Remake SequenceData") || sequence.data == null)
        {
            sequence.data = RemakeSequenceData(sequence.data, sequence.beatDiv);
        }

        Rect scrollRect = GUILayoutUtility.GetRect(Screen.width, 40);
        Rect viewRect = new Rect(0, 0, sequence.data.Length * TILE_WIDTH, 40);
        scrollPos = GUI.BeginScrollView(scrollRect, scrollPos, viewRect);
        sequence.data = MakeSequence(sequence.data);
        GUI.EndScrollView();
        Repaint();
    }

    private bool[] RemakeSequenceData(bool[] original, int beatDiv)
    {
        int beatNum = Mathf.RoundToInt((Scrubber.instance.source.clip.length * (Scrubber.instance.bpm / 60f))) * Mathf.RoundToInt(Mathf.Pow(2,beatDiv-1));
        bool[] data = new bool[beatNum];
        // copy over
        if (original != null)
        {
            for (int i = 0; i < original.Length && i < data.Length; i++)
            {
                data[i] = original[i];
            }
        }
        return data;
    }

    private bool[] MakeSequence(bool[] data) {
        Rect rect = new Rect(0, 0, data.Length*TILE_WIDTH, TILE_HEIGHT);

        int controlID = GUIUtility.GetControlID(FocusType.Passive);

        switch (Event.current.GetTypeForControl(controlID)) {
            case EventType.Repaint:
                Texture2D activeOn = onA;
                Texture2D activeOff = offA;
                for (int i = 0; i < data.Length; i++)
                {
                    if (i % 4 == 0) {
                        activeOn = activeOn == onA ? onB : onA;
                        activeOff = activeOff == offA ? offB : offA;
                    }
                    Rect tile = new Rect(i * TILE_WIDTH, 0, TILE_WIDTH, TILE_HEIGHT);
                    GUI.DrawTexture(tile, data[i] ? activeOn : activeOff);
                }
                // draw lead
                Rect lead = new Rect(data.Length * TILE_WIDTH * Scrubber.instance.source.time/Scrubber.instance.source.clip.length, 0, 1, TILE_HEIGHT);
                EditorGUI.DrawRect(lead, Color.black);
                break;
            case EventType.MouseDown:
                if (Event.current.button == 0 && rect.Contains(Event.current.mousePosition)) {
                    GUIUtility.hotControl = controlID;
                    changed.Clear();
                }
            break;
            case EventType.MouseUp:
                if (Event.current.button == 0 && GUIUtility.hotControl == controlID) {
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
                int idx = x;
                if (!changed.Contains(idx)) {
                    changed.Add(idx);
                    data[idx] = !data[idx];
                    var scene = SceneManager.GetActiveScene();
                    EditorSceneManager.MarkSceneDirty(scene);
                }
            }
        }
        return data;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(Scrubber))]
public class ScrubberEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Scrubber scrubber = (Scrubber)target;
        if (GUILayout.Button("Set As Master")) {
            scrubber.SetAsMaster();
        }
        if (GUILayout.Button("Play")) {
            scrubber.PlayMusic();
        }
        if (GUILayout.Button("Pause"))
        {
            scrubber.PauseMusic();
        }
        if (GUILayout.Button("Restart")) {
            scrubber.StopMusic();
        }
        if (scrubber.source != null)
        {
            AudioPreview(scrubber.source.clip, Screen.width, 100, Color.red);
            Rect lastRect = GUILayoutUtility.GetLastRect();
            float newTime;
            if (ScrubberLead(lastRect.position, scrubber.source, Screen.width, 100, out newTime)) {
                scrubber.source.time = newTime;
            }
        }
        Repaint();
    }

    public static Texture2D AudioWaveform(AudioClip aud, int width, int height, Color color)
    {
        int step = Mathf.CeilToInt((aud.samples * aud.channels) / width);
        float[] samples = new float[aud.samples * aud.channels];

        //getData after the loadType changed
        aud.GetData(samples, 0);
        Texture2D img = new Texture2D(width, height, TextureFormat.RGBA32, false);

        Color[] xy = new Color[width * height];
        for (int x = 0; x < width * height; x++)
        {
            xy[x] = new Color(0, 0, 0, 0);
        }

        img.SetPixels(xy);

        int i = 0;
        while (i < width)
        {
            int barHeight = Mathf.CeilToInt(Mathf.Clamp(Mathf.Abs(samples[i * step]) * height, 0, height));
            int add = samples[i * step] > 0 ? 1 : -1;
            for (int j = 0; j < barHeight; j++)
            {
                img.SetPixel(i, Mathf.FloorToInt(height / 2) - (Mathf.FloorToInt(barHeight / 2) * add) + (j * add), color);
            }
            ++i;

        }

        img.Apply();
        return img;
    }

    public static void AudioPreview(AudioClip clip, int width, int height, Color color) {
        RectOffset offset = new RectOffset(0, 0, 0, 0);
        GUIStyle style = new GUIStyle();
        style.contentOffset = Vector2.zero;
        style.padding = offset;
        style.normal = new GUIStyleState() { background = EditorGUIUtility.whiteTexture };
        GUILayout.Box(new GUIContent(AudioWaveform(clip, width, height, Color.red)), style, GUILayout.Width(width), GUILayout.Height(height));
    }

    // assumes width of texture is Screen.Width
    public static bool ScrubberLead(Vector2 clipDisplay, AudioSource audioSource, int displayWidth, int displayHeight, out float newTime, int leadWidth = 4) {
        newTime = audioSource.time;
        bool changed = false;

        // my rect
        Vector2 pos = clipDisplay - new Vector2(leadWidth/2,0);
        pos = pos + new Vector2(displayWidth * (audioSource.time) / audioSource.clip.length, 0);
        Rect rect = new Rect(pos, new Vector2(leadWidth, displayHeight));
        
        // control rect
        Rect controlRect = new Rect(clipDisplay, new Vector2(displayWidth, displayHeight));

        int controlID = GUIUtility.GetControlID(FocusType.Passive);
        switch (Event.current.GetTypeForControl(controlID)) {
            case EventType.Repaint:

                GUI.color = Color.black;
                GUI.DrawTexture(rect, GUI.skin.box.normal.background);
                GUI.color = Color.white;
            break;
            case EventType.MouseDown:
                // if in my rect and moust zero
                if (controlRect.Contains(Event.current.mousePosition)&&Event.current.button == 0) {
                    GUIUtility.hotControl = controlID;
                }
            break;
            case EventType.MouseUp:
                if (GUIUtility.hotControl == controlID && Event.current.button == 0) {
                    GUIUtility.hotControl = 0;
                }
                break;
        }

        if (GUIUtility.hotControl == controlID) {

            if (Event.current.isMouse)
            {
                float dx = Event.current.mousePosition.x - controlRect.position.x;
                dx = dx < 0 ? 0 : dx;
                newTime = audioSource.clip.length * (dx / displayWidth);
                GUI.changed = true;
                Event.current.Use();
            }
            changed = true;
        }

        return changed;
    }
}


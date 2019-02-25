using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BeatSequencer))]
public class BeatSequencerEditor : Editor
{

    Vector2 scrollPos;

    private const int BOX_WIDTH  = 10;
    private const int BOX_HEIGHT = 10;

    private GUIStyle boxOnAlpha = null;
    private GUIStyle boxOffAlpha = null;
    private GUIStyle boxOnBeta = null;
    private GUIStyle boxOffBeta = null;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //initialization
        BeatSequencer beatSequencer = (BeatSequencer)target;
        if (boxOnAlpha == null) {
            boxOnAlpha = new GUIStyle();
            boxOnAlpha.fixedWidth = BOX_WIDTH;
            boxOnAlpha.fixedHeight = BOX_HEIGHT;
            boxOnAlpha.normal.background = beatSequencer.sequencerOnTextureA;

            boxOffAlpha = new GUIStyle(boxOnAlpha);
            boxOffAlpha.normal.background = beatSequencer.sequencerOffTextureA;

            boxOnBeta = new GUIStyle(boxOnAlpha);
            boxOnBeta.normal.background = beatSequencer.sequencerOnTextureB;

            boxOffBeta = new GUIStyle(boxOnAlpha);
            boxOffBeta.normal.background = beatSequencer.sequencerOffTextureB;
        }

        GUIStyle[] alphaStyles = { boxOnAlpha, boxOffAlpha };
        GUIStyle[] betaStyles = { boxOnBeta, boxOffBeta };


        bool[] tracks = beatSequencer.tracks;

        int beats = 0;

        if (beatSequencer.useClip)
        {
            // bpm calc
            beats = (int) ((beatSequencer.bpm/ 60f) * beatSequencer.clip.length);
        }
        else {
            beats = beatSequencer.beatNum;
        }


        if (tracks==null || tracks.Length != beats * beatSequencer.trackNum)
        {
            tracks = new bool[beatSequencer.trackNum * beats];
        }

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, true, false);
        for (int i = 0; i < beatSequencer.trackNum; i++) {
            EditorGUILayout.BeginHorizontal();
            GUIStyle[] activeStyles = alphaStyles;
            for (int j = 0; j < beats; j++) {
                if (j % 4 == 0)
                {
                    // EditorGUI.DrawRect(new Rect(j * BOX_WIDTH, 0, BOX_WIDTH * 4, beatSequencer.trackNum * BOX_HEIGHT), colors[coloridx]);
                    // coloridx = (coloridx + 1) % colors.Length;
                    activeStyles = activeStyles == alphaStyles ? betaStyles : alphaStyles;
                }
                GUIStyle style = tracks[i + j*beatSequencer.trackNum] ? activeStyles[0] : activeStyles[1];
                tracks[i + j * beatSequencer.trackNum] = EditorGUI.Toggle(new Rect(j * BOX_WIDTH, i * BOX_HEIGHT, BOX_WIDTH, BOX_HEIGHT), "", tracks[i + j * beatSequencer.trackNum], style);
            }
            EditorGUILayout.EndHorizontal();
        }


        float beatPos = 0;

        if (Application.isPlaying && MusicManager.Current!=null)
        {
            beatPos = MusicManager.Current.TimeToBeat(MusicManager.Current.GetTotalTimer());
        }

        EditorGUI.DrawRect(new Rect(beatPos*BOX_WIDTH, 0, 2, beatSequencer.trackNum*BOX_HEIGHT), Color.black);

        EditorGUILayout.EndScrollView();

        beatSequencer.tracks = tracks;
        Repaint();
    }
}

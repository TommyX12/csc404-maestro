using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using XNode;
using XNodeEditor;

[CustomNodeEditor(typeof(SequencerNode))]
public class SequencerNodeEditor : NodeEditor {

    public override void OnBodyGUI() {
        base.OnBodyGUI();

        // //initialization
        // BeatSequencer beatSequencer = (BeatSequencer)target;
        // if (boxOnAlpha == null) {
        //     boxOnAlpha = new GUIStyle();
        //     boxOnAlpha.fixedWidth = BOX_WIDTH;
        //     boxOnAlpha.fixedHeight = BOX_HEIGHT;
        //     boxOnAlpha.normal.background = beatSequencer.sequencerOnTextureA;

        //     boxOffAlpha = new GUIStyle(boxOnAlpha);
        //     boxOffAlpha.normal.background = beatSequencer.sequencerOffTextureA;

        //     boxOnBeta = new GUIStyle(boxOnAlpha);
        //     boxOnBeta.normal.background = beatSequencer.sequencerOnTextureB;

        //     boxOffBeta = new GUIStyle(boxOnAlpha);
        //     boxOffBeta.normal.background = beatSequencer.sequencerOffTextureB;
        // }

        // GUIStyle[] alphaStyles = { boxOnAlpha, boxOffAlpha };
        // GUIStyle[] betaStyles = { boxOnBeta, boxOffBeta };


        // bool[] tracks = beatSequencer.tracks;

        // int beats = 0;

        // if (beatSequencer.useClip)
        // {
        //     // bpm calc
        //     beats = (int)((beatSequencer.bpm / 60f) * beatSequencer.clip.length);
        //     beatSequencer.beatNum = beats;
        // }
        // else {
        //     beats = beatSequencer.beatNum;
        // }


        // if (tracks == null || tracks.Length != beats * beatSequencer.trackNum)
        // {
        //     tracks = new bool[beatSequencer.trackNum * beats];
        // }

        // scrollPos = EditorGUILayout.BeginScrollView(scrollPos, true, false);
        // for (int i = 0; i < beatSequencer.trackNum; i++) {
        //     EditorGUILayout.BeginHorizontal();
        //     GUIStyle[] activeStyles = alphaStyles;
        //     for (int j = 0; j < beats; j++) {
        //         if (j % 4 == 0)
        //         {
        //             // EditorGUI.DrawRect(new Rect(j * BOX_WIDTH, 0, BOX_WIDTH * 4, beatSequencer.trackNum * BOX_HEIGHT), colors[coloridx]);
        //             // coloridx = (coloridx + 1) % colors.Length;
        //             activeStyles = activeStyles == alphaStyles ? betaStyles : alphaStyles;
        //         }
        //         GUIStyle style = tracks[i*beats + j] ? activeStyles[0] : activeStyles[1];
        //         tracks[i * beats + j] = EditorGUI.Toggle(new Rect(j * BOX_WIDTH, i * BOX_HEIGHT, BOX_WIDTH, BOX_HEIGHT), "", tracks[i * beats + j], style);
        //     }
        //     EditorGUILayout.EndHorizontal();
        // }


        // float beatPos = 0;

        // if (Application.isPlaying && MusicManager.current != null)
        // {
        //     beatPos = MusicManager.current.TimeToBeat(MusicManager.current.GetTotalTimer());
        // }

        // EditorGUI.DrawRect(new Rect(beatPos * BOX_WIDTH, 0, 2, beatSequencer.trackNum * BOX_HEIGHT), Color.black);

        // EditorGUILayout.EndScrollView();

        // if (GUILayout.Button("Clear All", GUILayout.Width(100))) {
        //     for (int i = 0; i < tracks.Length; i++) {
        //         tracks[i] = false;
        //     }
        // }

        // beatSequencer.tracks = tracks;

        // Repaint();

    }

}

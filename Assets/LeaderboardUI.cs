﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LeaderboardUI : MonoBehaviour
{
    // Injected references
    private GameplayModel model;
    private GlobalConfiguration config;

    [Inject]
    public void Construct(GameplayModel model,
                          GlobalConfiguration config)
    {
        this.model = model;
        this.config = config;
    }

    private class ScoreEntryComparer : IComparer<ScoreEntry>
    {
        public int Compare(ScoreEntry x, ScoreEntry y)
        {
            return x.score < y.score ? 1 : -1;
        }
    }

    public GameObject leaderBoard;
    public GameObject scoreEntry;

    public List<Text> topTen;
    public List<Text> topTenNames;
    public List<Image> topTenBackgrounds;

    public Text playerNameText;
    public Text playerScoreText;

    public Image playerScoreBackground;

    public Text scoreEntryPlayerScoreText;
    public InputField scoreEntryPlayerName;

    private void Start()
    {
        scoreEntryPlayerName.onEndEdit.AddListener(delegate { OnEndEdit(scoreEntryPlayerName); });
        scoreEntryPlayerName.onValidateInput += delegate (string input, int charIndex, char addedChar) { return MyValidate(addedChar); };
    }

    void OnEndEdit(InputField input) {
        if (Input.GetKey(KeyCode.Return)) {
            Submit();
        }
    }

    private char MyValidate(char charToValidate)
    {
        //Checks if a dollar sign is entered....
        if (charToValidate == ' ' || scoreEntryPlayerName.text.Length > 7)
        {
            // ... if it is change it to an empty character.
            charToValidate = '\0';
        }
        return charToValidate;
    }

    private void Update()
    {
        var score = model.Score;
        scoreEntryPlayerScoreText.text = score.ToString();
    }

    struct ScoreEntry {
        public string name;
        public int score;
    }

    public void Submit() {
        scoreEntry.SetActive(false);
        leaderBoard.SetActive(true);
        // load the file
        string text;

        Debug.Log(Application.persistentDataPath);

        if (File.Exists(Path.Combine(Application.persistentDataPath, "leaderboard.txt")))
        {
            text = File.ReadAllText(Path.Combine(Application.persistentDataPath, "leaderboard.txt"), Encoding.UTF8);
        }
        else {
            text = "";
        }

        string[] lines = text.Split('\n');
        
        // ascending?
        List<ScoreEntry> scores = new List<ScoreEntry>();
        Debug.Log(lines.Length);
        for (int i = 0; i < lines.Length; i++) {
            if (lines[i].Length < 1) {
                continue;
            }

            string[] line = lines[i].Split(' ');

            string playerName = line[0];
            int playerScore = int.Parse(line[1]);

            ScoreEntry entry = new ScoreEntry{ score = playerScore, name = playerName };
            scores.Add(entry);
        }

        scores.Sort(new ScoreEntryComparer());

        int idx = 0;
        ScoreEntry newEntry = new ScoreEntry() { score = Mathf.RoundToInt(model.Score), name = scoreEntryPlayerName.text };
        for (int i = 0; i < scores.Count; i++) {
            if (newEntry.score > scores[i].score) {
                break;
            }
            idx++;
        }

        scores.Add(newEntry);
        scores.Sort(new ScoreEntryComparer());

        if (idx < 10)
        {
            this.topTenBackgrounds[idx].color = Color.green;
            playerScoreBackground.color = Color.clear;
            playerScoreText.text = "";
            playerNameText.text = "";
        }
        else {
            playerScoreText.text = newEntry.score.ToString();
            playerNameText.text = (idx + 1).ToString() + ". " + newEntry.name;
        }

        for (int i = 0; i < 10 && i < scores.Count; i++) {
            this.topTenNames[i].text = (i + 1).ToString() + ". " + scores[i].name;
            this.topTen[i].text = scores[i].score.ToString();
        }

        string write = "";

        for (int i = 0; i < scores.Count; i++) {
            write += scores[i].name + " " + scores[i].score.ToString();
            if (i < scores.Count - 1) {
                write += "\n";
            }
        }
        System.IO.File.WriteAllText(Path.Combine(Application.persistentDataPath, "leaderboard.txt"), write);
    }
}
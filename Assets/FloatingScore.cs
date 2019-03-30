using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingScore : MonoBehaviour
{
    public Gradient ColorCurve;
    public Text image;
    public float aliveTime = 1f;

    private float sineOffset;

    public void SetScore(int score) {
        image.text = score.ToString();
    }

    private void Start()
    {
        sineOffset = Random.Range(-100, 100);
    }

    private void Update()
    {
        image.color = ColorCurve.Evaluate(Mathf.Sin(sineOffset + Time.fixedTime)/2f + 0.5f);
        aliveTime-=Time.deltaTime;
        if (aliveTime <= 0) {
            Destroy(gameObject);
        }
    }
}

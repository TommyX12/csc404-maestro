using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CassetteScore : MonoBehaviour
{
    public Sprite starFilledSprite;
    public UIScoreDisplay cassetteScoreDisplay;
    public AudioSource starNoise;
    public SpriteRenderer[] stars;

    public float inputCooldown = 1f;

    public float rotSpeed = 5;

    private int starScore = 0;

    public enum CassetteScoreState {
        FRONT,
        TURNING,
        SCORING,
        BACK
    }

    public CassetteScoreState state = CassetteScoreState.FRONT;

    private void Start()
    {
        transform.localRotation = Quaternion.Euler(0, 180, 0);
    }

    public void Update()
    {

        switch (state)
        {
            case CassetteScoreState.TURNING:
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * rotSpeed);
                if (Quaternion.Angle(transform.localRotation, Quaternion.Euler(0, 0, 0)) < 1) {
                    transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    cassetteScoreDisplay.SetTarget(ScoreManager.current.score);
                    ScoreManager.current.scoreDisplay.SetTarget(0);
                    state = CassetteScoreState.SCORING;
                }
                break;
            case CassetteScoreState.SCORING:

                if (starScore < stars.Length && cassetteScoreDisplay.currentNumber > ScoreManager.current.starScoreThresholds[starScore]) {
                    starScore++;
                    starNoise.Play();                    
                    stars[starScore - 1].sprite = starFilledSprite;
                }

                if (cassetteScoreDisplay.currentNumber == ScoreManager.current.score) {
                    state = CassetteScoreState.BACK;
                }

                break;
            case CassetteScoreState.BACK:
                break;
        }

        inputCooldown -= Time.deltaTime;

        if (ControllerProxy.GetButtonDown("Fire1") && inputCooldown < 0)
        {
            switch (state)
            {
                case CassetteScoreState.FRONT:
                    state = CassetteScoreState.TURNING;
                    break;
                case CassetteScoreState.TURNING:
                    transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    state = CassetteScoreState.SCORING;
                    cassetteScoreDisplay.SetTarget(ScoreManager.current.score);
                    ScoreManager.current.scoreDisplay.SetTarget(0);
                    break;
                case CassetteScoreState.SCORING:

                    for (int i = 0; i < stars.Length; i++) {
                        if (ScoreManager.current.score > ScoreManager.current.starScoreThresholds[starScore]) {
                            starScore++;
                            stars[starScore - 1].sprite = starFilledSprite;
                        }
                    }

                    cassetteScoreDisplay.currentNumber = ScoreManager.current.score;
                    state = CassetteScoreState.BACK;
                    break;
                case CassetteScoreState.BACK:
                    // scene transition
                    SceneManager.LoadScene("MainMenu");
                    break;
            }
        }
    }
}

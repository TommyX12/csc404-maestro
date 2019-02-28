using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CassetteScore : MonoBehaviour
{
    public Text scoreText;
    public SpriteRenderer[] stars;
    public float rotSpeed = 5;

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
                if ((transform.rotation.eulerAngles - new Vector3(0, 0, 0)).magnitude < 0.1) {
                    transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    state = CassetteScoreState.SCORING;
                }
                break;
            case CassetteScoreState.SCORING:

                break;
            case CassetteScoreState.BACK:
                break;
        }

        if (ControllerProxy.GetButtonDown("Fire1"))
        {
            switch (state)
            {
                case CassetteScoreState.FRONT:
                    state = CassetteScoreState.TURNING;
                    break;
                case CassetteScoreState.TURNING:
                    transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    state = CassetteScoreState.SCORING;
                    break;
                case CassetteScoreState.SCORING:
                    state = CassetteScoreState.BACK;
                    break;
                case CassetteScoreState.BACK:
                    // scene transition
                    break;
            }
        }
    }
}

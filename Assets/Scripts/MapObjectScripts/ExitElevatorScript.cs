using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ExitElevatorScript : MonoBehaviour
{
    public string TargetLevel = "_null";
    public float ElevatorSpeed = 5;
    public float TransitionTime = 5;


    private bool Elevating;

    public void BeginTransition()
    {
        Elevating = true;
    }

    private void FixedUpdate()
    {
        if (Elevating)
        {
            TransitionTime -= Time.deltaTime;
            transform.position += Vector3.up * ElevatorSpeed * Time.fixedDeltaTime;
            if (TransitionTime <= 0)
            {
                Elevating = false;
                if (TargetLevel.Equals("_null"))
                {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#endif
                }
                else
                {
                    SceneManager.LoadScene(TargetLevel);
                }
            }
        }
    }


}

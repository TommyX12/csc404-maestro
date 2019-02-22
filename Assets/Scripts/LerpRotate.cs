using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpRotate : MonoBehaviour
{
    public AnimationCurve movementCurve;

    public bool continueFromLastRotation = true;

    [Range(1, 10)]
    public float moveTime = 1;
    private float timer = 0;

    public Vector3 targetRotation;
    private Vector3 startRotation;
    public void SetTargetRotation(Vector3 position)
    {
        if (continueFromLastRotation)
        {
            this.startRotation = targetRotation;
        }
        else {
            this.startRotation = transform.rotation.eulerAngles;
        }
        this.targetRotation = position;
        
        timer = 0;
    }

    private void FixedUpdate()
    {
        float progress = timer / moveTime;
        transform.rotation = Quaternion.Euler(startRotation + (targetRotation - startRotation) * movementCurve.Evaluate(progress));
        timer += Time.fixedDeltaTime;
        if (timer > moveTime)
        {
            timer = moveTime;
        }
    }
}

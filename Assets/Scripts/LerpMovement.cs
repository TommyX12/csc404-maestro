using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpMovement : MonoBehaviour
{
    public AnimationCurve movementCurve;

    [Range(1, 10)]
    public float moveTime = 1;
    public bool useLocalPosition = false;
    private float timer = 0;

    public Vector3 targetPosition;
    private Vector3 startPosition;

    public void SetTargetPosition(Vector3 position) {
        this.targetPosition = position;
        this.startPosition = useLocalPosition? transform.localPosition :  transform.position;
        timer = 0;
    }

    private void FixedUpdate()
    {
        float progress = timer / moveTime;

        if (useLocalPosition)
        {
            transform.localPosition = startPosition + (targetPosition - startPosition) * movementCurve.Evaluate(progress);
        }
        else
        {
            transform.position = startPosition + (targetPosition - startPosition) * movementCurve.Evaluate(progress);
        }

        timer += Time.fixedDeltaTime;
        if (timer > moveTime) {
            timer = moveTime;
        }
    }
}

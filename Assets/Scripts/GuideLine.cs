using System.Collections;
using System.Collections.Generic;

using Zenject;

using UnityEngine;
using UnityEngine.Assertions;

public class GuideLine : MonoBehaviour {

    private LineRenderer lineRenderer;

    // Injected references
    private GlobalConfiguration config;

    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
        Assert.IsNotNull(lineRenderer, "lineRenderer");
    }

    [Inject]
    public void Construct(GlobalConfiguration config) {
        this.config = config;
    }

    private void Update() {
        Vector2 textureOffset = lineRenderer.material.mainTextureOffset;
        textureOffset.x =
            (textureOffset.x
             - config.GuideLineAnimationSpeed * Time.deltaTime)
            % 1.0f;
        lineRenderer.material.mainTextureOffset = textureOffset;
    }

    public void SetStart(Vector3 position) {
        lineRenderer.SetPosition(0, position);
    }

    public void SetEnd(Vector3 position) {
        lineRenderer.SetPosition(1, position);
    }

}

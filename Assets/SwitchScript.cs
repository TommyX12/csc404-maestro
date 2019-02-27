using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SwitchScript : BasicAgent
{
    public UnityEvent onActivate;
    public GameObject rotationPivot;
    [Range(-90,90)]
    public float finalXAngle;
    private float startAngle;

    private bool scriptControlled = true;

    public SwitchScript() {

    }

    private new void Start()
    {
        base.Start();
        this.type = Agent.Type.ENEMY;
        AgentManager.current.AddAgent(this);
        startAngle = rotationPivot.transform.localRotation.eulerAngles.x;
        onDeath -= DestroySelf;
    }


    protected override void OnDeath()
    {
        base.OnDeath();
        GetComponent<Collider>().enabled = false;
        scriptControlled = false;
        onActivate.Invoke();
    }

    private void LateUpdate()
    {
        if (!scriptControlled) {
            return;
        }

        float delta = finalXAngle - startAngle;
        rotationPivot.transform.localRotation = Quaternion.Euler(0, 0, startAngle + delta * (1f - this.hitPoint / this.initialHitPoint));

    }

    public override void ReceiveEvent(Event.Damage damage)
    {
        base.ReceiveEvent(damage);
    }
}

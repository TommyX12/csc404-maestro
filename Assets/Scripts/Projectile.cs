using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public abstract class Projectile : MonoBehaviour {

    [Serializable]
    public struct SpawnParameters {
        public Vector3 position;
        public Vector3 direction;
        public Agent.Event.Damage damage;
        public Color color;
        public float speed;
        public float scale;
        public float distance;
        public Agent.Type bypassAgentType;

        public SpawnParameters WithTransform(Vector3 position, Vector3 direction) {
            this.position = position;
            this.direction = direction;
            return this;
        }

        public SpawnParameters WithBypassAgentType(Agent.Type type) {
            this.bypassAgentType = type;
            return this;
        }
    }
    
    public Projectile() {
        
    }

    public abstract void SetSpawnParameters(SpawnParameters param);
}

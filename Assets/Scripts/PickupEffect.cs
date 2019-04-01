using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[Serializable]
public class PickupEffect {

    public enum PickupEffectType {
        DOUBLE_SHOT,
        SPEED_BOOST,
        SHIELD,
        EXTRA_BEATS
    }

    public PickupEffectType effectType;

    public PickupEffect(PickupEffectType effectType) {
        this.effectType = effectType;
    }

    public static PickupEffect GetRandomPickupEffect() {
        return new PickupEffect
            (Util.GetRandomElement<PickupEffectType>
             (Enum.GetValues(typeof(PickupEffectType))));
    }
}

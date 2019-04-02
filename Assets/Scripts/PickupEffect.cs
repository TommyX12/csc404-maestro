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

    public class EffectFunctionData {
        public PlayerAgentController player;
    }

    private static Dictionary<PickupEffectType, Action<EffectFunctionData>> startEffectFunctions =
        new Dictionary<PickupEffectType, Action<EffectFunctionData>>() {
        {PickupEffectType.DOUBLE_SHOT, DoubleShotStartEffect},
        {PickupEffectType.SPEED_BOOST, SpeedBoostStartEffect},
        {PickupEffectType.SHIELD, ShieldStartEffect},
        {PickupEffectType.EXTRA_BEATS, ExtraBeatsStartEffect}
    };

    private static Dictionary<PickupEffectType, Action<EffectFunctionData>> endEffectFunctions =
        new Dictionary<PickupEffectType, Action<EffectFunctionData>>() {
        {PickupEffectType.DOUBLE_SHOT, DoubleShotEndEffect},
        {PickupEffectType.SPEED_BOOST, SpeedBoostEndEffect},
        {PickupEffectType.SHIELD, ShieldEndEffect},
        {PickupEffectType.EXTRA_BEATS, ExtraBeatsEndEffect}
    };

    public PickupEffectType effectType;

    public PickupEffect(PickupEffectType effectType) {
        this.effectType = effectType;
    }

    public void Start(EffectFunctionData data) {
        startEffectFunctions[effectType](data);
    }

    public void End(EffectFunctionData data) {
        endEffectFunctions[effectType](data);
    }

    public static PickupEffect GetRandomPickupEffect() {
        // // Testing single effects.
        // return new PickupEffect(PickupEffectType.SPEED_BOOST);

        return new PickupEffect
            (Util.GetRandomElement<PickupEffectType>
             (Enum.GetValues(typeof(PickupEffectType))));
    }

    public static void DoubleShotStartEffect(EffectFunctionData data) {
        data.player.SetDoubleShotEnabled(true);
    }

    public static void DoubleShotEndEffect(EffectFunctionData data) {
        data.player.SetDoubleShotEnabled(false);
    }

    public static void SpeedBoostStartEffect(EffectFunctionData data) {
        data.player.SetSpeedBoostEnabled(true);
    }

    public static void SpeedBoostEndEffect(EffectFunctionData data) {
        data.player.SetSpeedBoostEnabled(false);
    }

    public static void ShieldStartEffect(EffectFunctionData data) {
        Debug.Log("ShieldStartEffect called");
    }

    public static void ShieldEndEffect(EffectFunctionData data) {
        Debug.Log("ShieldEndEffect called");
    }

    public static void ExtraBeatsStartEffect(EffectFunctionData data) {
        Debug.Log("ExtraBeatsStartEffect called");
    }

    public static void ExtraBeatsEndEffect(EffectFunctionData data) {
        Debug.Log("ExtraBeatsEndEffect called");
    }

}

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
        public LevelConfiguration levelConfiguration;
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
        // Test single effect.
        // return new PickupEffect(PickupEffectType.EXTRA_BEATS);

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
        data.player.SetShieldEnabled(true);
    }

    public static void ShieldEndEffect(EffectFunctionData data) {
        data.player.SetShieldEnabled(false);
    }

    public static void ExtraBeatsStartEffect(EffectFunctionData data) {
        data.player.SetTempRiff(data.levelConfiguration.ExtraBeatsRiffNotes);
    }

    public static void ExtraBeatsEndEffect(EffectFunctionData data) {
        data.player.ResetToOldRiff();
    }

}

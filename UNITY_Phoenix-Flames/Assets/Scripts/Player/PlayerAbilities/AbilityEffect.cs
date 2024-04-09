using System;
using Player.PlayerAbilities;

[Serializable] public class AbilityEffect 
{
    public AbilityEnums.EffectType _effectType;
    public AbilityEnums.ValueType _valueType;
    public AbilityEnums.AlterationType _alterationType;
    public AbilityEnums.EffectTempo _effectTempo;
    public float tempoDelay;
    public AbilityEnums.EffectApplication _effectApplication;
    public float effectValue;
    public float alterationDuration;
    public float overtimeDuration;
    public float overtimeTickRate;
    public int overtimeMaxTick;
}

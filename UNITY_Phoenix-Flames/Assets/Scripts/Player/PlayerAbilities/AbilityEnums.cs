namespace Player.PlayerAbilities
{
    public class AbilityEnums
    {
        public enum AbilityState{Ready, OnUse, OnCooldown}
        
        public enum PressureType{Unique, Pressure}
        public enum TargetingType{Selected, OnSelf, RaycastDetection}
        public enum TargetType{Amical, Hostile, All}
        public enum RaycastType{FrontRay, FrontRectangle, FrontCone, AroundCircle}
        public enum RaycastOrigin{OnSelf, Selected, WorldPoint}
        public enum ObjectType{None, Apparition}
        public enum ObjectOrigin{OnSelf, Selected, WorldPoint}
        public enum EffectType{Amélioration, Diminution, Altération}
        public enum ValueType{Health, Speed, Damage}
        public enum AlterationType{Stun, Contain, Push}
        public enum EffectTempo{Now, Delayed}
        public enum EffectApplication{Once, OverTime}

    }
}
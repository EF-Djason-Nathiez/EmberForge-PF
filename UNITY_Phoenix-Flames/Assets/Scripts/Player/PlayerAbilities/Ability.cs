using System;
using Player.PlayerAbilities;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

[CreateAssetMenu(menuName = "Ability/Create Ability", fileName = "new ability", order = 0)]
public class Ability : ScriptableObject
{
    public Action InputStarted;
    public Action OnAbilityEnable;
    
    public string title;
    public int number;
    public string id; //number_title

    public AbilityEnums.AbilityState _abilityState;
    public AbilityEnums.PressureType _pressureType;
    public AbilityEnums.TargetingType _targetingType;
    public AbilityEnums.TargetType _targetType;
    public AbilityEnums.RaycastType _raycastType;
    public AbilityEnums.RaycastOrigin _raycastOrigin;
    public AbilityEnums.ObjectType _objectType;
    public AbilityEnums.ObjectOrigin _objectOrigin;
        
    public float pressureDuration;
    private float pressureTimer;
    public float castDuration;
    public float detectionLenght;
    public float detectionHeight;
    public float detectionWidth;
    public float detectionRadius;
    public GameObject objectPrefab;
    public Vector3 objectVelocity;
    [SerializeField] public AbilityEffect[] effects;
    public int chargeCount;
    public float cooldownDuration;

    public AbilitySolver solver;

    public void SetAbilityState(AbilityEnums.AbilityState state)
    {
        _abilityState = state;
        Debug.Log(_abilityState);
    }
    
    public void Initialize()
    {
        solver = new AbilitySolver(this);
        SetAbilityState(AbilityEnums.AbilityState.Ready);
        
        InputStarted += () =>
        {
            pressureTimer += Time.deltaTime;

            if (pressureTimer >= pressureDuration)
            {
                solver.StartAbility();
                //cancel reader
            }
        };
        
        OnAbilityEnable += () =>
        {
            if (_abilityState == AbilityEnums.AbilityState.Ready)
            {
                solver.StartAbility();
            }
        };
    }
}
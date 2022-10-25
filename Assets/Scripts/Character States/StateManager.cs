using UnityEngine;
using dnSR_Coding.Utilities;
using NaughtyAttributes;
using System.Collections.Generic;

namespace dnSR_Coding
{
    public enum StateType
    {
        Idle, Moving, Interaction, Attack, Death
    }

    ///<summary> StateManager description <summary>
    public class StateManager : MonoBehaviour, IDebuggable
    {
        // Keep this block at top
        #region Debug

        [Space(5), SerializeField] private bool _isDebuggable = true;
        public bool IsDebuggable => _isDebuggable;

        #endregion

        //[Title( "STATE SETTINGS", 12, "white" )]

        private CharacterState _defaultState;
        protected CharacterState _currentState;

        private readonly Dictionary<StateType, CharacterState> _states = new();

        public StateManager()
        {
            _states [ StateType.Idle ] = new CharacterState_Idle();
            _states [ StateType.Moving ] = new CharacterState_Move();
            //_states [ StateType.Interaction ] = new Character_InteractionState();
            //_states [ StateType.Attack ] = new Character_AttackState();
            //_states [ StateType.Death ] = new Character_DeathState();
        }

        void Start() => Init();
        void Init()
        {
            SetDefaultState();
        }

        protected virtual void Update() => ExecuteCurrentState();

        private void ExecuteCurrentState()
        {
            if ( GameManager.Instance.IsGamePaused() ) { return; }

            _currentState.Process( this );
        }

        private void SetDefaultState()
        {
            _defaultState = GetSpecificState( StateType.Idle );

            _currentState = _defaultState;
            _currentState.Enter( this );
        }

        protected void SwitchToAnotherState( CharacterState state )
        {
            if ( _currentState == state ) { return; }

            _currentState.Exit( this );

            _currentState = state;
            _currentState.Enter( this );
        }

        protected CharacterState GetCurrentState() => _currentState;
        protected CharacterState GetSpecificState( StateType stateType ) => _states[ stateType ];

        #region OnValidate

#if UNITY_EDITOR

        private void OnValidate()
        {

        }
#endif

        #endregion
    }
}
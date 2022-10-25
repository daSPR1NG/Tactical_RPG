using UnityEngine;

namespace dnSR_Coding
{
    [RequireComponent( typeof( CharacterController ), typeof( Rigidbody ) )]
    [DisallowMultipleComponent]
    public abstract class CharacterLocomotion : StateManager
    {
        [Space( 5 )]

        [Title( "LOCOMOTION SETTINGS", 12, "white" )]

        [SerializeField] protected float _walkSpeed; // Temporary, it will eventualy be replaced by a stat
        [SerializeField] protected float _sprintSpeed; // Temporary, it will eventualy be replaced by a stat

        protected CharacterController _controller;

        protected float _movementSpeed;
        protected Vector3 _currentLocation = Vector3.zero;
        protected Vector3 _movementDirection = Vector3.zero;

        #region Enable, Disable

        void OnEnable() { }

        void OnDisable() { }

        #endregion

        protected virtual void Awake() => Init();
        protected virtual void Init()
        {
            GetLinkedComponents();

            SetMovementSpeedValue( _walkSpeed );
        }
        protected virtual void GetLinkedComponents()
        {
            _controller = GetComponent<CharacterController>();
        }

        protected override void Update()
        {
            base.Update();
        }

        protected void TryToMoveController( CharacterController controller, Vector2 movement )
        {
            if ( !CanMove() ) { return; }

            _movementDirection = new Vector3( movement.x, 0, movement.y );

            if ( _movementDirection == Vector3.zero )
            {
                SwitchToAnotherState( GetSpecificState( StateType.Idle ) );
                return;
            }

            Vector3 newPosition = _currentLocation
                + _movementSpeed
                * Time.fixedDeltaTime
                * _movementDirection.normalized;

            controller.Move( newPosition );

            SwitchToAnotherState( GetSpecificState( StateType.Moving ) );
        }

        protected void SetMovementSpeedValue( float value )
        {
            if ( _movementSpeed == value ) { return; }

            _movementSpeed = value;
        }

        protected bool CanMove()
        {
            return !GameManager.Instance.IsGamePaused();
        }
        protected bool IsSprinting() => _movementSpeed == _sprintSpeed;

        #region OnValidate

#if UNITY_EDITOR

        protected virtual void OnValidate()
        {
            GetLinkedComponents();
        }
#endif

        #endregion
    }
}
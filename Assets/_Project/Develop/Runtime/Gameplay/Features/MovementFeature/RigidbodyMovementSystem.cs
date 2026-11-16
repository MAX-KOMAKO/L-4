using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature
{
    public class RigidbodyMovementSystem : IInitializableSystem, IUpdatableSystem
    {
        private const float MinDirectionSqrMagnitude = 0.01f;

        private ReactiveVariable<Vector3> _moveDirection;
        private ReactiveVariable<float> _moveSpeed;
        private Rigidbody _rigidbody;

        public void OnInit(Entity entity)
        {
            _moveDirection = entity.MoveDirection;
            _moveSpeed = entity.MoveSpeed;
            _rigidbody = entity.Rigidbody;
            
            if (_rigidbody != null)
                _rigidbody.useGravity = false;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_rigidbody == null)
                return;

            Vector3 direction = _moveDirection.Value;
            Vector3 velocity = Vector3.zero;

            if (direction.sqrMagnitude > MinDirectionSqrMagnitude)
                velocity = direction.normalized * _moveSpeed.Value;

            _rigidbody.velocity = velocity;
        }
    }
}
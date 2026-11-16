using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.CharacterControllerFeature
{
    public class CharacterControllerRotationSystem : IInitializableSystem, IUpdatableSystem
    {
        private const float MinDirectionSqrMagnitude = 0.01f;

        private Transform _transform;
        private ReactiveVariable<Vector3> _moveDirection;
        private ReactiveVariable<float> _rotationSpeed;

        public void OnInit(Entity entity)
        {
            _transform = entity.Transform;
            _moveDirection = entity.MoveDirection;
            _rotationSpeed = entity.RotationSpeed;
        }

        public void OnUpdate(float deltaTime)
        {
            Vector3 direction = _moveDirection.Value;

            if (direction.sqrMagnitude <= MinDirectionSqrMagnitude)
                return;

            Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);

            _transform.rotation = Quaternion.RotateTowards(
                _transform.rotation,
                targetRotation,
                _rotationSpeed.Value * deltaTime);
        }
    }
}
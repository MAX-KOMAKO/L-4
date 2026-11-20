using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature.Rigidbody;
using Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature.CharacterController;
using Assets._Project.Develop.Runtime.Gameplay.Common;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets._Project.Develop.Runtime.Gameplay.EntitiesCore
{
    public class EntitiesFactory
    {
        private const float MoveSpeedValue = 10f;
        private const float RotationSpeedValue = 500f;
        private const string TestEntityPath = "Entities/TestEntity";
        private readonly DIContainer _container;
        private readonly EntitiesLifeContext _entitiesLifeContext;
        private readonly MonoEntitiesFactory _monoEntitiesFactory;

        public EntitiesFactory(DIContainer container)
        {
            _container = container;
            _entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();
            _monoEntitiesFactory = _container.Resolve<MonoEntitiesFactory>();
        }

        public Entity CreateRigidbodyEntity(Vector3 position)
        {
            Entity entity = CreateEmpty();
            _monoEntitiesFactory.Create(entity, position, TestEntityPath, OnCreateRigidbodyEntity);
            entity
                .AddMoveDirection()
                .AddMoveSpeed(new ReactiveVariable<float>(MoveSpeedValue))
                .AddRotationSpeed(new ReactiveVariable<float>(RotationSpeedValue));
            entity.AddSystem(new RigidbodyMovementSystem());
            entity.AddSystem(new RigidbodyRotationSystem());
            _entitiesLifeContext.Add(entity);
            return entity;
        }

        public Entity CreateCharacterControllerEntity(Vector3 position)
        {
            Entity entity = CreateEmpty();
            _monoEntitiesFactory.Create(entity, position, TestEntityPath, OnCreateCharacterEntity);
            entity
                .AddMoveDirection()
                .AddMoveSpeed(new ReactiveVariable<float>(MoveSpeedValue))
                .AddRotationSpeed(new ReactiveVariable<float>(RotationSpeedValue))
                .AddComponent(new VerticalVelocity { Value = 0f });
            entity.AddSystem(new CharacterControllerMovementSystem());
            entity.AddSystem(new CharacterControllerRotationSystem());
            _entitiesLifeContext.Add(entity);
            return entity;
        }

        private void OnCreateRigidbodyEntity(MonoEntity monoEntity)
        {
            if (monoEntity.TryGetComponent(out UnityEngine.CharacterController controller))
                Object.DestroyImmediate(controller);
            if (monoEntity.TryGetComponent(out CharacterControllerEntityRegistrator registrator))
                Object.DestroyImmediate(registrator);
        }

        private void OnCreateCharacterEntity(MonoEntity monoEntity)
        {
            if (monoEntity.TryGetComponent(out UnityEngine.Rigidbody rb))
                Object.DestroyImmediate(rb);
            if (monoEntity.TryGetComponent(out RigidbodyEntityRegistrator registrator))
                Object.DestroyImmediate(registrator);
        }

        private Entity CreateEmpty() => new Entity();
    }
}
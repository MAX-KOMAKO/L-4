using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Common
{
    public class CharacterControllerEntityRegistrator : MonoEntityRegistrator
    {
        public override void Register(Entity entity)
        {
            CharacterController characterController = GetComponent<CharacterController>();
            
            if (characterController == null)
            {
                Debug.LogError($"CharacterController не найден на объекте {gameObject.name}");
                return;
            }

            entity.AddCharacterController(characterController);
        }
    }
}
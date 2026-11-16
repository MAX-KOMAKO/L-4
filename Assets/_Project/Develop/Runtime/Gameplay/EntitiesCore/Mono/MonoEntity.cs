using Assets._Project.Develop.Runtime.Gameplay.Common;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Mono
{
    public class MonoEntity : MonoBehaviour
    {
        public void Setup(Entity entity)
        {
            entity.AddComponent(new TransformComponent { Value = transform });

            MonoEntityRegistrator[] registrators = GetComponentsInChildren<MonoEntityRegistrator>();

            if (registrators != null)
                foreach (MonoEntityRegistrator registrator in registrators)
                    registrator.Register(entity);
        }

        public void Cleanup(Entity entity)
        {
        }
    }
}
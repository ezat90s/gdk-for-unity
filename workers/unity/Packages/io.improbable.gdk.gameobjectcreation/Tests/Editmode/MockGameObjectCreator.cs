using System;
using Improbable.Gdk.Core;
using Improbable.Gdk.Subscriptions;
using Unity.Entities;

namespace Improbable.Gdk.GameObjectCreation.EditmodeTests
{
    public class MockGameObjectCreator : IEntityGameObjectCreator
    {
        public ComponentType[] MinimumComponentTypes { get; } = { };

        public void OnEntityCreated(SpatialOSEntity entity, EntityGameObjectLinker linker)
        {
            throw new System.NotImplementedException();
        }

        public void OnEntityRemoved(EntityId entityId)
        {
            throw new System.NotImplementedException();
        }
    }
}

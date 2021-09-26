using System;
using System.Collections.Generic;
using System.Linq;
using Generated.Improbable.PlayerLifecycle;
using Generated.Improbable.Transform;
using Generated.Playground;
using Improbable.Gdk.Core;
using Improbable.Worker.Core;

namespace Playground
{
    public static class PlayerTemplate
    {
        private static readonly List<string> AllWorkerAttributes =
            new List<string> { SystemConfig.UnityGameLogic, SystemConfig.UnityClient };

        public static Entity CreatePlayerEntityTemplate(List<string> clientAttributeSet,
            Generated.Improbable.Vector3f position)
        {
            var clientAttribute = clientAttributeSet.First(attribute => attribute != SystemConfig.UnityClient);

            if (clientAttribute == null)
            {
                throw new InvalidOperationException(
                    "Expected an attribute that is not \"UnityClient\" but none was found.");
            }

            var transform =
                Transform.Component.CreateSchemaComponentData(new Location(),
                    new Quaternion { W = 1, X = 0, Y = 0, Z = 0 }, 0);
            var playerInput = PlayerInput.Component.CreateSchemaComponentData(0, 0, false);
            var prefab = Prefab.Component.CreateSchemaComponentData(ArchetypeConfig.CharacterArchetype);
            var archetype = ArchetypeComponent.Component.CreateSchemaComponentData(ArchetypeConfig.CharacterArchetype);
            var launcher = Launcher.Component.CreateSchemaComponentData(100, 0);
            var clientHeartbeat = PlayerHeartbeatClient.Component.CreateSchemaComponentData();
            var serverHeartbeat = PlayerHeartbeatServer.Component.CreateSchemaComponentData();
            var score = Score.Component.CreateSchemaComponentData(0);

            var entityBuilder = EntityBuilder.Begin()
                .AddPosition(0, 0, 0, SystemConfig.UnityGameLogic)
                .AddMetadata(ArchetypeConfig.CharacterArchetype, SystemConfig.UnityGameLogic)
                .SetPersistence(false)
                .SetReadAcl(AllWorkerAttributes)
                .SetEntityAclComponentWriteAccess(SystemConfig.UnityGameLogic)
                .AddComponent(transform, SystemConfig.UnityGameLogic)
                .AddComponent(playerInput, clientAttribute)
                .AddComponent(prefab, SystemConfig.UnityGameLogic)
                .AddComponent(archetype, SystemConfig.UnityGameLogic)
                .AddComponent(launcher, SystemConfig.UnityGameLogic)
                .AddComponent(clientHeartbeat, clientAttribute)
                .AddComponent(serverHeartbeat, SystemConfig.UnityGameLogic)
                .AddComponent(score, SystemConfig.UnityGameLogic);

            return entityBuilder.Build();
        }
    }
}

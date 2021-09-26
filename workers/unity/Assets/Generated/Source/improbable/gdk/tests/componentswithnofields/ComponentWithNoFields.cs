// ===========
// DO NOT EDIT - this file is automatically regenerated.
// ===========

using Unity.Entities;
using Improbable.Gdk.Core;

namespace Generated.Improbable.Gdk.Tests.ComponentsWithNoFields
{
    public partial class ComponentWithNoFields
    {
        public struct Component : IComponentData, ISpatialComponentData
        {
            public uint ComponentId => 1003;

            public BlittableBool DirtyBit { get; set; }

            public static global::Improbable.Worker.Core.ComponentData CreateSchemaComponentData(
        )
            {
                var schemaComponentData = new global::Improbable.Worker.Core.SchemaComponentData(1003);
                var obj = schemaComponentData.GetFields();
                return new global::Improbable.Worker.Core.ComponentData(schemaComponentData);
            }
        }

        public static class Serialization
        {
            public static void Serialize(Generated.Improbable.Gdk.Tests.ComponentsWithNoFields.ComponentWithNoFields.Component component, global::Improbable.Worker.Core.SchemaObject obj)
            {
            }

            public static Generated.Improbable.Gdk.Tests.ComponentsWithNoFields.ComponentWithNoFields.Component Deserialize(global::Improbable.Worker.Core.SchemaObject obj, global::Unity.Entities.World world)
            {
                var component = new Generated.Improbable.Gdk.Tests.ComponentsWithNoFields.ComponentWithNoFields.Component();

                return component;
            }

            public static Generated.Improbable.Gdk.Tests.ComponentsWithNoFields.ComponentWithNoFields.Update GetAndApplyUpdate(global::Improbable.Worker.Core.SchemaObject obj, ref Generated.Improbable.Gdk.Tests.ComponentsWithNoFields.ComponentWithNoFields.Component component)
            {
                var update = new Generated.Improbable.Gdk.Tests.ComponentsWithNoFields.ComponentWithNoFields.Update();
                return update;
            }
        }

        public struct Update : ISpatialComponentUpdate
        {
        }

        public struct ReceivedUpdates : IComponentData
        {
            internal uint handle;
            public global::System.Collections.Generic.List<Update> Updates
            {
                get => Generated.Improbable.Gdk.Tests.ComponentsWithNoFields.ComponentWithNoFields.ReferenceTypeProviders.UpdatesProvider.Get(handle);
            }
        }
    }
}

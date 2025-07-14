using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate.Execution.Configuration;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using HotChocolate.Types.Descriptors.Definitions;

namespace AuthorDemo;

public class JsonTypeModule : ITypeModule
{
    private readonly string _file;

    public JsonTypeModule(string file)
    {
        _file = file;
    }

    public event EventHandler<EventArgs>? TypesChanged;

    public async ValueTask<IReadOnlyCollection<ITypeSystemMember>> CreateTypesAsync(
        IDescriptorContext context,
        CancellationToken cancellationToken)
    {
        var types = new List<ITypeSystemMember>();

        await using var file = File.OpenRead(_file);
        using var json = await JsonDocument.ParseAsync(file, cancellationToken: cancellationToken);

        foreach (var type in json.RootElement.EnumerateArray())
        {
            var typeDefinition = new ObjectTypeDefinition(type.GetProperty("name").GetString()!);

            foreach (var field in type.GetProperty("fields").EnumerateArray())
            {
                typeDefinition.Fields.Add(
                    new ObjectFieldDefinition(
                        field.GetString()!,
                        type: TypeReference.Parse("String!"),
                        pureResolver: ctx => "foo"));
            }

            types.Add(
                type.GetProperty("extension").GetBoolean()
                    ? ObjectTypeExtension.CreateUnsafe(typeDefinition)
                    : ObjectType.CreateUnsafe(typeDefinition));
        }

        return types;
    }
}

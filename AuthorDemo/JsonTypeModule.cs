using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
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
            var name = type.GetProperty("name").GetString()!;
            var isExtension = type.GetProperty("extension").GetBoolean();

            var typeDefinition = new ObjectTypeDefinition(name);

            foreach (var field in type.GetProperty("fields").EnumerateArray())
            {
                string fieldName;
                string fieldType = "String";

                if (field.ValueKind == JsonValueKind.String)
                {
                    fieldName = field.GetString()!;
                }
                else
                {
                    fieldName = field.GetProperty("name").GetString()!;
                    if (field.TryGetProperty("type", out var typeProp))
                    {
                        fieldType = typeProp.GetString()!;
                    }
                }

                var fieldDefinition = new ObjectFieldDefinition(
                    fieldName,
                    type: TypeReference.Parse(fieldType),
                    pureResolver: (isExtension && name == "Query") ? ctx => new object() : ctx => "foo");

                typeDefinition.Fields.Add(fieldDefinition);
            }

            types.Add(
                isExtension
                    ? ObjectTypeExtension.CreateUnsafe(typeDefinition)
                    : ObjectType.CreateUnsafe(typeDefinition));
        }

        return types;
    }

    public async Task AddFieldAsync(string typeName, string fieldName, string fieldType = "String")
    {
        JsonArray root;
        if (File.Exists(_file))
        {
            var json = await File.ReadAllTextAsync(_file);
            root = JsonNode.Parse(json)?.AsArray() ?? new JsonArray();
        }
        else
        {
            root = new JsonArray();
        }

        var typeObj = root.OfType<JsonObject>().FirstOrDefault(t =>
            string.Equals(t["name"]?.ToString(), typeName, StringComparison.OrdinalIgnoreCase));

        if (typeObj is null)
        {
            typeObj = new JsonObject
            {
                ["name"] = typeName,
                ["fields"] = new JsonArray(),
                ["extension"] = false
            };
            root.Add(typeObj);
        }

        var fields = typeObj["fields"]?.AsArray() ?? new JsonArray();
        fields.Add(new JsonObject
        {
            ["name"] = fieldName,
            ["type"] = fieldType
        });
        typeObj["fields"] = fields;

        await File.WriteAllTextAsync(_file, root.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));

        TypesChanged?.Invoke(this, EventArgs.Empty);
    }
}

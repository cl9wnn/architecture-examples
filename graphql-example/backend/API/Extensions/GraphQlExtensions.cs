using System.Text;
using HotChocolate.Execution;
using Path = System.IO.Path;

namespace API.Extensions;

/// <summary>
/// Методы расширения для работы со схемой GraphQl
/// </summary>
public static class GraphQlSchemaExtensions
{
    /// <summary>
    /// Сохраняет актуальную схему GraphQl в заданную директорию, либо в директорию misc по умолчанию
    /// </summary>
    /// <param name="webApplication"></param>
    /// <param name="path"></param>
    public static async Task SaveGraphQlSchemaAsync(this WebApplication webApplication, string? path = null)
    {
        var executorProvider = webApplication.Services.GetService<IRequestExecutorProvider>();
        if (executorProvider != null)
        {
            var executor = await executorProvider.GetExecutorAsync();
            var schemaDirectory = path ?? Path.Combine(Directory.GetCurrentDirectory(), @"..\misc");
            if (Directory.Exists(schemaDirectory))
            {
                var newSchema = executor.Schema.ToString();
                var schemaFile = Path.Combine(schemaDirectory, "schema.graphql");
                if (!File.Exists(schemaFile) || newSchema != await File.ReadAllTextAsync(schemaFile, Encoding.UTF8))
                {
                    await File.WriteAllTextAsync(schemaFile, newSchema, Encoding.UTF8);
                }
            }
        }
    }
}
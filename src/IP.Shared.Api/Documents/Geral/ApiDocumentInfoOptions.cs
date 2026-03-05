namespace IP.Shared.Api.Documents.Geral;

internal class ApiDocumentInfoOptions
{
    public const string NameKey = "Api.Documentation";

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Contact Contact { get; set; } = new Contact();
    public List<Tags> Tags { get; set; } = [];
}

internal class Contact
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

internal class Tags { 
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
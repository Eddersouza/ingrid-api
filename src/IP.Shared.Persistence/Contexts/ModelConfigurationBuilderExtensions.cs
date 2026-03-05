namespace IP.Shared.Persistence.Contexts;

public static class ModelConfigurationBuilderExtensions
{
    private const int DEFAULT_MAX_LENGTH_TO_TEXT_COLUMN = 100;

    public static ModelConfigurationBuilder AddDefaultStringConfiguration(
        this ModelConfigurationBuilder builder)
    {
        builder
            .Properties<string>()
            .AreUnicode()
            .HaveMaxLength(DEFAULT_MAX_LENGTH_TO_TEXT_COLUMN);

        return builder;
    }
}
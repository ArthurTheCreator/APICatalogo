namespace APICatalogo.Logging;

public class CustomLoggerProviderConfiguration
{
    // Vai definir o nivel mini,o de log a ser registrado
    public LogLevel LogLevel { get; set; } = LogLevel.Warning;

    // Define o ID do evento de log
    public int EventId { get; set; } = 0;
}


namespace APICatalogo.Logging;

public class CustomerLogger : ILogger
{
    readonly string loggerName;
    readonly CustomLoggerProviderConfiguration loggerConfig;

    public CustomerLogger(string loggerName, CustomLoggerProviderConfiguration loggerConfig)
    {
        loggerName = loggerName;
        loggerConfig = loggerConfig;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel == loggerConfig.LogLevel;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        string mensagem = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";
        EscreverTexto(mensagem);
    }
    private void EscreverTexto (string mensagem)
    {
        string caminhoArquivolog = @"C:\Users\arthu_dzh116v\OneDrive\Área de Trabalho\log\arthurlog.txt";
        using (StreamWriter streamWriter = new StreamWriter(caminhoArquivolog, true))
        {
            try
            {
                streamWriter.WriteLine(mensagem);
                streamWriter.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

using System.IO;
using System;
using ApplicationCore.Interfaces;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Logging 
{
    public class LoggerAdapter<T> : IAppLogger<T>
    {
        private readonly ILogger<T> _logger;
        private static readonly string logFilePath = $"Logs/{DateTime.Now:yyyyMMdd}.log";
        public LoggerAdapter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<T>();
        }

        public void LogWarning(string message, params object[] args)
        {
            _logger.LogWarning(message, args);
            WriteToFile("WARNING", message, args);
        }

        public void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
            WriteToFile("INFO", message, args);
        }
        private void WriteToFile(string level, string message, params object[] args)
        {
            var logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {string.Format(message, args)}";
            Directory.CreateDirectory(Path.GetDirectoryName(logFilePath)); // 確保目錄存在
            File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
        }
    }
}



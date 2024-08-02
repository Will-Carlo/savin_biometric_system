using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_asistencia_savin.Notifications
{
    internal class LoggingManager
    {
        private static ILoggerFactory loggerFactory;

        //public static void InitializeLogger()
        //{
        //    // Configurar Serilog
        //    Log.Logger = new LoggerConfiguration()
        //        .MinimumLevel.Debug()
        //        .WriteTo.Console()
        //        .WriteTo.File("logs/savin.log", rollingInterval: RollingInterval.Day)
        //        .CreateLogger();

        //    // Configurar la fábrica de ILogger para usar Serilog
        //    loggerFactory = LoggerFactory.Create(builder =>
        //    {
        //        builder.AddSerilog();
        //    });
        //}
        static LoggingManager()
        {
            // Configurar Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/savin.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            // Configurar la fábrica de ILogger para usar Serilog
            loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddSerilog();
            });
        }

        public static Microsoft.Extensions.Logging.ILogger GetLogger<T>()
        {
            // Crear una instancia de ILogger desde la fábrica
            return loggerFactory.CreateLogger<T>();
        }

        public static void CloseAndFlush()
        {
            // Cerrar y vaciar el registro
            Log.CloseAndFlush();
        }


    }
}

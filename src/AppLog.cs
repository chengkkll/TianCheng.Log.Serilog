using Microsoft.Extensions.Configuration;
using Serilog;

namespace TianCheng.Log
{
    /// <summary>
    /// 读取配置信息的日志
    /// </summary>
    public class AppLog
    {
        /// <summary>
        /// 日志操作对象
        /// </summary>
        static private ILogger _Logger = null;
        /// <summary>
        /// 日志操作
        /// </summary>
        static public ILogger Logger
        {
            get
            {
                if (_Logger == null)
                {
                    InitLogger();
                }
                return _Logger;
            }
        }
        /// <summary>
        /// 默认的文件格式
        /// </summary>
        private static readonly string FileFormat = "Logs/app-{Date}.txt";

        /// <summary>
        /// 初始化日志
        /// </summary>
        static private void InitLogger()
        {
            try
            {
                var con = new LoggerConfiguration().ReadFrom.Configuration(BuildConfiguration());
                _Logger = con.CreateLogger();
            }
            catch
            {
                _Logger = new LoggerConfiguration()
                            .WriteTo.Console(Serilog.Events.LogEventLevel.Information)
                            .WriteTo.Debug()
                            .WriteTo.RollingFile(FileFormat)
                            .CreateLogger();
            }
        }

        /// <summary>
        /// 创建一份配置文件信息
        /// </summary>
        /// <returns></returns>
        public static IConfiguration BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }
    }
}

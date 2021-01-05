using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreapi.LogApiHandler
{
    internal class LogLevel
    {
        /// <summary>
        /// Trace log level.
        /// </summary>        
        public static readonly LogLevel Trace = new LogLevel("Trace", 0);

        /// <summary>
        /// Debug log level.
        /// </summary>        
        public static readonly LogLevel Debug = new LogLevel("Debug", 1);

        /// <summary>
        /// Info log level.
        /// </summary>        
        public static readonly LogLevel Info = new LogLevel("Info", 2);

        /// <summary>
        /// Warn log level.
        /// </summary>        
        public static readonly LogLevel Warn = new LogLevel("Warn", 3);

        /// <summary>
        /// Error log level.
        /// </summary>        
        public static readonly LogLevel Error = new LogLevel("Error", 4);

        /// <summary>
        /// Fatal log level.
        /// </summary>        
        public static readonly LogLevel Fatal = new LogLevel("Fatal", 5);
        private readonly int _ordinal;
        private readonly string _name;
        /// <summary>
        /// Initializes a new instance of <see cref="LogLevel"/>.
        /// </summary>
        /// <param name="name">The log level name.</param>
        /// <param name="ordinal">The log level ordinal number.</param>
        private LogLevel(string name, int ordinal)
        {
            _name = name;
            _ordinal = ordinal;
        }
        /// <summary>
        /// Gets the name of the log level.
        /// </summary>
        public string Name => _name;
        /// <summary>
        /// Gets the ordinal of the log level.
        /// </summary>
        public int Ordinal => _ordinal;
        /// <summary>
        /// 请求地址
        /// </summary>
        public string LogApi
        {
            get
            {
                switch (_name)
                {
                    case "Trace":
                        return "https://localhost:44389/api/log/trace";
                    case "Debug":
                        return "https://localhost:44389/api/log/debug";
                    case "Info":
                        return "https://localhost:44389/api/log/info";
                    case "Warn":
                        return "https://localhost:44389/api/log/warn";
                    case "Error":
                        return "https://localhost:44389/api/log/error";
                    case "Fatal":
                        return "https://localhost:44389/api/log/fatal";
                    default:
                        return "";
                }
            }
        }
        /// <summary>
        /// Returns the <see cref="T:NLog.LogLevel"/> that corresponds to the supplied <see langword="string" />.
        /// </summary>
        /// <param name="levelName">The textual representation of the log level.</param>
        /// <returns>The enumeration value.</returns>
        public static LogLevel FromString(string levelName)
        {
            if (levelName == null)
            {
                throw new ArgumentNullException(nameof(levelName));
            }

            if (levelName.Equals("Trace", StringComparison.OrdinalIgnoreCase))
            {
                return Trace;
            }

            if (levelName.Equals("Debug", StringComparison.OrdinalIgnoreCase))
            {
                return Debug;
            }

            if (levelName.Equals("Info", StringComparison.OrdinalIgnoreCase))
            {
                return Info;
            }

            if (levelName.Equals("Warn", StringComparison.OrdinalIgnoreCase))
            {
                return Warn;
            }

            if (levelName.Equals("Error", StringComparison.OrdinalIgnoreCase))
            {
                return Error;
            }

            if (levelName.Equals("Fatal", StringComparison.OrdinalIgnoreCase))
            {
                return Fatal;
            }

            throw new ArgumentException($"Unknown log level: {levelName}");
        }
    }
}

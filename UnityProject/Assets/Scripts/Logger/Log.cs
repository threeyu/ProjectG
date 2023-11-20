using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ppy {
    public interface ILogger {
        void Debug(params object[] args);
        void Info(params object[] args);
        void Warning(params object[] args);
        void Error(params object[] args);
    }

    public class DefaultLogger : ILogger {
        public char separator = ' ';

        public void Debug(params object[] args) {
            UnityEngine.Debug.Log(LogUtils.ArgsToString(args, separator));
        }

        public void Info(params object[] args) {
            UnityEngine.Debug.Log(LogUtils.ArgsToString(args, separator));
        }

        public void Warning(params object[] args) {
            UnityEngine.Debug.LogWarning(LogUtils.ArgsToString(args, separator));
        }

        public void Error(params object[] args) {
            UnityEngine.Debug.LogError(LogUtils.ArgsToString(args, separator));
        }
    }

    public class Log {
        public static class LogLevel {
            public static int Debug = 400;
            public static int Info = 300;
            public static int Warning = 200;
            public static int Error = 100;
        }

        public static char separator = ' ';
        public static ILogger logger = new DefaultLogger();
        public static int logLevel = LogLevel.Debug;

        public static void Debug(params object[] args) {
            if (logLevel >= LogLevel.Debug) {
                logger?.Debug(args);
            }
        }

        public static void Info(params object[] args) {
            if (logLevel >= LogLevel.Info) {
                logger?.Info(args);
            }
        }

        public static void Warning(params object[] args) {
            if (logLevel >= LogLevel.Warning) {
                logger?.Warning(args);
            }
        }

        public static void Error(params object[] args) {
            if (logLevel >= LogLevel.Error) {
                logger?.Error(args);
            }
        }
    }
}
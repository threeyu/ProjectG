using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ppy {
    public class LogUtils {
        public static List<string> logFilePath = new List<string>();

        public static string ArgsToString(object[] args, char separator) {
            var sb = new StringBuilder();
            for (var i = 0; i < args.Length; i++) {
                if (args[i] == null) {
                    sb.Append("(null)");
                }
                else {
                    sb.Append(args[i].ToString());
                }

                if (separator != 0) {
                    sb.Append(separator);
                }
            }

            return sb.ToString();
        }

        public static string Red(string log) {
            var sb = new StringBuilder();
            sb.Append("<color=red>");
            sb.Append(log);
            sb.Append("</color>");
            return sb.ToString();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Ogopogo {
    public class ExcelConvertTools {
        [MenuItem("Tools/ExternalTool/转表", false, 1)]
        private static void ConvertExcel() {
            var workDir = Application.dataPath + ResPath.ExcelToolFolder;
            var batPath = workDir + "convert.bat";
            var arg = "";
            SystemCommandCall.RunBat(batPath, arg, workDir);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Ogopogo {
    public class SignLuaCode {
        // // 签名工具所在目录
        // private static string mSignatureFolder = Application.dataPath + "/../../Tools/LuaTool/";
        // private static string mSignatureFile = mSignatureFolder + "FilesSignature.exe";
        // // lua源码所在目录
        // private static string mLuaRawFolder = Application.dataPath.Replace("Assets", "") + "Assets/LuaProj";
        // // 临时目录，签名后的lua源码目录
        // private static string mLuaTmpOutputFolder = mSignatureFolder + "LuaSignScript";
        // // 代码最终存储目录
        // private static string mLuaOutputFolder = Application.dataPath.Replace("Assets", "") + "Assets/LuaBytes";
        //
        // //[MenuItem("构建/构建 Lua", false)]
        // public static void CreateLuaCode() {
        //     // 清空临时目录
        //     ClearOutputFolder();
        //
        //     // 代码签名到临时目录
        //     SystemCommandCall.RunBat(mSignatureFile, mLuaRawFolder + " " + mLuaTmpOutputFolder, mSignatureFolder);
        //
        //     // 签名后的代码输出
        //     IOUtils.ReplaceFilesToBytes(mLuaTmpOutputFolder, mLuaOutputFolder, ".lua");
        //     AssetDatabase.Refresh();
        // }
        //
        // //[MenuItem("构建/清除 Lua 输出", false)]
        // public static void ClearOutputFolder() {
        //     if (Directory.Exists(mLuaTmpOutputFolder)) {
        //         Directory.Delete(mLuaTmpOutputFolder, true);
        //     }
        // }
    }
}

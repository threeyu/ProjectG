using System.Diagnostics;

public class SystemCommandCall {

    //public static void RunCmd(string cmdExe, string cmdStr = "")
    //{
    //    RunCmd2(cmdExe, cmdStr);
    //}

    public static bool RunCmd(string cmdExe, string cmdStr = "") {
        //UnityEngine.Debug.Log(string.Format("{0}", cmdExe + " " + cmdStr));

        bool result = false;
        try {
            using (Process myPro = new Process()) {
                //指定启动进程是调用的应用程序和命令行参数
                ProcessStartInfo psi = new ProcessStartInfo(cmdExe, cmdStr);

                psi.CreateNoWindow = true;
                psi.WindowStyle = ProcessWindowStyle.Hidden;
                // psi.Arguments = "/s /v /qn /min";
                // psi.UseShellExecute = false;

                myPro.StartInfo = psi;
                myPro.Start();
                myPro.WaitForExit();
                result = true;
            }
        } catch {

        }
        return result;
    }

    static void RunCmd2(string cmdExe, string cmdStr) {
        try {
            using (Process myPro = new Process()) {
                myPro.StartInfo.FileName = "cmd.exe";
                myPro.StartInfo.UseShellExecute = false;
                myPro.StartInfo.RedirectStandardInput = true;
                myPro.StartInfo.RedirectStandardOutput = true;
                myPro.StartInfo.RedirectStandardError = true;
                myPro.StartInfo.CreateNoWindow = true;
                myPro.Start();
                //如果调用程序路径中有空格时，cmd命令执行失败，可以用双引号括起来 ，在这里两个引号表示一个引号（转义）
                string str = string.Format("{0} {1} & exit", cmdExe, cmdStr);

                myPro.StandardInput.WriteLine(str);
                myPro.StandardInput.AutoFlush = true;
                myPro.WaitForExit();
            }
        } catch {

        }
    }

    public static System.Diagnostics.Process CreateShellExProcess(string cmd, string args, string workingDir = "") {
#if UNITY_EDITOR_OSX
        // 在命令行中输which mono查看路径，再复制到此处
	    string monoPath = "/Library/Frameworks/Mono.framework/Versions/Current/Commands/mono";
        var pStartInfo = new System.Diagnostics.ProcessStartInfo(monoPath);
        pStartInfo.Arguments = string.Format("{0} {1}", cmd, args);
#else
        var pStartInfo = new System.Diagnostics.ProcessStartInfo(cmd);
        pStartInfo.Arguments = args;
#endif
        pStartInfo.CreateNoWindow = false;
        pStartInfo.UseShellExecute = true;
        pStartInfo.RedirectStandardError = false;
        pStartInfo.RedirectStandardInput = false;
        pStartInfo.RedirectStandardOutput = false;
        if (!string.IsNullOrEmpty(workingDir))
            pStartInfo.WorkingDirectory = workingDir;
        return System.Diagnostics.Process.Start(pStartInfo);
    }

    public static void RunBat(string batfile, string args, string workingDir = "") {
        var p = CreateShellExProcess(batfile, args, workingDir);
        p.WaitForExit();
    }
}
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

namespace MW2_RCON
{
    public sealed class ErrorLog
    {
        private string _LogPath;

        public ErrorLog()
        {
            this._LogPath = AppDomain.CurrentDomain.BaseDirectory;
            if (!Directory.Exists(this._LogPath))
            {
                Directory.CreateDirectory(this._LogPath);
            }
        }

        public ErrorLog(string logPath)
        {
            this._LogPath = logPath;
            if (!Directory.Exists(this._LogPath))
            {
                Directory.CreateDirectory(this._LogPath);
            }
        }

        private string GetExceptionStack(Exception e)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(e.Message);
            while (e.InnerException != null)
            {
                e = e.InnerException;
                builder.Append(Environment.NewLine);
                builder.Append(e.Message);
            }
            return builder.ToString();
        }

        public string LogError(Exception exception, string Version)
        {
            Assembly entryAssembly = Assembly.GetEntryAssembly();
            Process currentProcess = Process.GetCurrentProcess();
            string str = DateTime.Now.ToString("yyyy-MM-dd_HH.mm.ss") + ".log";
            using (StreamWriter writer = new StreamWriter(Path.Combine(this._LogPath, str)))
            {
                writer.WriteLine("==============================================================================");
                writer.WriteLine(entryAssembly.FullName);
                writer.WriteLine("BUILD        : {0}", Version);
                writer.WriteLine("------------------------------------------------------------------------------");
                writer.WriteLine("Application Information");
                writer.WriteLine("------------------------------------------------------------------------------");
                writer.WriteLine("Program      : " + entryAssembly.Location);
                writer.WriteLine("Time         : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                writer.WriteLine("User         : " + Environment.UserName);
                writer.WriteLine("Computer     : " + Environment.MachineName);
                writer.WriteLine("OS           : " + Environment.OSVersion.ToString());
                writer.WriteLine("Culture      : " + CultureInfo.CurrentCulture.Name);
                writer.WriteLine("Processors   : " + Environment.ProcessorCount);
                writer.WriteLine("Working Set  : " + Environment.WorkingSet);
                writer.WriteLine("Framework    : " + Environment.Version);
                writer.WriteLine("Run Time     : " + ((TimeSpan)(DateTime.Now - Process.GetCurrentProcess().StartTime)).ToString());
                writer.WriteLine("------------------------------------------------------------------------------");
                writer.WriteLine("Exception Information");
                writer.WriteLine("------------------------------------------------------------------------------");
                writer.WriteLine("Source       : " + exception.Source.ToString().Trim());
                writer.WriteLine("Method       : " + exception.TargetSite.Name.ToString());
                writer.WriteLine("Type         : " + exception.GetType().ToString());
                writer.WriteLine("Error        : " + this.GetExceptionStack(exception));
                writer.WriteLine("Stack Trace  : " + exception.StackTrace.ToString().Trim());
                writer.WriteLine("Line         : " + new StackTrace(exception, true).GetFrame(0).GetFileLineNumber());
                writer.WriteLine("Stack Trace  : " + new StackTrace(exception, true).GetFrame(0));
                writer.WriteLine("------------------------------------------------------------------------------");
                writer.WriteLine("Loaded Modules");
                writer.WriteLine("------------------------------------------------------------------------------");
                foreach (ProcessModule module in currentProcess.Modules)
                {
                    try
                    {
                        writer.WriteLine(string.Concat(new object[] { module.FileName, " | ", module.FileVersionInfo.FileVersion, " | ", module.ModuleMemorySize }));
                    }
                    catch (FileNotFoundException)
                    {
                        writer.WriteLine("File Not Found: " + module.ToString());
                    }
                    catch (Exception)
                    {
                    }
                }
                writer.WriteLine("------------------------------------------------------------------------------");
                writer.WriteLine(str);
                writer.WriteLine("==============================================================================");
            }
            return str;
        }

        public string LogPath
        {
            get
            {
                return this._LogPath;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Carnac.Logic;

namespace Carnac.Logger
{
    public class FileSystemLogger : AbstractLogger, IDisposable
    {
        private FileStream _File;
        private StreamWriter _Writer;

        public FileSystemLogger(string fileName)
        {
            _File= File.Open(fileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            _Writer = new StreamWriter(_File);
        }

        public override void Log(KeyPress value, TimeSpan interval)
        {
            _Writer.WriteLine(value.InterceptKeyEventArgs.Key.Sanitise() + "," + interval.ToString());
        }

        public override void OnCompleted()
        {
            CloseStream();
        }

        public override void Dispose()
        {
            base.Dispose();
            CloseStream();
        }

        private void CloseStream()
        {
            if (_Writer != null)
            {
                try
                {
                    _Writer.Flush();
                    _Writer.Close();
                    _Writer.Dispose();
                    _Writer = null;
                }
                catch { }
            }
            if (_File != null)
            {
                try
                {
                    _File.Flush();
                    _File.Close();
                    _File.Dispose();
                    _File = null;
                }
                catch { }
            }
        }
    }
}

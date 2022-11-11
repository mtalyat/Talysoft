using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talysoft.Debugging
{
    /// <summary>
    /// Manages logging text.
    /// </summary>
    public class Logger
    {
        public string LogPrefix { get; set; } = "";
        public string LogWarningPrefix { get; set; } = "[WRN] ";
        public string LogErrorPrefix { get; set; } = "[ERR] ";

        public static Logger Active = null;

        private List<string> _lines = new List<string>();

        public Action<Logger> OnLog = null;

        public Logger()
        {
            if(Active == null)
            {
                Active = this;
            }
        }

        ~Logger()
        {
            if(this == Active)
            {
                Active = null;
            }
        }

        #region Logging

        public void LogRaw(object o) => LogRaw(o.ToString());

        public void LogRaw(string s)
        {
            _lines.Add(s);
        }

        private void Log(string prefix, string contents)
        {
            LogRaw($"{prefix}{contents}");

            if(OnLog != null)
            {
                OnLog(this);
            }
        }

        public void Log(object o) => Log(o.ToString());

        public void Log(string s) => Log(LogPrefix, s);

        public void LogWarning(object o) => LogWarning(o.ToString());

        public void LogWarning(string s) => Log(LogWarningPrefix, s);

        public void LogError(object o) => LogError(o.ToString());

        public void LogError(string s) => Log(LogErrorPrefix, s);

        #endregion

        #region Lines

        /// <summary>
        /// Gets all logged strings within this Logger.
        /// </summary>
        /// <returns></returns>
        public string[] GetLines()
        {
            return ToStringArray();
        }

        /// <summary>
        /// Gets the last string logged, or returns an empty string if there is nothing logged.
        /// </summary>
        /// <returns></returns>
        public string GetLastLine()
        {
            if(_lines.Any())
            {
                return _lines[_lines.Count - 1];
            }

            return string.Empty;
        }

        public void Clear()
        {
            _lines.Clear();
        }

        #endregion

        #region Strings

        public override string ToString()
        {
            return string.Join(Environment.NewLine, _lines);
        }

        public string[] ToStringArray()
        {
            return _lines.ToArray();
        }

        #endregion
    }
}

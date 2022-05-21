using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Core;
using NLog;

namespace Core.CrossCuttingConcerns.Logging.NLog
{
    internal class SerializableLogEvent
    {
        private readonly LogEventInfo _logEvent;
        public SerializableLogEvent(LogEventInfo logEvent)
        {
            _logEvent = logEvent;
        }

        public object Message => _logEvent.Message;
    }
}

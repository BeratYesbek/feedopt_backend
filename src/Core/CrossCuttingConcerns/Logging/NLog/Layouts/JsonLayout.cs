using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NLog;
using NLog.LayoutRenderers;
using NLog.Layouts;

namespace Core.CrossCuttingConcerns.Logging.NLog.Layouts
{
    public class JsonLayout : LayoutRenderer
    {
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            var log = new SerializableLogEvent(logEvent);
            var json = JsonConvert.SerializeObject(log, Formatting.Indented);
            builder.Append(json);
        }
    }
}

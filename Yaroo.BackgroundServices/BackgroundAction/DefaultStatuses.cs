using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yaroo.BackgroundServices.BackgroundAction
{
    public static class DefaultStatuses
    {
        public static class Common
        {
            public const string Stopped = "Default:Stopped";
            public const string Initialized = "Default:Initialized";
        }

        public static class QueueAction
        {
            public const string Running = "Default:ProcessingMessage";
            public const string WaitingForMessage = "Default:WaitingForMessage";
        }

        public static class TimerAction
        {
            public const string Running = "Default:Running";
            public const string WaitingForTrigger = "Default:WaitingForTimerTrigger";
        }

        public static class StartupAction
        {
            public const string Running = "Default:Running";
            public const string Completed = "Default:Completed";
        }
    }
}

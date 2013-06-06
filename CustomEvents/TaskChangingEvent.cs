using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoronoiMapGen.CustomEvents
{
    public enum ChangeReason {
        Starting,
        NextStep,
        Completed,
        Cancelled,
    }
    public delegate void TaskChangingHandler(object source, TaskChangingEventArgs e);

    //This is a class which describes the event to the class that recieves it.
    //An EventArgs class must always derive from System.EventArgs.
    public class TaskChangingEventArgs : EventArgs
    {
        public ChangeReason Reason { get; private set; }
        public string NewTaskName { get; private set; }

        public TaskChangingEventArgs(ChangeReason reason, string newTask) {
            Reason = reason;
            NewTaskName = newTask;
        }
    }
}

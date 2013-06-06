/*
 * Copyright (c) 2013 Gareth Higgins

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 * 
*/

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

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Extensions
{
    public static class TaskExtensions
    {
        public async static void AwaitTask(this Task task)
        {
            await task;
        }
    }
}

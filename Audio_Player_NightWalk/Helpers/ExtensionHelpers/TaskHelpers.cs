using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audio_Player_NightWalk
{
    public static class TaskHelpers
    {

        /// <summary>
        /// Temporary patch for logging exceptions in the non awaitable Tasks.
        /// </summary>
        /// <param name="task"></param>
        public static async void FireAndForeget(this Task task)
        {

            try
            {
                await task;
            } 
            catch(Exception e)
            {

                Debugger.Break();

                Debug.WriteLine(e);

            }


        }

    }
}

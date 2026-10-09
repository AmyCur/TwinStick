using System;
using System.Threading.Tasks;

namespace Util
{
    public static class TimeUtil{
        public static async Task SetBoolAfterDelay(Action action, int delayTime){
            await Task.Delay(delayTime);
            action();
        }
    }
}
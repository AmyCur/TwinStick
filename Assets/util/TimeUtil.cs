using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Util
{
    public static class TimeUtil{
        public static async Task SetBoolAfterDelay(Action action, int delayTime){
            await Task.Delay(delayTime);
            action();
        }

        public static void PauseGame(){
            Time.timeScale=0;
        }

        public static void ResumeGame(){
            Time.timeScale=1;
        }
    }
}
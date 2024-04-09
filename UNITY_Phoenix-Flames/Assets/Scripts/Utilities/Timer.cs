using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Utilities
{
    public class Timer
    {
        public Action OnTimerStart;
        public Action OnTimerEnded;
   
        public async void StartTimer(float duration)
        {
            OnTimerStart?.Invoke();
            Debug.Log("Timer started");
            
            TimeSpan time = TimeSpan.FromSeconds(duration);
            await Task.Delay(time);
            
            EndTimer();
        }
        
        public void EndTimer()
        {
            Debug.Log("Timer ended");

            OnTimerEnded?.Invoke();
        }
    }
}

using System;
using System.Threading;
namespace WebApplication4.TrafficLight.Timer;

public class TrafficLightTimer
{
    public void setTimer(int seconds)
    {
        
        Thread.Sleep(seconds * 1000); 
    }
}
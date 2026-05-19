namespace WebApplication4.TrafficLight;
using System.Collections.Concurrent;
public static class taskManeger
{
    public static ConcurrentQueue<string> Queue = new ConcurrentQueue<string>();
}
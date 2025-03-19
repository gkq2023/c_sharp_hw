using System;

public class AlarmClock
{
    private int hour;
    private int minute;
    private int second;
    private bool alarmTriggered;

    public event Action Alarm;
    public event Action Tick;

    public void SetAlarmTime(int hour, int minute, int second)
    {
        this.hour = hour;
        this.minute = minute;
        this.second = second;
        alarmTriggered = false;
    }

    public void SubscribeToEvents()
    {
        Alarm += () => Console.WriteLine($"ALARM RINGING! It's {DateTime.Now:HH:mm:ss}");
        Tick += () => Console.WriteLine($"Tick: {DateTime.Now:HH:mm:ss}");
    }

    public void Start()
    {
        Console.WriteLine($"Alarm set for {hour:D2}:{minute:D2}:{second:D2}");
        while (true)
        {
            System.Threading.Thread.Sleep(1000); // 每秒触发一次Tick
            Tick?.Invoke();

            DateTime now = DateTime.Now;
            bool isAlarmTime = hour == now.Hour &&
                              minute == now.Minute &&
                              second == now.Second;

            if (isAlarmTime && !alarmTriggered)
            {
                Alarm?.Invoke();
                alarmTriggered = true;
            }
            else if (!isAlarmTime)
            {
                alarmTriggered = false;
            }
        }
    }
}

class Program
{
    static void Main()
    {
        AlarmClock clock = new AlarmClock();

        // 设置闹钟时间（这里设置为当前时间+10秒用于演示）
        DateTime alarmTime = DateTime.Now.AddSeconds(10);
        clock.SetAlarmTime(alarmTime.Hour, alarmTime.Minute, alarmTime.Second);

        clock.SubscribeToEvents();
        clock.Start();
    }
}
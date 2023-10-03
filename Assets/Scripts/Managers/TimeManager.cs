using Isometric;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager 
{
    // �Ⱦ��� ������ �Լ��� �����ϴ�.. ������ PlayingTime��
    float timeStart;
    List<Timer> timerlist = new List<Timer>();
    bool isPaused = false;
    float ingameTime;

    public float IngameTime { get { return ingameTime; } }
    
    //���� �ΰ��� ����ð��� �����ϴ� ����
    public float PlayingTime;
    public bool IsPaused { get { return isPaused; } set => isPaused = value; }

    public void Init()
    {
        
    }
    
    public void StartGameTimer()
    {
        timeStart = Time.time;
        PlayingTime = 0f;
    }

    public void AddPlayingTime()
    {
        PlayingTime = (Time.time - timeStart);
    }
    
    public void SetTimeScale(float timescale)
    {
        Time.timeScale = timescale;
    }
    public void OnUpdate()
    {

        AddPlayingTime();
        if (IsPaused && timerlist.Count > 0)
        {
            
            for (int i = 0; i < timerlist.Count; i++)
            {
                timerlist[i].waitsec -= Time.deltaTime;
                if (timerlist[i].waitsec < 0)
                {
                    timerlist[i].action();
                    timerlist.RemoveAt(i);
                }
            }
        }
    }
    
    
    public void SetTimer(float time, System.Action action)
    {
        timerlist.Add(new Timer(time, action));
    }
    
}
public class Timer
{
    public System.Action action;
    public float waitsec;
    public Timer(float waitsec, System.Action action)
    {
        this.waitsec = waitsec;
        this.action = action;
    }
}

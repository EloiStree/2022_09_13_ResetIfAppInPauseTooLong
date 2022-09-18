using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResetEventIfAppIsPauseTooLong : MonoBehaviour
{

    public float m_timeToBeConsiderAway = 15;
    public UnityEvent m_onComeBack;
    public UnityEvent m_onAfkReset;

    public bool m_isInPause;
    public DateTime m_startPause;
    public DateTime m_endPause;
    public float m_timeBetweenPause;

    private void Awake()
    {
        m_startPause = DateTime.Now;
        m_endPause = DateTime.Now;
    }

    private void OnApplicationFocus(bool focus)
    {
        //if(Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer )
        SetAsPause(!focus);
    }

    private void OnApplicationPause(bool pause)
    {
        //if (Application.platform == RuntimePlatform.Android)
        SetAsPause(pause);
    }

    public void SetAsPause(bool pause) {
        Debug.Log("Pause " + pause);
        m_isInPause = pause;
        if (pause)
        {
            m_startPause = DateTime.Now;
        }
        else
        {
            m_endPause = DateTime.Now;
            m_timeBetweenPause = (float)((m_endPause - m_startPause).TotalSeconds);
            if (m_timeBetweenPause > m_timeToBeConsiderAway)
                m_onAfkReset.Invoke();
            else m_onComeBack.Invoke();

        }
    }
}

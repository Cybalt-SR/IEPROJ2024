using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoroutineWrapper
{

    private IEnumerator m_coroutine;
    private bool m_isRunning;

    public Action OnStart;
    public Action OnStop;

    public CoroutineWrapper (IEnumerator coroutine)
    {
        m_coroutine = coroutine;
        m_isRunning = false;
    }

    public IEnumerator Run()
    {
        if (m_isRunning) 
            yield break;

        m_isRunning = true;
        OnStart?.Invoke();

        yield return m_coroutine;

        m_isRunning = false;
        OnStop?.Invoke();
    }

    public bool IsRunning { get => m_isRunning; }
}

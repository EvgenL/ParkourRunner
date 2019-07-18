using System;
using UnityEngine;
using UnityEngine.Advertisements;

public abstract class BaseAdController : MonoBehaviour
{
    protected Action _finishedCallback;
    protected Action _skippedCallback;
    protected Action _failedCallback;

    public abstract void Initialize();

    public abstract bool IsAvailable();

    public abstract void ShowAdvertising();
    
    public void InitCallbackHandlers(Action finishedCallback, Action skippedCallback, Action failedCallback)
    {
        _finishedCallback = finishedCallback;
        _skippedCallback = skippedCallback;
        _failedCallback = failedCallback;
    }

    public void HandleAdResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                _finishedCallback.SafeInvoke();
                break;

            case ShowResult.Skipped:
                _skippedCallback.SafeInvoke();
                break;

            case ShowResult.Failed:
                _finishedCallback.SafeInvoke();
                break;
        }
    }
}

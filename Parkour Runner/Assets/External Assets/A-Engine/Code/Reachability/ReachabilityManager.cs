// A-Engine, Code version: 1

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AEngine
{
    public enum ReachabilityStatus
    {
        NotReachable = 0,
        Reachable
    };

    public enum ReachabilityType
    {
        HostReachability = 0,
        InternetReachability,
        WifiReachability
    };

    public class ReachabilityManager : MonoSingleton<ReachabilityManager>
    {
		/*
        public delegate void OnInternetReachabilityStatusChanged(ReachabilityStatus newStatus);
        public event OnInternetReachabilityStatusChanged OnInternetReachabilityStatusChangedEvent;

        public delegate void OnAverageInternetStatusChanged(ReachabilityStatus newStatus);
        public event OnAverageInternetStatusChanged OnAverageInternetStatusChangedEvent;

        private ReachabilityStatus _averageReachabilityStatus = ReachabilityStatus.Reachable;
        public ReachabilityStatus AverageReachabilityStatus
        {
            get { return _averageReachabilityStatus; }
            set
            {
                if (_averageReachabilityStatus != value)
                {
                    _averageReachabilityStatus = value;
                    if (Debug.isDebugBuild)
                        Debug.Log("AVERAGE Network status changed = " + _averageReachabilityStatus);

                    if (OnAverageInternetStatusChangedEvent != null)
                        OnAverageInternetStatusChangedEvent(_averageReachabilityStatus);
                }
            }
        }

        private List<ReachabilityStatus> _reachabilityStatusHistory;
        private ReachabilityStatus _currentReachabilityStatus = ReachabilityStatus.Reachable;
        public ReachabilityStatus CurrentReachabilityStatus
        {
            get { return _currentReachabilityStatus; }
            set
            {
                CorrectAverageInternetStatus(value);
                AddStatusToHistory(value);

                if (_currentReachabilityStatus != value)
                {
                    _currentReachabilityStatus = value;
                    if (Debug.isDebugBuild)
                        Debug.Log(" Network status changed = " + _currentReachabilityStatus);

                    if (OnInternetReachabilityStatusChangedEvent != null)
                        OnInternetReachabilityStatusChangedEvent(_currentReachabilityStatus);
                }
            }
        }

        private const int _averageHistoryCount = 2; // must be less than "_historyCapacity"
        private void CorrectAverageInternetStatus(ReachabilityStatus newStatus)
        {
            int lookIndex = _reachabilityStatusHistory.Count - _averageHistoryCount;
            lookIndex = Mathf.Max(lookIndex, 0);
            bool isAllStatusEquals = true;
            for (int i = _reachabilityStatusHistory.Count - 1; i >= lookIndex; i--)
            {
                ReachabilityStatus historyStatus = _reachabilityStatusHistory[i];
                if (newStatus != historyStatus)
                    isAllStatusEquals = false;
            }
            if (isAllStatusEquals)
                AverageReachabilityStatus = newStatus;
        }

        private const int _historyCapacity = 3;
        private void AddStatusToHistory(ReachabilityStatus status)
        {
            if (_reachabilityStatusHistory.Count > _historyCapacity)
                _reachabilityStatusHistory.RemoveAt(0);
            _reachabilityStatusHistory.Add(status);
        }

        protected override void Init()
        {
            _reachabilityStatusHistory = new List<ReachabilityStatus>();
            if (Debug.isDebugBuild)
                Debug.Log("OPERATION SYSTEM: " + SystemInfo.operatingSystem);

            sendTime = Time.time - sendingInterval - 1;
            DontDestroyOnLoad(this.gameObject);
            errorConnectionCount = 0;
        }

        private const int ErrorCountToChangeStatue = 2;
        private float sendTime = 0.0f;
        private float sendingInterval = 4.0f;
        private bool isSending = false;
        private int errorConnectionCount;


        public void Update()
        {
            if (Time.time - sendTime > sendingInterval)
            {
                isSending = false;
            }

            if (!isSending)
            {
                isSending = true;
                sendTime = Time.time;

                StopCoroutine("CheckConnection");
                StartCoroutine("CheckConnection");
            }
        }

        IEnumerator CheckConnection()
        {
            const float timeout = 3.2f;
            float startTime = Time.time;
            Ping ping = new Ping("8.8.8.8");

            while (true)
            {
                if (ping.isDone && ping.time != -1)
                {                    
                    CurrentReachabilityStatus = ReachabilityStatus.Reachable;
                    yield break;
                }
                if (Time.time - startTime > timeout)
                {
                    errorConnectionCount++;
                    if (errorConnectionCount >= ErrorCountToChangeStatue)
                    {
                        CurrentReachabilityStatus = ReachabilityStatus.NotReachable;
                        errorConnectionCount = 0;
                    }

                    yield break;
                }
                yield return new WaitForEndOfFrame();
            }
        }
        */
    }
    
}

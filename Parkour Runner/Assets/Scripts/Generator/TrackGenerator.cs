using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Track;
using UnityEngine;

enum Phase
{
    Start,
    Challenge
}

public class TrackGenerator : MonoBehaviour
{
    private static string _tracksPath = "Track/";
    private static string _obstaclePath = "Obstacles/";
    private static string _backgroundPath = "Background/";

    public Transform Player;

    public Transform TrackCreationPoint;
    public Transform TrackDestructionPoint;

    private List<GameObject> _trackPrefabs = new List<GameObject>();
    private List<GameObject> _obstaclePrefabs = new List<GameObject>();
    private List<GameObject> _backgroundPrefabs = new List<GameObject>();

    private Queue<GameObject> _tracksPool = new Queue<GameObject>();
    private Queue<GameObject> _backgroundPool = new Queue<GameObject>();

    private Transform TrackEndPoint;
    private Transform BackgroundEndPoint;
    private Phase PhaseName;

    void Awake()
    {
        LoadTrackPrefabs();
    }

    void Start ()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        FollowPlayer();
        PhaseName = Phase.Start;
        GenerateTracks();
	    PhaseName = Phase.Challenge;
    }

    private void LoadTrackPrefabs()
    {
        _trackPrefabs = Resources.LoadAll<GameObject>(_tracksPath).ToList();
        print("Loaded " + _trackPrefabs.Count + " track prefabs.");
        _obstaclePrefabs = Resources.LoadAll<GameObject>(_obstaclePath).ToList();
        print("Loaded " + _obstaclePrefabs.Count + " obstacle prefabs.");
        _backgroundPrefabs = Resources.LoadAll<GameObject>(_backgroundPath).ToList();
        print("Loaded " + _backgroundPrefabs.Count + " background prefabs.");

    }

    void Update ()
    {
        FollowPlayer();
        GenerateTracks();
        DeletePassedTracks();
        GenerateBackground();
        //DeletePassedBackground();
    }

    private void GenerateBackground()
    {
        while (
        (BackgroundEndPoint == null) ||
        Vector3.Distance(Player.position, BackgroundEndPoint.position)
               < Vector3.Distance(Player.position, TrackCreationPoint.position))
        {
            Vector3 nextPosition;
            if (BackgroundEndPoint == null)
            {
                nextPosition = TrackDestructionPoint.position + Vector3.down * 35f;
            }
            else
            {
                nextPosition = BackgroundEndPoint.position;
            }
            GameObject bgGo = Instantiate(GetNextBackground(), nextPosition, Quaternion.identity);
            _backgroundPool.Enqueue(bgGo);
            Background bgScript = bgGo.GetComponent<Background>();
            BackgroundEndPoint = bgScript.EndPoint;
        }
    }

    private void GenerateTracks()
    {
        while (CheckDistanceToNextTrack())
        {
            Vector3 nextTrackPosition;
            if (TrackEndPoint == null)
            {
                nextTrackPosition = TrackDestructionPoint.position;
            }
            else
            {
                nextTrackPosition = TrackEndPoint.position + TrackEndPoint.forward * GapToNextTrack();
            }
            GameObject trackGo = Instantiate(GetNextTrack(), nextTrackPosition, Quaternion.identity);
            _tracksPool.Enqueue(trackGo);
            Track trackScript = trackGo.GetComponent<Track>();
            TrackEndPoint = trackScript.EndPoint;
            GenerateObstacles(trackScript);
        }
    }

    private void GenerateObstacles(Track trackScript)
    {
        foreach (Transform obstaclePoint in trackScript.ObstaclePoints)
        {
            if (!ObstaclePossibility()) continue;
            Instantiate(GetRandomObstacle(), obstaclePoint.position, obstaclePoint.rotation, trackScript.transform);
        }
    }

    private bool ObstaclePossibility()
    {
        if (PhaseName == Phase.Start) return false;
        //TODO Зависимость от фазы игры.
        return true;
    }

    private bool CheckDistanceToNextTrack()
    {
        if (TrackEndPoint == null) return true;
        return Vector3.Distance(Player.position, TrackEndPoint.position)
               < Vector3.Distance(Player.position, TrackCreationPoint.position);
    }
    
    private GameObject GetRandomObstacle()
    {
        //TODO (нужна ли?) зависимость от предыдущих препятствий
        return _obstaclePrefabs[Random.Range(0, _obstaclePrefabs.Count)];
    }

    public GameObject GetNextTrack()
    {
        //TODO псевдослучайная генерация
        //TODO Зависимость от текущей высоты(?)
        return _trackPrefabs[Random.Range(0, _trackPrefabs.Count)];
    }

    private GameObject GetNextBackground()
    {
        //TODO Зависимость от текущей локации
        return _backgroundPrefabs[Random.Range(0, _backgroundPrefabs.Count)];
    }

    //TODO Расстояние между двумя сегментами домами будет зависеть от скорости игрока
    private float GapToNextTrack()
    {
        return 0f;
    }
    
    private void DeletePassedTracks()
    {
        var lastTrack = _tracksPool.Peek().transform;
        if (Vector3.Distance(Player.position, lastTrack.position)
            > Vector3.Distance(Player.position, TrackDestructionPoint.position))
        {
            Destroy(_tracksPool.Dequeue());
        }
    }

    private void DeletePassedBackground()
    {
        var lastBg = _backgroundPool.Peek().transform;
        if (Vector3.Distance(Player.position, lastBg.position)
            > Vector3.Distance(Player.position, TrackDestructionPoint.position))
        {
            Destroy(_backgroundPool.Dequeue());
        }
    }

    private void FollowPlayer()
    {
        transform.position = new Vector3(Player.position.x, 0f , Player.position.z);
    }

}

using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ExtremlyReloader : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _minY;
    [SerializeField] private float _checkTime;

    private void Start()
    {
        StartCoroutine(CheckFallErrorProcess());
    }

    private IEnumerator CheckFallErrorProcess()
    {
        float duration = _checkTime;

        while (duration > 0f)
        {
            if (_target.position.y < _minY)
            {
                Debug.LogError("Fall player muscles (colliders). Camera under floor");
                Reload();
            }

            duration -= Time.deltaTime;

            yield return null;
        }
    }

    private void Reload()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(index);
        
        while (unloadOperation != null && !unloadOperation.isDone)
        {
        }

        ParkourSlowMo.Instance.UnSlow();
        SceneManager.LoadScene(index);
    }
}

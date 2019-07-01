using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour {


    [SerializeField]  private int _sceneID;
    [SerializeField]  private Image _loadingImg;
    [SerializeField]  private Text _progressText;

    private void Start()
    {
        StartCoroutine(AsyncLoad());
    }

    private void Update()
    {
        gameObject.transform.GetChild(0).gameObject.GetComponent<RectTransform>().Rotate(new Vector3(0,0,1)*500*Time.deltaTime);
    }
    private IEnumerator AsyncLoad()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(_sceneID);
        while (!operation.isDone)
        {
            float progress = operation.progress / 0.9f;
            _loadingImg.fillAmount = progress;
            _progressText.text = string.Format("{0:0}%", progress*100);
            yield return null;
        }
    }
}

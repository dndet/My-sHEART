using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScript : MonoBehaviour
{
    async void Start()
    {
        Time.timeScale = 1.0f;
        AsyncOperation scene = SceneManager.LoadSceneAsync("Home");
        if(scene != null)
        {
            scene.allowSceneActivation = false;
            await Task.Delay(2000);
            scene.allowSceneActivation = true;
        }
    }
}

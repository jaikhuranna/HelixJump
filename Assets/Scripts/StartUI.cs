using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }
}

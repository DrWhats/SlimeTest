using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour
{

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}

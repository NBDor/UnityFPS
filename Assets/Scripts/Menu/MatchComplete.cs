using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchComplete : MonoBehaviour
{

    public void LoadCredicts()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

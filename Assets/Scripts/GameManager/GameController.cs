using UnityEngine;
using System.Threading.Tasks;

public class GameController : MonoBehaviour
{

    public GameOverScreen GameOverScreen;
    public GameObject victoryUI;
    
    bool gameHasEnded = false;

    public void Victory()
    {
        victoryUI.SetActive(true);
    }

    public void GameOver()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            GameObject ally = GameObject.FindWithTag("Allies");
            GameOverScreen.Setup();
        }
    }
    public void checkVictory()
    {
        GameObject enemy = GameObject.FindWithTag("Enemy");
        GameObject enemyCommander = GameObject.FindWithTag("Enemy Commander");

        if (enemy.GetComponent<Health>().IsDead() && enemyCommander.GetComponent<Health>().IsDead() && !gameHasEnded)
        {
            gameHasEnded = true;
            Victory();
        }
        return;
    }
}

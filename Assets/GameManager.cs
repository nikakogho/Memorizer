using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public string[] allWords;
    public int amount;
    public GameObject gamePlayUI;
    public GameObject entranceUI;
    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }

    [ContextMenu("Test Extra Words")]
    void TestExtraWords()
    {
        bool allClear = true;

        for (int i = 0; i < allWords.Length; i++)
        {
            for (int j = 0; j < i; j++)
            {
                if (allWords[i] == allWords[j])
                {
                    Debug.LogError("Elements " + i + " and " + j + " have the same value!");
                    allClear = false;
                }
            }
        }

        if (allClear)
            Debug.Log("All Clear!");
    }

    public void StartGame()
    {
        entranceUI.SetActive(false);
        gamePlayUI.SetActive(true);
    }

    public void OnWordAmountChanged(string value)
    {
        amount = int.Parse(value);
    }

    public void Again()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }
}

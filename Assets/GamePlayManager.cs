using UnityEngine;
using System.Linq;
using System.Collections;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour {
    string[] words;
    int index = -1;

    public GameObject showUI;
    public GameObject guessUI;
    public GameObject congratulationsUI;
    public Button confirmGuessButton;
    public Text showText;
    public InputField guessField;
    public Text guessPlaceHolderText;

    public Color guessPlaceHolderDefaultColor;

    void OnEnable()
    {
        words = GetWords();
        index = -1;

        showUI.SetActive(true);
        ShowNext();
    }

    string[] GetWords()
    {
        var manager = GameManager.instance;
        var list = manager.allWords.ToList();
        var words = new string[manager.amount];

        for(int i = 0; i < manager.amount; i++)
        {
            int j = Random.Range(0, list.Count);

            words[i] = list[j];
            list.RemoveAt(j);
        }

        return words;
    }

    public void ShowNext()
    {
        if(index < words.Length - 1)
        {
            index++;
            showText.text = words[index];
        }
        else
        {
            index = 0;
            StartGuessing();
        }
    }

    public void ConfirmGuessing()
    {
        if(guessField.text == words[index])
        {
            StartCoroutine(CorrectlyGuessedRoutine());
        }
        else
        {
            guessField.text = string.Empty;
            guessPlaceHolderText.text = "Wrong!";
            guessPlaceHolderText.color = Color.red;
        }
    }

    IEnumerator CorrectlyGuessedRoutine()
    {
        confirmGuessButton.enabled = false;
        guessField.text = string.Empty;
        guessPlaceHolderText.text = "Correct!";
        guessPlaceHolderText.color = Color.green;

        yield return new WaitForSeconds(1);

        index++;

        if(index < words.Length)
        {
            guessPlaceHolderText.text = "Enter Text...";
            guessPlaceHolderText.color = guessPlaceHolderDefaultColor;
            confirmGuessButton.enabled = true;
        }
        else
        {
            guessUI.SetActive(false);
            congratulationsUI.SetActive(true);
        }
    }

    void StartGuessing()
    {
        showUI.SetActive(false);
        guessUI.SetActive(true);
        confirmGuessButton.enabled = true;
    }
}

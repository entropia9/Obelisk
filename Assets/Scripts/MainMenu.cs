using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField]
    private Button startGameButton;
    [SerializeField]
    private Button continueButton;

    private void Start()
    {
        if (!GameManager.Instance.HasGameData())
        {
            continueButton.interactable = false;
        }
    }
    public void OnGameStartClicked()
    {
        DisableButtons();
        GameManager.Instance.ClearData();
        GameManager.Instance.NewGame();

        SceneManager.LoadScene("Main");
    }

    public void OnContinueClicked()
    {
        DisableButtons();
        GameManager.Instance.LoadGame();
        SceneManager.LoadScene("Main");
    }

    private void DisableButtons()
    {
        startGameButton.interactable= false;
        continueButton.interactable= false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    [SerializeField] GameMaster master;
    [SerializeField] GameUI gameUI;

    [SerializeField] GameObject ExitButton;
    [SerializeField] GameObject ExitButton2;

    public void OnRetryButton()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    public void OnTitleButton()
    {
        SceneManager.LoadScene("Title");
    }

    public void OnRuleButton()
    {
        gameUI.RulePanel.SetActive(true);
    }

    public void OnExitButton()
    {
        gameUI.RulePanel.SetActive(false);
        ExitButton.transform.localScale = Vector3.one;
        Image imege = ExitButton.GetComponent<Image>();
        imege.color = Color.white;
    }

    public void OnDeckButton()
    {
        gameUI.DeckPanel.SetActive(true);
    }

    public void OffDeckButton()
    {
        gameUI.DeckPanel.SetActive(false);
        ExitButton.transform.localScale = Vector3.one;
        Image imege = ExitButton.GetComponent<Image>();
        imege.color = Color.white;
    }


    public void SlimeButton()
    {
        master.enemyNum = 0;
        master.Serect();
    }

    public void GolemButton()
    {
        master.enemyNum = 1;
        master.Serect();
    }

    public void DragonButton()
    {
        master.enemyNum = 2;
        master.Serect();
    }


    public void ProceedButton()
    {
        gameUI.Proceed();
    }

    public void ReturnButton()
    {
        gameUI.ReturnAA();
    }

    public void ResetButton()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }
}
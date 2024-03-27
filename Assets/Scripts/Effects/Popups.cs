using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using GameQuest;
using Levels;

public class Popups : MonoBehaviour
{
    [SerializeField] Button restartButton;
    [SerializeField] TextMeshProUGUI questText;

    private void Start()
    {
        GameConditions.LastLevelPassed += ShowRestartButton;
        Quest.QuestIsReady += ShowQuestText;
        GridBuilder.QuestHide += HideQuestText;
    }

    void ShowQuestText(string text)
    {
        questText.text = "Choose: " + text;
        questText.gameObject.SetActive(true);

        var color = questText.color;
        var tempColor = color;
        tempColor.a = 1;

        questText.DOColor(tempColor, 1f);
    }

    void HideQuestText()
    {
        questText.gameObject.SetActive(false);
    }

    public void ShowRestartButton(Action callback)
    {
        restartButton.gameObject.SetActive(true);
        restartButton.transform.localScale = new Vector3(0,0,0);

        restartButton.onClick.AddListener(() => callback());
        restartButton.onClick.AddListener(() => RestartButton());

        restartButton.transform.DOScale(new Vector3(1,1,1), 1).SetEase(Ease.InBounce);
    }

    async void RestartButton()
    {
        await restartButton.transform.DOScale(new Vector3(0, 0, 0), 1).SetEase(Ease.InBounce).AsyncWaitForCompletion();
        restartButton.gameObject.SetActive(false);
    }
}

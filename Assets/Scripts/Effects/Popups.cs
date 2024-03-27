using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using GameQuest;
using Levels;

public class Popups : MonoBehaviour
{
    [SerializeField] GameObject restartButton;
    [SerializeField] Transform restartButtonPosition;
    [SerializeField] TextMeshProUGUI questText;

    Button restartBtn;

    private void Start()
    {
        GameConditions.LastLevelPassed += ShowRestartButton;
        Quest.QuestIsReady += ShowQuestText;
        GridBuilder.QuestHide += HideQuestText;
    }

    async void ShowQuestText(string text)
    {
        questText.text = "Choose: " + text;
        questText.gameObject.SetActive(true);

        await questText.DOFade(1, 1.5f).AsyncWaitForCompletion();
    }

    void HideQuestText()
    {
        questText.gameObject.SetActive(false);
    }

    public void ShowRestartButton(Action callback)
    {
        restartBtn = Instantiate(restartButton, restartButtonPosition).GetComponent<Button>();

        restartBtn.transform.localScale = new Vector3(0,0,0);

        restartBtn.onClick.AddListener(() => callback());
        restartBtn.onClick.AddListener(() => RestartButton());

        restartBtn.transform.DOScale(new Vector3(1,1,1), 1).SetEase(Ease.InBounce);
        restartBtn.interactable = true;
    }

    void RestartButton()
    {
        restartBtn.interactable = false;
        restartBtn.transform.DOScale(new Vector3(0, 0, 0), 1).SetEase(Ease.InBounce);

        Destroy(restartBtn.gameObject);
        restartBtn = null;
    }
}

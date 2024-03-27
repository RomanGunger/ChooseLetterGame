using System;
using System.Threading.Tasks;
using GridItems;
using Levels;
using UnityEngine;

public class GameConditions : MonoBehaviour
{
    public static event Action<Action> LastLevelPassed;
    public static event Action<string> QuestShow;

    [SerializeField] GameObject winEffectPrefab;
    [SerializeField] LevelsProgression levelsProgression;
    [SerializeField] LevelBuilder levelBuilder;
    [SerializeField] Fader fader;
    [SerializeField] GameObject raycastBlocker;

    int currentLevel = 0;

    private void Start()
    {
        NewGame();
        GridItem.Right += Win;
        GridItem.Wrong += Mistake;
    }

    public async void NewGame()
    {
        raycastBlocker.SetActive(true);

        await fader.Fade(0, 2f);
        await levelBuilder.Build(true, levelsProgression.GetCollumnsCount(currentLevel)
            , levelsProgression.GetItemsCount(currentLevel));

        raycastBlocker.SetActive(false);
    }

    public async void NextLevel()
    {
        await levelBuilder.Build(false, levelsProgression.GetCollumnsCount(currentLevel)
        , levelsProgression.GetItemsCount(currentLevel));

        raycastBlocker.SetActive(false);
    }

    async public void Win(Transform imageTransform)
    {
        raycastBlocker.SetActive(true);
        Shaker shaker = new Shaker(imageTransform);
        shaker.DoShakeAction(0.5f, 5f);

        GameObject winEffect = Instantiate(winEffectPrefab, imageTransform);

        currentLevel++;

        await Task.Delay(2000);

        if (currentLevel >= levelsProgression.GetLevelsCount())
        {
            await fader.Fade(0.99f, 2f);

            LastLevelPassed?.Invoke(RestartButton);
        }
        else
        {
            levelBuilder.DestroyLevel();
            NextLevel();
        }
    }

    private void RestartButton()
    {
        levelBuilder.DestroyLevel();
        currentLevel = 0;
        NewGame();
    }

    public void Mistake(Transform imageTransform)
    {
        Shaker shaker = new Shaker(imageTransform);
        shaker.DoShakeAction(0.7f, 7f);
    }

    private void OnDestroy()
    {
        GridItem.Right -= Win;
        GridItem.Wrong -= Mistake;
    }
}

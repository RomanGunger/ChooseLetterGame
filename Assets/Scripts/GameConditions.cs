using System;
using System.Threading.Tasks;
using GridItems;
using Levels;
using UnityEngine;

public class GameConditions : MonoBehaviour
{
    public static event Action LevelPassed;

    [SerializeField] GameObject winEffectPrefab;
    [SerializeField] LevelsProgression levelsProgression;
    [SerializeField] LevelBuilder levelBuilder;

    int currentLevel = 0;

    private void Start()
    {
        NewGame();
        GridItem.Right += Win;
        GridItem.Wrong += Mistake;
    }

    public async void NewGame()
    {
        await levelBuilder.Build(true, levelsProgression.GetCollumnsCount(currentLevel)
            ,levelsProgression.GetItemsCount(currentLevel));
    }

    public async void NextLevel()
    {

    }

    async public void Win(Transform imageTransform)
    {
        Shaker shaker = new Shaker(imageTransform);
        shaker.DoShakeAction(0.5f, 5f);

        GameObject winEffect = Instantiate(winEffectPrefab, imageTransform);

        currentLevel++;

        await Task.Delay(2000);
        LevelPassed?.Invoke();
    }

    public void Mistake(Transform imageTransform)
    {
        Shaker shaker = new Shaker(imageTransform);
        shaker.DoShakeAction(0.7f, 7f);
        Debug.Log("Mistake");
    }
}

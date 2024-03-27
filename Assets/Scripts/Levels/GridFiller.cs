using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using GameQuest;
using GridItems;
using UnityEngine;
using UnityEngine.UI;

namespace Levels
{
    public class GridFiller
    {
        List<GameItemScriptableObject> gameItemScriptableObjects = new List<GameItemScriptableObject>();

        bool RandomBool()
        {
            System.Random random = new System.Random();
            int randomIndex = random.Next(0, 2);
            return randomIndex == 0;
        }

        public void LoadObjects()
        {
            gameItemScriptableObjects.Clear();

            var loadedObjects = RandomBool() ? Resources.LoadAll("Letters", typeof(GameItemScriptableObject))
                : Resources.LoadAll("Numbers", typeof(GameItemScriptableObject));

            foreach (GameItemScriptableObject item in loadedObjects)
            {
                gameItemScriptableObjects.Add(item);
            }
        }

        public async Task FillGrid(bool isAsync, List<GridItem> gridItems)
        {
            List<GameItemScriptableObject> tempItems = new List<GameItemScriptableObject>(gameItemScriptableObjects);

            foreach (var item in gridItems)
            {
                Quest.QuestIsReady += item.OnQuestReceived;

                Transform trans = item.image.gameObject.transform;
                trans.localScale = new Vector3(0, 0, 0);

                // Prevent dublicates
                System.Random random = new System.Random();
                int randomIndex = random.Next(0, tempItems.Count);
                item.id = tempItems[randomIndex].Id;
                item.image.sprite = tempItems[randomIndex].Sprite;

                float aspectRatio = tempItems[randomIndex].Sprite.bounds.size.x /
                    tempItems[randomIndex].Sprite.bounds.size.y;

                item.image.GetComponent<AspectRatioFitter>().aspectRatio = aspectRatio;

                tempItems.RemoveAt(randomIndex);


                var tempColor = item.image.color;
                tempColor.a = 1f;
                item.image.color = tempColor;

                if (isAsync)
                {
                    trans.DOScale(1, .5f).SetEase(Ease.OutBounce);
                    await Task.Delay(500);
                }
                else
                    trans.DOScale(1, 0f).SetEase(Ease.OutBounce);
            }
        }

        public void RemoveItem(string id)
        {
            foreach (var item in gameItemScriptableObjects)
            {
                if (item.Id == id)
                {
                    gameItemScriptableObjects.Remove(item);
                    break;
                }
            }
        }
    }
}


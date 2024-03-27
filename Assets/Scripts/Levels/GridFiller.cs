using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using GameQuest;
using GridItems;
using UnityEngine;

namespace Levels
{
    public class GridFiller
    {
        List<GameItemScriptableObject> gameItemScriptableObjects = new List<GameItemScriptableObject>();

        public void LoadObjects()
        {
            var loadedObjects = Resources.LoadAll("GameItems", typeof(GameItemScriptableObject));

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
    }
}


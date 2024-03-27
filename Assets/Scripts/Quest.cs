using System;
using System.Collections.Generic;
using GridItems;
using UnityEngine;

namespace GameQuest
{
    public class Quest
    {
        public static event Action<string> QuestIsReady;

        string questId = string.Empty;

        public void SetQuest(List<GridItem> gridItems)
        {
            if (gridItems.Count > 0)
            {
                System.Random random = new System.Random();
                int randomIndex = random.Next(0, gridItems.Count);
                questId = gridItems[randomIndex].id;
                QuestIsReady?.Invoke(questId);
            }
            else
                Debug.Log("Grid items is empty, can't set the quest");
        }
    }
}


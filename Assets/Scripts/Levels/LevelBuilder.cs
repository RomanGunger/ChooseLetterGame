using System.Collections.Generic;
using System.Threading.Tasks;
using GameQuest;
using GridItems;
using UnityEngine;

namespace Levels
{
    public class LevelBuilder : MonoBehaviour
    {
        [SerializeField] GridBuilder gridBuilder;
        GridFiller gridFiller;

        List<GridItem> gridItems = new List<GridItem>();

        private void Awake()
        {
            gridFiller = new GridFiller();
            Quest.QuestIsReady += gridFiller.RemoveItem;
        }

        public async Task Build(bool isAsync, int colimnsCount, int itemsCount)
        {
            gridItems = await gridBuilder.BuildGrid(isAsync, itemsCount, colimnsCount);

            if(isAsync)
                gridFiller.LoadObjects();

            await gridFiller.FillGrid(isAsync, gridItems);

            Quest quest = new Quest();
            quest.SetQuest(gridItems);
        }

        public void DestroyLevel()
        {
            gridBuilder.DestroyLevel(gridItems);
        }

    }
}


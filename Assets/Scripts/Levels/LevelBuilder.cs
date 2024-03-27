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

        List<GridItem> gridItems = new List<GridItem>();

        public async Task Build(bool isAsync, int colimnsCount, int itemsCount)
        {
            gridItems = await gridBuilder.BuildGrid(isAsync, itemsCount, colimnsCount);

            GridFiller gridFiller = new GridFiller();
            gridFiller.LoadObjects();
            await gridFiller.FillGrid(isAsync, gridItems);

            Quest quest = new Quest();
            quest.SetQuest(gridItems);
        }
    }
}


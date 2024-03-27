using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using GridItems;
using UnityEngine;
using UnityEngine.UI;

namespace Levels
{
    public class GridBuilder : MonoBehaviour
    {
        [SerializeField] GridLayoutGroup gridLayout;
        [SerializeField] GameObject gridItemObject;

        public async Task<List<GridItem>> BuildGrid(bool isAsync, int itemsCount, int collumnsCount)
        {
            gridLayout.gameObject.transform.localScale = new Vector3(0, 0, 0);
            gridLayout.constraintCount = collumnsCount;

            List<GridItem> gridItems = new List<GridItem>();

            for (int i = 0; i < itemsCount; i++)
            {
                GridItem gridItem = Instantiate(gridItemObject, gridLayout.GetComponent<Transform>())
        .GetComponent<GridItem>();
                gridItems.Add(gridItem);
            }

            if (isAsync)
                await gridLayout.gameObject.transform.DOScale(1, .5f).SetEase(Ease.OutBounce).AsyncWaitForCompletion();
            else
                gridLayout.gameObject.transform.DOScale(1, 0f).SetEase(Ease.OutBounce);

            return gridItems;
        }


        public void DestroyLevel(List<GridItem> gridItems)
        {
            foreach (var item in gridItems)
            {
                Destroy(item.gameObject);
            }

            gridItems.Clear();
            //fader.HideQuestText();
        }
    }
}

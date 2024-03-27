using UnityEngine;

namespace GridItems
{
    [CreateAssetMenu(fileName = "LevelProgression", menuName = "LevelProgression/New Level Progression", order = 0)]
    public class LevelsProgression : ScriptableObject
    {
        [SerializeField] LevelProgressionClass[] levelProgression = null;

        public int GetCollumnsCount(int level)
        {
            return levelProgression[level].collumnCount;
        }

        public int GetItemsCount(int level)
        {
            return levelProgression[level].itemsCount;
        }

        public int GetLevelsCount()
        {
            return levelProgression.Length;
        }


        [System.Serializable]
        class LevelProgressionClass
        {
            public int collumnCount;
            public int itemsCount;
        }
    }
}


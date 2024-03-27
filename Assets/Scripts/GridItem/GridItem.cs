using System;
using GameQuest;
using UnityEngine;
using UnityEngine.UI;

namespace GridItems
{
    [RequireComponent(typeof(Button))]
    public class GridItem : MonoBehaviour
    {
        public static event Action<Transform> Right;
        public static event Action<Transform> Wrong;

        public string id;
        public Image image;

        public Button Button { get; private set; }

        private void Awake()
        {
            Button = GetComponent<Button>();
        }

        public void Compare(string questId)
        {
            if (id == questId)
            {
                Right?.Invoke(image.transform);
            }
            else
            {
                Wrong?.Invoke(image.transform);
            }
        }

        public void OnQuestReceived(string quest)
        {
            Button.onClick.AddListener(() => Compare(quest));
        }

        private void OnDestroy()
        {
            Quest.QuestIsReady -= OnQuestReceived;
        }
    }
}


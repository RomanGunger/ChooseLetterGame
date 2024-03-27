using UnityEngine;

[CreateAssetMenu(fileName = "GameItemScriptableObject"
    , menuName = "GameItemScriptableObject/New GameItem ScriptableObject", order = 1)]
public class GameItemScriptableObject : ScriptableObject
{
    [SerializeField] string id;
    [SerializeField] Sprite sprite;

    public string Id => id;
    public Sprite Sprite => sprite;
}

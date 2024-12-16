using UnityEngine;

[CreateAssetMenu(fileName = "New LevelData", menuName = "Level Data")]
public class LevelData : ScriptableObject
{
    [Min(1), SerializeField]
    private int _rows;
    
    [Min(1), SerializeField]
    private int _levelsAmount;

    [SerializeField]
    private CardBundleData[] _cardBundlesData;

    public int Rows => _rows;
 
    public int LevelsAmount => _levelsAmount;   

    public CardBundleData[] CardBundlesData => _cardBundlesData;
}

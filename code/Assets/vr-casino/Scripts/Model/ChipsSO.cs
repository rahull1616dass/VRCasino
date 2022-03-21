using UnityEngine;

public enum EChipValue{
    None = 0,
    White,
    Blue,
    Green,
    Red,
    Black

};


[CreateAssetMenu(fileName = "ChipsSO", menuName = "Chips/ChipsSO", order = 0)]
public class ChipsSO : ScriptableObject {

    [SerializeField] private EChipValue m_EChipType;
    [SerializeField] private Material m_ChipMat;
}

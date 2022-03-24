using UnityEngine;

public enum EChipValue{
    None = 0,
    White = 1, //10
    Blue = 10, //10
    Green = 25, //4
    Red = 5, //20
    Black = 100 //1
};


[CreateAssetMenu(fileName = "ChipsSO", menuName = "Chips/ChipsSO", order = 0)]
public class ChipSO: ScriptableObject {

    public EChipValue m_EChipType;
    public Material m_ChipMat;
}

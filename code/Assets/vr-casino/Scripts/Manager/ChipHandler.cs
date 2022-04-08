using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChipHandler : MonoBehaviour
{
    // The height of each chip, so that when stacked on top of each other the chips won't get clipped.
    const float CHIP_HEIGHT = 0.0033f;

    // Distance between two given chip stacks.
    const float CHIP_DISTANCE = 0.08f;

    [SerializeField] List<ChipSO> Chips;
    [SerializeField] Transform PlayerPos, ChipParent;
    [SerializeField] int InitalChipValue = 500;
    [SerializeField] Chip ChipPrefab;
    [SerializeField] Player player;

    private void Start()
    {
        GenerateChipsForPlayer(InitalChipValue);
    }

    public void Bet(Chip Chip)
    {
        player.currentChips.Remove(Chip);
        player.CurrentBet += (int)Chip._chipValue;
    }

    public void GenerateChipsForPlayer(int chipValue)
    {
        Dictionary<EChipValue, int> chipStackSizes = new Dictionary<EChipValue, int>();
        foreach (EChipValue chipType in Enum.GetValues(typeof(EChipValue)))
        {
            chipStackSizes.Add(chipType, 0);
        }


        while (chipValue > 0)
        {
            foreach (ChipSO Chip in Chips)
            {
                if (chipValue >= (int)Chip.m_EChipType)
                {
                    Chip chip = Instantiate<Chip>(ChipPrefab, ChipParent);
                    chip.gameObject.SetActive(true);
                    chip.transform.localPosition = PlayerPos.localPosition +
                        new Vector3(
                         GetStackPosition(Chip.m_EChipType) * CHIP_DISTANCE,
                         chipStackSizes[Chip.m_EChipType] * CHIP_HEIGHT,
                         0
                        );
                    chipStackSizes[Chip.m_EChipType]++;
                    chip.GetComponent<Renderer>().material = Chip.m_ChipMat;
                    chip._chipValue = Chip.m_EChipType;

                    chipValue -= (int)Chip.m_EChipType;
                    player.currentChips.Add(chip.GetComponent<Chip>());
                }
            }
        }
    }

    // Each stack is ordered accordingly, multiplied by the CHIP_DISTANCE.
    public int GetStackPosition(EChipValue eChipValue)
    {
        switch (eChipValue)
        {
            case EChipValue.White: return 1;
            case EChipValue.Red: return 2;
            case EChipValue.Blue: return 3;
            case EChipValue.Green: return 4;
            case EChipValue.Black: return 5;
            default: return 0;
        }
    }
}
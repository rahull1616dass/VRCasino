using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChipHandler : MonoBehaviour
{
    [SerializeField] List<ChipSO> Chips;
    [SerializeField] Transform PlayerPos, ChipParent;
    [SerializeField] int InitalChipValue = 500;
    [SerializeField] GameObject ChipPrefab;
    [SerializeField] Player player;


    private void Start()
    {
        GenerateChipsForPlayer(InitalChipValue);
    }

    public void Bet(ChipSO Chip)
    {
        player.currentChips.Remove(Chip);
        player.CurrentBet += (int)Chip.m_EChipType;
    }

    public void GenerateChipsForPlayer(int chipValue)
    {
        while (chipValue > 0)
        {
           foreach(ChipSO Chip in Chips)
            {
                if(chipValue > (int)Chip.m_EChipType)
                {
                    Instantiate(ChipPrefab, ChipParent).transform.position = PlayerPos.position;
                    chipValue -= (int)Chip.m_EChipType;
                    player.currentChips.Add(Chip);
                }
            }
        }
    }
}
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

    float lastChipHeight = 0;
    float chipHeight = 0.0033f;


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
                if (chipValue >= (int)Chip.m_EChipType)
                {
                    GameObject chip = Instantiate(ChipPrefab, ChipParent);
                    chip.SetActive(true);
                    chip.transform.localPosition = PlayerPos.localPosition + new Vector3(0, chipHeight + lastChipHeight, 0);
                    lastChipHeight = Math.Abs(PlayerPos.localPosition.y - chip.transform.localPosition.y);
                    chip.GetComponent<Renderer>().material = Chip.m_ChipMat;
                    chipValue -= (int)Chip.m_EChipType;
                    player.currentChips.Add(Chip);
                }
            }
        }
    }
}
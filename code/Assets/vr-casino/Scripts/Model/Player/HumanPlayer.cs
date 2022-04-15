using System.Collections.Generic;
using UnityEngine;

public class HumanPlayer : Player
{
    [SerializeField] private BettingHole bettingHole;

    public void LockBettingValue()
    {
        CurrentBet = bettingHole.m_ChipValues;
        bettingHole.DestroyAllObj();
    }
}
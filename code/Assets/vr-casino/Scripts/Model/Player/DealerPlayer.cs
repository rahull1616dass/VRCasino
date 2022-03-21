using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealerPlayer : Player
{
    public WaitForSeconds TurnWaitForSeconds { get; private set; }

    [SerializeField]
    private float _turnTime;

    private void Start()
    {
        TurnWaitForSeconds = new WaitForSeconds(_turnTime);
    }

    /// <summary>
    /// In order to have a better readable code, I divided it into statements following the AI logic.
    /// </summary>
    public void UpdateBehaviour(Hand humanHand)
    {
        /*
        When the dealer has served every player, the dealers face-down card is turned up. If the total is 17 or more, 
        it must stand. If the total is 16 or under, they must take a card.
        The dealer must continue to take cards until the total is 17 or more, at which point the dealer must stand.
        */
        if(Hand.Count < 17) IsHitting = true;
        else IsHitting = false;

    }
}

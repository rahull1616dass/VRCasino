using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BettingHole : MonoBehaviour
{
    public int m_ChipValues;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Chip>() == null)
            return;
        other.gameObject.GetComponent<Chip>().InsideTheHole = true;
        m_ChipValues += (int)other.gameObject.GetComponent<Chip>()._chipValue;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Chip>() == null)
            return;
        other.gameObject.GetComponent<Chip>().InsideTheHole = false;
        m_ChipValues -= (int)other.gameObject.GetComponent<Chip>()._chipValue;
    }
}

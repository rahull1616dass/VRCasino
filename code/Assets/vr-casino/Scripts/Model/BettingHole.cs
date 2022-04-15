using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BettingHole : MonoBehaviour
{
    public int m_ChipValues;
    private List<GameObject> allCoins = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Chip>() == null)
            return;
        other.gameObject.GetComponent<Chip>().InsideTheHole = true;
        m_ChipValues += (int)other.gameObject.GetComponent<Chip>()._chipValue;
        allCoins.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Chip>() == null)
            return;
        other.gameObject.GetComponent<Chip>().InsideTheHole = false;
        m_ChipValues -= (int)other.gameObject.GetComponent<Chip>()._chipValue;
        allCoins.Remove(other.gameObject);
    }

    public void DestroyAllObj()
    {
        foreach (var item in allCoins)
        {
            Destroy(item);
        }
        allCoins.Clear();
    }
}

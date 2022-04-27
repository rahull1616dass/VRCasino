using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class BettingHole : MonoBehaviour
{
    public int m_ChipValues;
    private List<GameObject> allCoins = new List<GameObject>();
    [SerializeField] private UIManager m_UiManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Chip>() == null)
            return;
        other.gameObject.GetComponent<Chip>().InsideTheHole = true;
        m_ChipValues += (int)other.gameObject.GetComponent<Chip>()._chipValue;
        m_UiManager.OnBetUpdate(m_ChipValues);
        allCoins.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Chip>() == null)
            return;
        other.gameObject.GetComponent<Chip>().InsideTheHole = false;
        m_ChipValues -= (int)other.gameObject.GetComponent<Chip>()._chipValue;
        m_UiManager.OnBetUpdate(m_ChipValues);
        allCoins.Remove(other.gameObject);
    }

    public void DoAnimation(string AnimationName)
    {
        StartCoroutine(DoAnimationOneByOne(AnimationName));
    }

    IEnumerator DoAnimationOneByOne(string AnimationName)
    {
        lock (allCoins)
            for (int i = 0; i < allCoins.Count; i++)
            {

                Debug.Log(allCoins[i].name + "   " + AnimationName);
                allCoins[i].GetComponent<Animator>().enabled = true;
                allCoins[i].GetComponent<Animator>().Play(AnimationName, 0, -1);
                yield return new WaitForSeconds(0.1f);
            }

        lock (allCoins)
            for (int i = 0; i < allCoins.Count; i++)
            {
                yield return new WaitForSeconds(0.8f);
                allCoins.Remove(allCoins[i]);
                //Destroy(allCoins[i]);
            }
        allCoins.Clear();
    }

    public void DestroyAllTheCoins()
    {
        foreach (var item in allCoins)
        {
            Destroy(item);
        }
        allCoins.Clear();
    }
}

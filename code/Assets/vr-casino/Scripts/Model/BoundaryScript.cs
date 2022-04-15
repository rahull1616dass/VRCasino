using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryScript : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Chip>() != null)
            other.GetComponent<Chip>().InitialState = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip : MonoBehaviour
{
    public EChipValue _chipValue;

    private Vector3 oldPosition;
    private Quaternion oldRotation;

    [HideInInspector]
    public bool InsideTheHole;
    private void Start()
    {
        //Save our position / rotation so that we can restore it when we detach
        oldPosition = transform.position;
        oldRotation = transform.rotation;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Table"&&!InsideTheHole)
        {
            transform.position = oldPosition;
            transform.rotation = oldRotation;
        }
    }
}

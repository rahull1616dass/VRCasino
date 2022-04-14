using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Chip : MonoBehaviour
{
    public EChipValue _chipValue;

    private Vector3 oldPosition;
    private Quaternion oldRotation;

    [HideInInspector]
    public bool InsideTheHole;

    private bool InitialState;

    private void Start()
    {
        //Save our position / rotation so that we can restore it when we detach
        oldPosition = transform.position + new Vector3(0, 5f, 0);
        oldRotation = transform.rotation;
        InitialState=true;
        
    }

    private void HandHoverUpdate(Hand hand)
    {
        Debug.Log(hand);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!InsideTheHole && !InitialState)
            return;
        if (collision.transform.tag == "Table")
        {
            transform.position = oldPosition;
            transform.rotation = oldRotation;
            InitialState =true;
        }
        else
            InitialState =false;
    }
}

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

    [HideInInspector]
    public bool InitialState;


    private void Start()
    {
        //Save our position / rotation so that we can restore it when we detach
        oldPosition = transform.position;
        oldRotation = transform.rotation;
        InitialState=true;
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (InitialState)
            return;
        if (collision.transform.tag == "Table"&&!InsideTheHole)
        {
            transform.position = oldPosition;
            transform.rotation = oldRotation;
            InitialState =true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Called");
        InitialState = false;
    }
 }

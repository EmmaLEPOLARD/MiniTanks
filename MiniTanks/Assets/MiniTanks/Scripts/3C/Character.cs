using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Character : MonoBehaviour
{
    [SerializeField]
    protected int m_SprintSpeed = 100;
    [SerializeField]
    protected float m_Speed = 100;
    [SerializeField]
    protected float m_RotateSpeed = 100;
    [SerializeField]
    protected int m_Life = 100;
    [SerializeField]
    protected int m_Armor = 50;

}
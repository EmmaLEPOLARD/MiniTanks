using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Player : Character
{
    #region Member variables
    Rigidbody m_Rg;
    #endregion

    #region Constructor
    public void Awake()
    {
        Time.timeScale = 1;
        m_Rg = GetComponent<Rigidbody>();
    }

    #endregion

    #region Move
    public void Move(Vector3 _dirToGo)
    {
        m_Rg.velocity = _dirToGo.normalized * m_Speed;
    }



    public void Sprint(Vector3 dirToGo)
    {
        m_Rg.velocity = dirToGo.normalized * m_SprintSpeed;
    }
    public void Rotate(Vector3 rotate)
    {
        transform.LookAt(rotate);
    }

    #endregion

}
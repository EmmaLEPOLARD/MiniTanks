using UnityEngine;
using System.Collections.Generic;

public class CameraScript : MonoBehaviour
{
    #region Member variables
    [SerializeField]
    Player m_Player = null;
    [SerializeField]
    int m_YDecalage = 25;
    [SerializeField]
    int m_xAngle = 85;

    Vector3 m_Offset;
    #endregion

    #region Constructor
    void Start()
    {
        m_Offset = new Vector3(0, m_YDecalage, 0);
        transform.rotation = Quaternion.AngleAxis(m_xAngle, transform.right);
    }
    #endregion
    #region Updates
    void LateUpdate()
    {
        transform.position = m_Player.transform.position + m_Offset;
    }
    #endregion

    #region Action
   
    #endregion

    #region Accessor
  
    #endregion
}
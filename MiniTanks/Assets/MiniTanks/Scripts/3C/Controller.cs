using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{

    #region Member variables

    Player m_Player;
    Vector3 m_DirToGo;
    Vector3 m_Rotate;
    Camera m_Camera;
    #endregion

    #region Constructor
    void Start()
    {
        m_Camera = Camera.main;
        m_Player = GetComponent<Player>();

    }
    #endregion

    #region Updates
    void Update()
    {
        #region moveCall
        Move();
        Rotate();
        #endregion

    }
    #endregion

    #region Move Function
    public void Move()
    {
        m_DirToGo = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (Input.GetButton("Sprint"))
        {
            m_Player.Sprint(m_DirToGo);
        }
        else
        {
            m_Player.Move(m_DirToGo);
        }
    }
    public void Rotate()
    {
        m_Rotate = Vector3.zero;

        RaycastHit hit;
        Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            hit.point = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            m_Rotate = hit.point;
        }
        m_Player.Rotate(m_Rotate);
    }
    #endregion
}
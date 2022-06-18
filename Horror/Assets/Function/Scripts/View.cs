using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    // AI �þ߰�
    [SerializeField] float m_angle = 0f;
    // AI �þ� �Ÿ�
    [SerializeField] float m_distance = 0f;
    // �÷��̾� ���̾� ����ũ
    [SerializeField] LayerMask m_layerMask = 0;

    // �þ� üũ �Լ�
    void Sight()
    {
        // ���� �ݰ� ���� �ִ� �÷��̾� �ݶ��̴� ����
        Collider[] t_cols = Physics.OverlapSphere(transform.position, m_distance, m_layerMask);

        // ����Ǿ��ٸ�,
        if ( t_cols.Length > 0)
        {
            // �÷��̾��� transform���� �޾ƿ���
            Transform t_tfPlayer = t_cols[0].transform;
            // �� ��, �÷��̾�� 1�� ���̹Ƿ� �迭�� �ε��� = 0

            // �÷��̾ ������⿡ �ִ���
            Vector3 t_direction = (t_tfPlayer.position - transform.position).normalized;
            // ���� z�� ����� �÷��̾� ���� ���� ���� ���� ���ϱ�
            float t_angle = Vector3.Angle(t_direction, transform.forward);
            
            // ���� ������ ���̰� �þ߰��� �� �̳��� ����ٸ�,
            if (t_angle < m_angle *0.5)
            {
                // ���Ͱ� �÷��̾�� Ray�� �� - �÷���� ���� ���̿� ���ع��� ������ üũ
                if ( Physics.Raycast(transform.position, t_direction, out RaycastHit hit, m_distance))
                {
                    Debug.DrawRay(transform.position, transform.forward, Color.blue, 0.3f);
                    Debug.Log("플레이어 콜라이더");
                    // Ray�� ���� ��ü�� Player���
                    if (hit.transform.name == "Player")
                    {
                        // ���Ͱ� �÷��̾ ���� ���� �ٰ����� ��.
                        transform.position = Vector3.Lerp(transform.position, hit.transform.position, 0.01f);
                    }
                }
            }
        }

    }
    void Update()
    {
        Sight();   
    }
}

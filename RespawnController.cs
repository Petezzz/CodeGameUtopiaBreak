using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    // �ӹǹ Object ����ͧ������������� Scene
    public int desiredObjectCount = 50;

    // Prefab �ͧ Object ����ͧ��� Respawn
    public GameObject objectPrefab;

    // ���˹觷���ͧ������ Object �����Դ
    public Transform[] respawnPoints;

    // ��ǧ���������ҧ��� Respawn (˹���: �Թҷ�)
    public float respawnInterval = 1f;

    // ���������Ѻ�Ǻ������ Respawn
    private float respawnTimer = 0f;

    void Start()
    {
        // ���ҧ Object ����ͧ������ú�ӹǹ㹵͹�������
        for (int i = 0; i < desiredObjectCount; i++)
        {
            SpawnObject();
        }
    }

    void Update()
    {
        // �Ѻ�ӹǹ Object � Scene ���� Tag ����˹�
        int objectCount = GameObject.FindGameObjectsWithTag("RespawnTag").Length;

        // ��Ҩӹǹ Object ���¡��Ҩӹǹ����ͧ���
        if (objectCount < desiredObjectCount)
        {
            // �������Ңͧ��ǨѺ����
            respawnTimer += Time.deltaTime;

            // ��ҵ�ǨѺ���Ҷ֧���ҷ���˹�
            if (respawnTimer >= respawnInterval)
            {
                // �ӡ�� Respawn Object
                SpawnObject();

                // ���絵�ǨѺ����
                respawnTimer = 0f;
            }
        }
    }

    void SpawnObject()
    {
        // ���͡���˹� Respawn Ẻ�����ҡ����˹�
        Transform respawnPoint = respawnPoints[Random.Range(0, respawnPoints.Length)];

        // ���ҧ Object ��������˹觷���˹�
        Instantiate(objectPrefab, respawnPoint.position, respawnPoint.rotation);
    }
}

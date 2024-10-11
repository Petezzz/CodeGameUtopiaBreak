using UnityEngine;

public class FloorController : MonoBehaviour
{
    public GameObject prefab; // �ѵ�ط���ͧ����ҧ�ǧ���
    public int numberOfObjects = 10; // �ӹǹ�ѵ�ط����ҧ
    public float radius = 5f; // ����բͧǧ���

    void Start()
    {
        PlaceObjectsInCircle();
    }

    void PlaceObjectsInCircle()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            // �ӹǳ����ͧ�ѵ�����Ъ���ǧ���
            float angle = i * Mathf.PI * 2 / numberOfObjects;
            // �ӹǳ���˹觢ͧ�ѵ���ǧ���
            Vector3 position = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            // ���ҧ�ѵ�ط����˹觷��ӹǳ��
            Instantiate(prefab, position, Quaternion.identity, transform);
        }
    }
}

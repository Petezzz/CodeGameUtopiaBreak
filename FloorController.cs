using UnityEngine;

public class FloorController : MonoBehaviour
{
    public GameObject prefab; // วัตถุที่ต้องการวางในวงกลม
    public int numberOfObjects = 10; // จำนวนวัตถุที่จะวาง
    public float radius = 5f; // รัศมีของวงกลม

    void Start()
    {
        PlaceObjectsInCircle();
    }

    void PlaceObjectsInCircle()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            // คำนวณมุมของวัตถุแต่ละชิ้นในวงกลม
            float angle = i * Mathf.PI * 2 / numberOfObjects;
            // คำนวณตำแหน่งของวัตถุในวงกลม
            Vector3 position = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            // สร้างวัตถุที่ตำแหน่งที่คำนวณได้
            Instantiate(prefab, position, Quaternion.identity, transform);
        }
    }
}

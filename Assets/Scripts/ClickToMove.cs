using UnityEngine;
using UnityEngine.InputSystem;

public class ClickToMove : MonoBehaviour
{
    private bool isCarrying = false;
    private Collider2D MyCollider;
    private TileSpawner spawner;

    void Start()
    {
      MyCollider= GetComponent<Collider2D>();

        spawner = FindFirstObjectByType<TileSpawner>();
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            mousePos.z = -Camera.main.transform.position.z;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            if (!isCarrying)
            {
                //つかみ判定
                if (MyCollider == Physics2D.OverlapPoint(worldPos))
                {
                    isCarrying = true;
                    transform.SetParent(null);
                    spawner.SpawnAllEmptySlots();
                }
            }
            else
            {
                //離した時
                isCarrying = false;
            }
        }

        //掴んでいる間はマウスに追従
        if (isCarrying)
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            mousePos.z= -Camera.main.transform.position.z;
            transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        }
    }
}

using UnityEngine;

public class PatternMoving : MonoBehaviour
{
    [SerializeField] [Range(0, 3)] private float range = 0f;
    [SerializeField] [Range(0, 3)] private float speed = 1f;
    private Vector3 pos = default;
    private enum Direction {right,left};
    Direction dir = Direction.right;

    void Start()
    {
        Init();
    }

    private void Init()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Check();
        switch (dir)
        {
            case (Direction.left):
                MoveLeft();
                break;
            case (Direction.right):
                MoveRight();
                break;
            default:
                break;
        }
    }
    void Check()
    {
        if(transform.position.x <= pos.x - range)
        {
            dir = Direction.right;
        }
        if (transform.position.x >= pos.x + range)
        {
            dir = Direction.left;
        }
    }
    void MoveLeft()
    {
        transform.Translate(new Vector3(-speed, 0, 0) * Time.deltaTime);
    }
    void MoveRight()
    {
        transform.Translate(new Vector3(speed, 0, 0) * Time.deltaTime);
    }
}

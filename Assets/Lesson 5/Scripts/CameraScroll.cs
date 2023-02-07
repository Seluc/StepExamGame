using lesson2;
using UnityEngine;
using Unity.Mathematics;

public class CameraScroll : MonoBehaviour
{
    public float ScrollSensitivity = 2f;
    public float MinY = 1f;
    public float MaxY = 10f;

    public PlayerMovement PlayerMovementComponent;

    private float _currentY;

    void Start()
    {
        _currentY = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        var scroll = Input.GetAxis("Mouse ScrollWheel") * ScrollSensitivity;
        var pos = transform.localPosition;

        if (scroll > 0f)
        { //forward
            if(transform.localPosition.y > MinY)
            {
                _currentY = pos.y - scroll;
                transform.localPosition = new Vector3(pos.x, _currentY, pos.z);
            }
        }
        else if (scroll < 0f)
        { //backward
            if (transform.localPosition.y < MaxY)
            {
                _currentY = pos.y - scroll;
                transform.localPosition = new Vector3(pos.x, _currentY, pos.z);
            }
        }

        PlayerMovementComponent.downLimit = math.remap(MinY, MaxY, 50, 80, _currentY);
    }
}

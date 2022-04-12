using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private float Speed;
    public float MaxMovement = 2.0f;

    private string InputAxis;
    
    void Start()
    {
      switch(GameManager.ControlInput)
        {
            case "Keys":
                InputAxis = "Horizontal";
                Speed = 2.0f;
                break;
            case "Mouse":
                InputAxis = "Mouse X";
                Speed = 100.0f;
                break;
            default:
                break;
        }
    }
    
    void Update()
    {
        float input = Input.GetAxis(InputAxis);

        Vector3 pos = transform.position;
        pos.x += input * Speed * Time.deltaTime;

        if (pos.x > MaxMovement)
            pos.x = MaxMovement;
        else if (pos.x < -MaxMovement)
            pos.x = -MaxMovement;

        transform.position = pos;
    }
}

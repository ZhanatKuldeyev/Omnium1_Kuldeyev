using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementComponent : MonoBehaviour, IMovable
{
    private Character character;
    private CharacterData characterData;
    private float speed;

    public float Speed 
    { 
        get => speed; 
        set 
        { 
            if (value < 0)
                return; 
            speed = value;
        }
    }

    public void Initialize(CharacterData characterData)
    {
        this.characterData = characterData;
        speed = characterData.DefaultSpeed;
    }

    public void Move(Vector3 direction)
    {
        if (characterData == null)
        {
            Debug.LogError("characterData is NULL in CharacterMovementComponent!");
            return;
        }

        if (characterData.CharacterController == null)
        {
            Debug.LogError("CharacterMovementComponent: CharacterController is NULL!");
            return;
        }
        Debug.Log($"Move() called. CharacterController: {characterData.CharacterController}, DefaultSpeed: {characterData.DefaultSpeed}");
        characterData.CharacterController.Move(direction * characterData.DefaultSpeed * Time.deltaTime);

        if (direction == Vector3.zero)
        {
            character.AnimationComponent.SetValue("Movement", 0);
            return;
        }
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Vector3 move = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
        characterData.CharacterController.Move(move * Speed * Time.deltaTime);
        character.AnimationComponent.SetValue("Movement", direction.magnitude);
    }

    public void Rotation(Vector3 direction)
    {
        if (direction == Vector3.zero)
            return;

        float smooth = 0.1f;
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(characterData.CharacterTransform.eulerAngles.y, targetAngle, ref smooth, smooth);
    }

}

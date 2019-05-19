using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    private IPlayer player;
    private float deltaTime;
    private float verInput, horInput;

    void Start()
    {
        player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        getInputs();
        updatePlayerInputs();
        deltaTime = Time.fixedDeltaTime;
        player.tick(deltaTime);
    }


    private void getInputs()
    {
        verInput = Input.GetAxis("Vertical");
        horInput = Input.GetAxis("Horizontal");
    }

    private void updatePlayerInputs()
    {
        float magnitude = Mathf.Abs(verInput) + Mathf.Abs(horInput);
        magnitude = Mathf.Clamp01(magnitude);
        player.updateInputs(verInput, horInput, magnitude);
    }


}

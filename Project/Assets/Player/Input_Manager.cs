using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Manager : MonoBehaviour
{

    Player player;

    float deltaTime;
    float verInput, horInput;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        player.Init();
    }

    private void FixedUpdate()
    {
        getInput();
        updatePlayerInputs();
        deltaTime = Time.fixedDeltaTime;
        player.tick(deltaTime);
    }


    private void getInput()
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

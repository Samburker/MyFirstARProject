using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class touchInput : MonoBehaviour
{
    [SerializeField] private TMP_Text debugtext; // debug text object

    //singletap function needs to be an Unity event
    // for the CallbackContext to work

    public void SingleTap(InputAction.CallbackContext ctx)
    {
        //Check that input was completed
        if(ctx.phase == InputActionPhase.Performed)
        {
            var touchPos = ctx.ReadValue<Vector2>(); // read position
            debugtext.text  = touchPos.ToString(); // Write to debug field
        }
    }

}

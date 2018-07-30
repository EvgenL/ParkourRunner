using Invector.CharacterController;

public class KeyboardInput : vThirdPersonInput
{

    public bool LockStrafe = false;
    public bool SprintAlways = true;

    protected override void MoveCharacter()
    {
        cc.input.y = 1;

        cc.input.x = !LockStrafe ? horizontalInput.GetAxis() : 0; //Если LoskStrafe то игнорируем ввод поворотов

        // update oldInput to compare with current Input if keepDirection is true
        if (!keepDirection)
            oldInput = cc.input;
    }
    /*
    protected override void SprintInput()
    {
        if (!SprintAlways)
            base.SprintInput();
        else
            cc.Sprint(true);
    }*/
}

using WeChatWASM;
using WeChatWASM.Extension;

public class RuntimeDebugConsolCommandWXInput : Inputs
{
    protected override void OnConfirm(OnKeyboardInputListenerResult v)
    {
        input.onValidateInput(v.value, 0, '\n');
        base.OnConfirm(v);
    }

    protected override void OnComplete(OnKeyboardInputListenerResult v)
    {
        input.onValidateInput(v.value, 0, '\n');
        base.OnComplete(v);
    }
}
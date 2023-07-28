using System;
using UnityEngine;
using System.Collections;
using WeChatWASM;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace WeChatWASM.Extension
{
    public class Inputs : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
    {
        public InputField input;
        private bool isShowKeyboad = false;

        public bool confirmHold;
        public string confirmType = "go";
        public string defaultValue = "";
        public double maxLength = 200;
        public bool multiple;

        public void OnPointerClick(PointerEventData eventData)
        {
            ShowKeyboad();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // 隐藏输入法
            if (!input.isFocused)
            {
                HideKeyboad();
            }
        }

        protected virtual void OnInput(OnKeyboardInputListenerResult v)
        {
            Debug.Log("onInput");
            Debug.Log(v.value);
            if (input.isFocused)
            {
                if (input.text != v.value)
                {
                    input.text = v.value;
                    input.onValueChanged?.Invoke(v.value);
                }
            }
        }

        protected virtual void OnConfirm(OnKeyboardInputListenerResult v)
        {
            // 输入法confirm回调
            Debug.Log("onConfirm");
            Debug.Log(v.value);
            HideKeyboad();
        }

        protected virtual void OnComplete(OnKeyboardInputListenerResult v)
        {
            // 输入法complete回调
            Debug.Log("OnComplete");
            Debug.Log(v.value);
            HideKeyboad();
        }

        private void ShowKeyboad()
        {
            if (!isShowKeyboad)
            {
                try
                {
                    WX.ShowKeyboard(new ShowKeyboardOption()
                    {
                        defaultValue = defaultValue,
                        maxLength = maxLength,
                        confirmType = confirmType,
                        multiple = multiple,
                        confirmHold = confirmHold,
                    });

                    //绑定回调
                    WX.OnKeyboardConfirm(OnConfirm);
                    WX.OnKeyboardComplete(OnComplete);
                    WX.OnKeyboardInput(OnInput);
                    isShowKeyboad = true;
                }
                catch (Exception e)
                {
                    // Debug.LogException(e);
                }
            }
        }

        private void HideKeyboad()
        {
            if (isShowKeyboad)
            {
                try
                {
                    WX.HideKeyboard(new HideKeyboardOption());
                    //删除掉相关事件监听
                    WX.OffKeyboardInput(OnInput);
                    WX.OffKeyboardConfirm(OnConfirm);
                    WX.OffKeyboardComplete(OnComplete);
                    isShowKeyboad = false;
                }
                catch (Exception e)
                {
                    // Debug.LogException(e);
                }
            }
        }
    }
}
// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Entities/Movement.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Movement : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Movement()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Movement"",
    ""maps"": [
        {
            ""name"": ""Move"",
            ""id"": ""019a4154-0930-4e36-b7dc-1c854c53ddca"",
            ""actions"": [
                {
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""376beef2-e208-4614-9610-bec60c413ea7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""abba05be-37be-483c-9bc2-12f020298213"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""75b18c4c-b6a5-40f1-836e-ef23a7e7d606"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""571e9243-e5fa-47e8-bb2e-e74b8cf613fc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""c5c1f230-b76f-4cab-b790-3077c11cdcd2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a4563a19-eaef-4972-b489-a2e0b97c307c"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eded15a0-105b-40e7-a9cf-9a254d8a4583"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""71e51ac9-1901-43df-8725-b1e837b438c3"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""add0c33b-df18-426c-8278-9f43fc2b7bbf"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d70339a0-eec4-47ec-8683-e36f35cb7937"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Move
        m_Move = asset.FindActionMap("Move", throwIfNotFound: true);
        m_Move_Up = m_Move.FindAction("Up", throwIfNotFound: true);
        m_Move_Down = m_Move.FindAction("Down", throwIfNotFound: true);
        m_Move_Left = m_Move.FindAction("Left", throwIfNotFound: true);
        m_Move_Right = m_Move.FindAction("Right", throwIfNotFound: true);
        m_Move_Select = m_Move.FindAction("Select", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Move
    private readonly InputActionMap m_Move;
    private IMoveActions m_MoveActionsCallbackInterface;
    private readonly InputAction m_Move_Up;
    private readonly InputAction m_Move_Down;
    private readonly InputAction m_Move_Left;
    private readonly InputAction m_Move_Right;
    private readonly InputAction m_Move_Select;
    public struct MoveActions
    {
        private @Movement m_Wrapper;
        public MoveActions(@Movement wrapper) { m_Wrapper = wrapper; }
        public InputAction @Up => m_Wrapper.m_Move_Up;
        public InputAction @Down => m_Wrapper.m_Move_Down;
        public InputAction @Left => m_Wrapper.m_Move_Left;
        public InputAction @Right => m_Wrapper.m_Move_Right;
        public InputAction @Select => m_Wrapper.m_Move_Select;
        public InputActionMap Get() { return m_Wrapper.m_Move; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MoveActions set) { return set.Get(); }
        public void SetCallbacks(IMoveActions instance)
        {
            if (m_Wrapper.m_MoveActionsCallbackInterface != null)
            {
                @Up.started -= m_Wrapper.m_MoveActionsCallbackInterface.OnUp;
                @Up.performed -= m_Wrapper.m_MoveActionsCallbackInterface.OnUp;
                @Up.canceled -= m_Wrapper.m_MoveActionsCallbackInterface.OnUp;
                @Down.started -= m_Wrapper.m_MoveActionsCallbackInterface.OnDown;
                @Down.performed -= m_Wrapper.m_MoveActionsCallbackInterface.OnDown;
                @Down.canceled -= m_Wrapper.m_MoveActionsCallbackInterface.OnDown;
                @Left.started -= m_Wrapper.m_MoveActionsCallbackInterface.OnLeft;
                @Left.performed -= m_Wrapper.m_MoveActionsCallbackInterface.OnLeft;
                @Left.canceled -= m_Wrapper.m_MoveActionsCallbackInterface.OnLeft;
                @Right.started -= m_Wrapper.m_MoveActionsCallbackInterface.OnRight;
                @Right.performed -= m_Wrapper.m_MoveActionsCallbackInterface.OnRight;
                @Right.canceled -= m_Wrapper.m_MoveActionsCallbackInterface.OnRight;
                @Select.started -= m_Wrapper.m_MoveActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_MoveActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_MoveActionsCallbackInterface.OnSelect;
            }
            m_Wrapper.m_MoveActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Up.started += instance.OnUp;
                @Up.performed += instance.OnUp;
                @Up.canceled += instance.OnUp;
                @Down.started += instance.OnDown;
                @Down.performed += instance.OnDown;
                @Down.canceled += instance.OnDown;
                @Left.started += instance.OnLeft;
                @Left.performed += instance.OnLeft;
                @Left.canceled += instance.OnLeft;
                @Right.started += instance.OnRight;
                @Right.performed += instance.OnRight;
                @Right.canceled += instance.OnRight;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
            }
        }
    }
    public MoveActions @Move => new MoveActions(this);
    public interface IMoveActions
    {
        void OnUp(InputAction.CallbackContext context);
        void OnDown(InputAction.CallbackContext context);
        void OnLeft(InputAction.CallbackContext context);
        void OnRight(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
    }
}

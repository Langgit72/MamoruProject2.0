//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Input Assets/PlayerControl.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerControl : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControl()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControl"",
    ""maps"": [
        {
            ""name"": ""MotionControl"",
            ""id"": ""c65b0c98-aac3-4539-ae23-cd69a73cb8ef"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""d01e23e2-364b-4e1e-a4ee-2c173a54fb87"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""77148698-58a8-439a-b369-3f7ab977abd0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Fall"",
                    ""type"": ""Button"",
                    ""id"": ""5ea7e297-e2a8-4a13-b50a-a27b25833790"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8418654e-61aa-4fca-a732-70b0a6c2ffe3"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d4d9df4f-0e89-4279-961b-76a543b13450"",
                    ""path"": ""<SwitchProControllerHID>/buttonSouth"",
                    ""interactions"": ""Tap,Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""23369410-2a10-4069-898b-d4ce527bd8ca"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""a37e6657-7f5d-4f24-a9e1-46eeed0b5564"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""2b6124a3-fd9f-439f-9205-95b062ed0e73"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""bab8ba83-6beb-4735-807d-d76dcf172e23"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""1e93c846-b2b3-46f6-b9bf-0e131b865778"",
                    ""path"": ""<SwitchProControllerHID>/leftStick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""b7df5d7e-12b7-4e06-9daf-29dd7ad97cc1"",
                    ""path"": ""<SwitchProControllerHID>/leftStick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""05ebbb9d-f16b-4d54-8498-666ee95b14c7"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fall"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""AbilityControl"",
            ""id"": ""6832e3df-e261-4c65-a553-89d824ef704a"",
            ""actions"": [
                {
                    ""name"": ""Chi"",
                    ""type"": ""Button"",
                    ""id"": ""df59e16d-0921-4343-a401-420f15729ad2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""c42770e0-e3f2-460b-a9f1-366cebb699c4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""18f1dd17-5fcb-4ee6-bade-e9d451b44e0e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""7a33308f-fcaf-4c6d-ab5d-5fbf1899164d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""OpenInventory"",
                    ""type"": ""Button"",
                    ""id"": ""b9c969e2-64c3-4a61-9979-fa35daad854d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""700e2420-621a-49aa-be03-0327d968fc2c"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Chi"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0e89b2d4-c2e9-4697-b77a-770d1944d6a8"",
                    ""path"": ""<Keyboard>/rightShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9e020b34-d62f-4ad4-85de-70492e8884c0"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""627a078b-8f5c-4bab-83ca-07422a66570a"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0ffe1e53-14e6-479a-97cc-f333eebfab8a"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bd21f807-e0b0-414b-ae0f-8d35e6523dca"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // MotionControl
        m_MotionControl = asset.FindActionMap("MotionControl", throwIfNotFound: true);
        m_MotionControl_Jump = m_MotionControl.FindAction("Jump", throwIfNotFound: true);
        m_MotionControl_Move = m_MotionControl.FindAction("Move", throwIfNotFound: true);
        m_MotionControl_Fall = m_MotionControl.FindAction("Fall", throwIfNotFound: true);
        // AbilityControl
        m_AbilityControl = asset.FindActionMap("AbilityControl", throwIfNotFound: true);
        m_AbilityControl_Chi = m_AbilityControl.FindAction("Chi", throwIfNotFound: true);
        m_AbilityControl_Sprint = m_AbilityControl.FindAction("Sprint", throwIfNotFound: true);
        m_AbilityControl_Attack = m_AbilityControl.FindAction("Attack", throwIfNotFound: true);
        m_AbilityControl_Interact = m_AbilityControl.FindAction("Interact", throwIfNotFound: true);
        m_AbilityControl_OpenInventory = m_AbilityControl.FindAction("OpenInventory", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // MotionControl
    private readonly InputActionMap m_MotionControl;
    private IMotionControlActions m_MotionControlActionsCallbackInterface;
    private readonly InputAction m_MotionControl_Jump;
    private readonly InputAction m_MotionControl_Move;
    private readonly InputAction m_MotionControl_Fall;
    public struct MotionControlActions
    {
        private @PlayerControl m_Wrapper;
        public MotionControlActions(@PlayerControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_MotionControl_Jump;
        public InputAction @Move => m_Wrapper.m_MotionControl_Move;
        public InputAction @Fall => m_Wrapper.m_MotionControl_Fall;
        public InputActionMap Get() { return m_Wrapper.m_MotionControl; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MotionControlActions set) { return set.Get(); }
        public void SetCallbacks(IMotionControlActions instance)
        {
            if (m_Wrapper.m_MotionControlActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_MotionControlActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_MotionControlActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_MotionControlActionsCallbackInterface.OnJump;
                @Move.started -= m_Wrapper.m_MotionControlActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_MotionControlActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_MotionControlActionsCallbackInterface.OnMove;
                @Fall.started -= m_Wrapper.m_MotionControlActionsCallbackInterface.OnFall;
                @Fall.performed -= m_Wrapper.m_MotionControlActionsCallbackInterface.OnFall;
                @Fall.canceled -= m_Wrapper.m_MotionControlActionsCallbackInterface.OnFall;
            }
            m_Wrapper.m_MotionControlActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Fall.started += instance.OnFall;
                @Fall.performed += instance.OnFall;
                @Fall.canceled += instance.OnFall;
            }
        }
    }
    public MotionControlActions @MotionControl => new MotionControlActions(this);

    // AbilityControl
    private readonly InputActionMap m_AbilityControl;
    private IAbilityControlActions m_AbilityControlActionsCallbackInterface;
    private readonly InputAction m_AbilityControl_Chi;
    private readonly InputAction m_AbilityControl_Sprint;
    private readonly InputAction m_AbilityControl_Attack;
    private readonly InputAction m_AbilityControl_Interact;
    private readonly InputAction m_AbilityControl_OpenInventory;
    public struct AbilityControlActions
    {
        private @PlayerControl m_Wrapper;
        public AbilityControlActions(@PlayerControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @Chi => m_Wrapper.m_AbilityControl_Chi;
        public InputAction @Sprint => m_Wrapper.m_AbilityControl_Sprint;
        public InputAction @Attack => m_Wrapper.m_AbilityControl_Attack;
        public InputAction @Interact => m_Wrapper.m_AbilityControl_Interact;
        public InputAction @OpenInventory => m_Wrapper.m_AbilityControl_OpenInventory;
        public InputActionMap Get() { return m_Wrapper.m_AbilityControl; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(AbilityControlActions set) { return set.Get(); }
        public void SetCallbacks(IAbilityControlActions instance)
        {
            if (m_Wrapper.m_AbilityControlActionsCallbackInterface != null)
            {
                @Chi.started -= m_Wrapper.m_AbilityControlActionsCallbackInterface.OnChi;
                @Chi.performed -= m_Wrapper.m_AbilityControlActionsCallbackInterface.OnChi;
                @Chi.canceled -= m_Wrapper.m_AbilityControlActionsCallbackInterface.OnChi;
                @Sprint.started -= m_Wrapper.m_AbilityControlActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_AbilityControlActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_AbilityControlActionsCallbackInterface.OnSprint;
                @Attack.started -= m_Wrapper.m_AbilityControlActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_AbilityControlActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_AbilityControlActionsCallbackInterface.OnAttack;
                @Interact.started -= m_Wrapper.m_AbilityControlActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_AbilityControlActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_AbilityControlActionsCallbackInterface.OnInteract;
                @OpenInventory.started -= m_Wrapper.m_AbilityControlActionsCallbackInterface.OnOpenInventory;
                @OpenInventory.performed -= m_Wrapper.m_AbilityControlActionsCallbackInterface.OnOpenInventory;
                @OpenInventory.canceled -= m_Wrapper.m_AbilityControlActionsCallbackInterface.OnOpenInventory;
            }
            m_Wrapper.m_AbilityControlActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Chi.started += instance.OnChi;
                @Chi.performed += instance.OnChi;
                @Chi.canceled += instance.OnChi;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @OpenInventory.started += instance.OnOpenInventory;
                @OpenInventory.performed += instance.OnOpenInventory;
                @OpenInventory.canceled += instance.OnOpenInventory;
            }
        }
    }
    public AbilityControlActions @AbilityControl => new AbilityControlActions(this);
    public interface IMotionControlActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnFall(InputAction.CallbackContext context);
    }
    public interface IAbilityControlActions
    {
        void OnChi(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnOpenInventory(InputAction.CallbackContext context);
    }
}
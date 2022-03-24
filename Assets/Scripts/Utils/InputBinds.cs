// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Utils/InputBinds.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputBinds : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputBinds()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputBinds"",
    ""maps"": [
        {
            ""name"": ""Character"",
            ""id"": ""e8fc03ca-239f-4654-ad81-b90fec170d2d"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""baef648b-4179-4612-8ae7-366a396c82cf"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Mouse"",
                    ""type"": ""Value"",
                    ""id"": ""1f2abe45-49b5-4b4c-a161-9f9ae581b1f4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""1027ad0b-3188-44b1-80a6-71c7fe3b5878"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""e450b07f-08e1-4aa8-9714-15bcc00645c4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""68aca178-996d-4341-bf1f-68c5e37a38a5"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Mouse and keyboard "",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""707de6d9-5485-42a5-bbaf-45fa9a64a0d4"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""316c1120-2c87-4f2e-a394-7fb4b0cc2c3e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and keyboard "",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""3a248a0a-bc32-4fa1-8b32-8383b92e776f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and keyboard "",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""973646cc-394e-488b-ab31-dd7ab8813d0f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and keyboard "",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e16a6632-5309-49a8-8ffc-5b59b3de95de"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and keyboard "",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6faa1a12-b206-46c9-9cc0-37f34e0fc43a"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Mouse and keyboard "",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b6520a14-781f-4ae5-acde-a86351f6246a"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and keyboard "",
                    ""action"": ""Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""37780691-eda0-4e50-921e-b39c5c543f3f"",
            ""actions"": [
                {
                    ""name"": ""PauseOrUnpause"",
                    ""type"": ""Button"",
                    ""id"": ""e152c725-21a1-4ec9-9d0a-e80b822e1fce"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4a0838b3-cf75-47e7-a5dc-a1ae6e8cc1e4"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Mouse and keyboard "",
                    ""action"": ""PauseOrUnpause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Mouse and keyboard "",
            ""bindingGroup"": ""Mouse and keyboard "",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Character
        m_Character = asset.FindActionMap("Character", throwIfNotFound: true);
        m_Character_Move = m_Character.FindAction("Move", throwIfNotFound: true);
        m_Character_Mouse = m_Character.FindAction("Mouse", throwIfNotFound: true);
        m_Character_Attack = m_Character.FindAction("Attack", throwIfNotFound: true);
        m_Character_Dash = m_Character.FindAction("Dash", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_PauseOrUnpause = m_UI.FindAction("PauseOrUnpause", throwIfNotFound: true);
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

    // Character
    private readonly InputActionMap m_Character;
    private ICharacterActions m_CharacterActionsCallbackInterface;
    private readonly InputAction m_Character_Move;
    private readonly InputAction m_Character_Mouse;
    private readonly InputAction m_Character_Attack;
    private readonly InputAction m_Character_Dash;
    public struct CharacterActions
    {
        private @InputBinds m_Wrapper;
        public CharacterActions(@InputBinds wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Character_Move;
        public InputAction @Mouse => m_Wrapper.m_Character_Mouse;
        public InputAction @Attack => m_Wrapper.m_Character_Attack;
        public InputAction @Dash => m_Wrapper.m_Character_Dash;
        public InputActionMap Get() { return m_Wrapper.m_Character; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterActions set) { return set.Get(); }
        public void SetCallbacks(ICharacterActions instance)
        {
            if (m_Wrapper.m_CharacterActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnMove;
                @Mouse.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnMouse;
                @Mouse.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnMouse;
                @Mouse.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnMouse;
                @Attack.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnAttack;
                @Dash.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnDash;
            }
            m_Wrapper.m_CharacterActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Mouse.started += instance.OnMouse;
                @Mouse.performed += instance.OnMouse;
                @Mouse.canceled += instance.OnMouse;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
            }
        }
    }
    public CharacterActions @Character => new CharacterActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_PauseOrUnpause;
    public struct UIActions
    {
        private @InputBinds m_Wrapper;
        public UIActions(@InputBinds wrapper) { m_Wrapper = wrapper; }
        public InputAction @PauseOrUnpause => m_Wrapper.m_UI_PauseOrUnpause;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @PauseOrUnpause.started -= m_Wrapper.m_UIActionsCallbackInterface.OnPauseOrUnpause;
                @PauseOrUnpause.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnPauseOrUnpause;
                @PauseOrUnpause.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnPauseOrUnpause;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PauseOrUnpause.started += instance.OnPauseOrUnpause;
                @PauseOrUnpause.performed += instance.OnPauseOrUnpause;
                @PauseOrUnpause.canceled += instance.OnPauseOrUnpause;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    private int m_MouseandkeyboardSchemeIndex = -1;
    public InputControlScheme MouseandkeyboardScheme
    {
        get
        {
            if (m_MouseandkeyboardSchemeIndex == -1) m_MouseandkeyboardSchemeIndex = asset.FindControlSchemeIndex("Mouse and keyboard ");
            return asset.controlSchemes[m_MouseandkeyboardSchemeIndex];
        }
    }
    public interface ICharacterActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnMouse(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnPauseOrUnpause(InputAction.CallbackContext context);
    }
}

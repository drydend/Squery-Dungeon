// GENERATED AUTOMATICALLY FROM 'Assets/InputBinds.inputactions'

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
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""1027ad0b-3188-44b1-80a6-71c7fe3b5878"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChangeCharacterToTriangle"",
                    ""type"": ""Button"",
                    ""id"": ""7b4fc2ba-fff2-4093-88fd-6a2419252c8e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChangeCharacterToSquare"",
                    ""type"": ""Button"",
                    ""id"": ""b37e9e7b-6c89-4ff7-807e-1603eda0d60f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChangeCharacterToPentagon"",
                    ""type"": ""Button"",
                    ""id"": ""b4bdd395-8970-484f-ba31-73e2d4df0a42"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChangeCharacterToOctagon"",
                    ""type"": ""Button"",
                    ""id"": ""020e95ef-0bcf-46f0-ab42-a680834d6caf"",
                    ""expectedControlType"": ""Button"",
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
                    ""name"": """",
                    ""id"": ""8e20b802-3ad3-4449-9fd3-9db1254bf404"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and keyboard "",
                    ""action"": ""ChangeCharacterToTriangle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f187424b-ecf1-4039-adf7-aa472cb409e1"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and keyboard "",
                    ""action"": ""ChangeCharacterToSquare"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8f2aad35-73da-4722-94aa-6a96536162e0"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and keyboard "",
                    ""action"": ""ChangeCharacterToPentagon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1baf009e-9d6d-4a27-9179-1b3563c749bf"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and keyboard "",
                    ""action"": ""ChangeCharacterToOctagon"",
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
        m_Character_Attack = m_Character.FindAction("Attack", throwIfNotFound: true);
        m_Character_ChangeCharacterToTriangle = m_Character.FindAction("ChangeCharacterToTriangle", throwIfNotFound: true);
        m_Character_ChangeCharacterToSquare = m_Character.FindAction("ChangeCharacterToSquare", throwIfNotFound: true);
        m_Character_ChangeCharacterToPentagon = m_Character.FindAction("ChangeCharacterToPentagon", throwIfNotFound: true);
        m_Character_ChangeCharacterToOctagon = m_Character.FindAction("ChangeCharacterToOctagon", throwIfNotFound: true);
        m_Character_Mouse = m_Character.FindAction("Mouse", throwIfNotFound: true);
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
    private readonly InputAction m_Character_Attack;
    private readonly InputAction m_Character_ChangeCharacterToTriangle;
    private readonly InputAction m_Character_ChangeCharacterToSquare;
    private readonly InputAction m_Character_ChangeCharacterToPentagon;
    private readonly InputAction m_Character_ChangeCharacterToOctagon;
    private readonly InputAction m_Character_Mouse;
    public struct CharacterActions
    {
        private @InputBinds m_Wrapper;
        public CharacterActions(@InputBinds wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Character_Move;
        public InputAction @Attack => m_Wrapper.m_Character_Attack;
        public InputAction @ChangeCharacterToTriangle => m_Wrapper.m_Character_ChangeCharacterToTriangle;
        public InputAction @ChangeCharacterToSquare => m_Wrapper.m_Character_ChangeCharacterToSquare;
        public InputAction @ChangeCharacterToPentagon => m_Wrapper.m_Character_ChangeCharacterToPentagon;
        public InputAction @ChangeCharacterToOctagon => m_Wrapper.m_Character_ChangeCharacterToOctagon;
        public InputAction @Mouse => m_Wrapper.m_Character_Mouse;
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
                @Attack.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnAttack;
                @ChangeCharacterToTriangle.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnChangeCharacterToTriangle;
                @ChangeCharacterToTriangle.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnChangeCharacterToTriangle;
                @ChangeCharacterToTriangle.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnChangeCharacterToTriangle;
                @ChangeCharacterToSquare.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnChangeCharacterToSquare;
                @ChangeCharacterToSquare.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnChangeCharacterToSquare;
                @ChangeCharacterToSquare.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnChangeCharacterToSquare;
                @ChangeCharacterToPentagon.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnChangeCharacterToPentagon;
                @ChangeCharacterToPentagon.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnChangeCharacterToPentagon;
                @ChangeCharacterToPentagon.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnChangeCharacterToPentagon;
                @ChangeCharacterToOctagon.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnChangeCharacterToOctagon;
                @ChangeCharacterToOctagon.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnChangeCharacterToOctagon;
                @ChangeCharacterToOctagon.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnChangeCharacterToOctagon;
                @Mouse.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnMouse;
                @Mouse.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnMouse;
                @Mouse.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnMouse;
            }
            m_Wrapper.m_CharacterActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @ChangeCharacterToTriangle.started += instance.OnChangeCharacterToTriangle;
                @ChangeCharacterToTriangle.performed += instance.OnChangeCharacterToTriangle;
                @ChangeCharacterToTriangle.canceled += instance.OnChangeCharacterToTriangle;
                @ChangeCharacterToSquare.started += instance.OnChangeCharacterToSquare;
                @ChangeCharacterToSquare.performed += instance.OnChangeCharacterToSquare;
                @ChangeCharacterToSquare.canceled += instance.OnChangeCharacterToSquare;
                @ChangeCharacterToPentagon.started += instance.OnChangeCharacterToPentagon;
                @ChangeCharacterToPentagon.performed += instance.OnChangeCharacterToPentagon;
                @ChangeCharacterToPentagon.canceled += instance.OnChangeCharacterToPentagon;
                @ChangeCharacterToOctagon.started += instance.OnChangeCharacterToOctagon;
                @ChangeCharacterToOctagon.performed += instance.OnChangeCharacterToOctagon;
                @ChangeCharacterToOctagon.canceled += instance.OnChangeCharacterToOctagon;
                @Mouse.started += instance.OnMouse;
                @Mouse.performed += instance.OnMouse;
                @Mouse.canceled += instance.OnMouse;
            }
        }
    }
    public CharacterActions @Character => new CharacterActions(this);
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
        void OnAttack(InputAction.CallbackContext context);
        void OnChangeCharacterToTriangle(InputAction.CallbackContext context);
        void OnChangeCharacterToSquare(InputAction.CallbackContext context);
        void OnChangeCharacterToPentagon(InputAction.CallbackContext context);
        void OnChangeCharacterToOctagon(InputAction.CallbackContext context);
        void OnMouse(InputAction.CallbackContext context);
    }
}

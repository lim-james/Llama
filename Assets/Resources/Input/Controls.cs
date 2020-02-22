// GENERATED AUTOMATICALLY FROM 'Assets/Resources/Input/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""78b8ed0a-b2ef-41f7-a06f-e7991d1d99d4"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""7a03acc2-f561-4ac2-a78c-f5e44d432afc"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pick up"",
                    ""type"": ""Button"",
                    ""id"": ""d6168b1b-513e-4bc2-80b9-5960c4ae226c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Switch"",
                    ""type"": ""Button"",
                    ""id"": ""fea73ced-cfbd-442e-b199-05392a3ca0cd"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Release"",
                    ""type"": ""Button"",
                    ""id"": ""38d9bb78-2a8c-4ca1-90e5-60caefc86124"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Consume"",
                    ""type"": ""Button"",
                    ""id"": ""2b0ae069-d844-4c78-a1df-a1f897bf7f3a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""12e1b1e7-2b50-4691-a63c-aa8a9ca20c46"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""260a0ada-5aa4-41e6-ac20-79c8abaf7e52"",
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
                    ""id"": ""c1ea87b1-7b2b-4ae2-ad51-d7f32d72ac7c"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""eac6c9c4-cc32-4c83-9dc4-420e6b77c60b"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""88d62219-efb6-4645-810a-303a426670ca"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""449e3a49-33b5-413a-9827-24fb9eee0634"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""65b91bee-c97b-4c2f-92bf-c1be29f26587"",
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
                    ""id"": ""b947ed9e-628d-474f-a761-dffef6af0c86"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""86ebeabc-c2ea-4e1a-b44c-811a19a5def4"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4e1561be-6344-4e58-ad9c-bb51a7c431c7"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f4b8b1a8-9584-470b-b954-c295fae17bc1"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""978a88a1-aa9b-4ac6-8a8b-bb7f80009564"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Pick up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fb0f3279-f68c-4503-accd-f882622b958d"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Pick up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Bumpers"",
                    ""id"": ""1cb4654d-edd0-42e4-b422-4ff309548541"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""34a89768-ff23-4bad-85e0-1eced3e9ca81"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""faaa883d-607e-41e8-87c5-948ae8d1ecc3"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""88164147-5266-4870-b96e-b25ec8caf431"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""6cf49bd4-c14c-45a3-997c-07ff8d9f1536"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""a08da050-5609-4936-9ce6-3f0d5a14b990"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""4062ab9e-fc19-4f65-8c97-ade56ce8c69e"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Release"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5a4fa472-e2c3-4113-9314-623b17f39cf3"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Release"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""869cf043-d4b0-4ea9-a1a0-e2847d51e2f1"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Consume"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1cbfc823-785a-408b-a90b-6717b4bb7475"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Consume"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Lobby"",
            ""id"": ""03bb0b3b-fc78-48a1-bc02-864c9f757983"",
            ""actions"": [
                {
                    ""name"": ""Switch (Character)"",
                    ""type"": ""Button"",
                    ""id"": ""6bcd6cbe-99ea-4cea-a25b-2067ce609b16"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Switch (Team)"",
                    ""type"": ""Button"",
                    ""id"": ""2ee9517e-7ec7-497a-8c86-345bd1f672f5"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Show Stats"",
                    ""type"": ""Button"",
                    ""id"": ""966806c5-08a3-4c0d-a4af-acf4049caf56"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Joystick (Controller)"",
                    ""id"": ""7167f947-ae05-489a-b3b7-66075e8f5cab"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch (Character)"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""3357f5f8-6ac2-4077-8a76-a2a9bbe6e756"",
                    ""path"": ""<Joystick>/stick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch (Character)"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""a6de73b2-2407-4a5d-8e3b-6e32786f588f"",
                    ""path"": ""<Joystick>/stick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch (Character)"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Bumper (Controller)"",
                    ""id"": ""bdd8bb0e-63e8-4939-8629-b752ebd02b36"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch (Character)"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""100d4c48-caf6-4640-8ed0-38a8c59eacff"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Switch (Character)"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""15170d9b-117a-4d33-a8b5-04d08e04265d"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Switch (Character)"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""DPad (Controller)"",
                    ""id"": ""bbbfe613-6394-4636-8248-7d742df663a9"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch (Character)"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""94da15ca-792f-40d1-98ee-ff190ff8e7ef"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Switch (Character)"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""ae0e040b-aaeb-43ab-a2c6-fbfda68a40a4"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Switch (Character)"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows (Keyboard)"",
                    ""id"": ""23192b29-dcee-4f17-824d-85be081174b2"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch (Character)"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""8dd70f93-26a7-45ba-98a6-6ae61d8fc100"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Switch (Character)"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""f8c3e78e-36ed-47e8-b839-694fec2683ec"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Switch (Character)"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""AD (Keyboard)"",
                    ""id"": ""57bcb9d5-4f16-469d-94f0-50219b194c8c"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch (Character)"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""f6467d52-f8a6-476f-a61f-7f497571c85c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Switch (Character)"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""c18f8e95-4353-451f-8942-39ea54a94b40"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Switch (Character)"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""WS (Keyboard)"",
                    ""id"": ""7808754a-a82b-402c-8f58-463e185bee89"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch (Team)"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""9db27ec1-ebd4-45d6-8d37-32b4a0fd63b2"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch (Team)"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""60445629-95f7-4084-b9fc-d50eb10d1d33"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch (Team)"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows (Keyboard)"",
                    ""id"": ""8cfa794d-e2d3-4bb5-9abe-ac16543e6095"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch (Team)"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""3366edbd-3318-40b7-8e07-440aafd49447"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch (Team)"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""a6d47fa8-f752-42f6-9867-0c22436b631c"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch (Team)"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""DPad (Controller)"",
                    ""id"": ""d2c879ab-4638-4295-a23e-fcbbc5407acf"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch (Team)"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""e3f8bc3f-a1e5-487e-8f42-70fe27992876"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch (Team)"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""fd91a926-3460-4bb4-9516-444eed75646d"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch (Team)"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Joystick (Controller)"",
                    ""id"": ""d66c851d-4406-407c-8ac3-fa9977043547"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch (Team)"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""3b395565-d856-4041-9d1a-dc716e0e7865"",
                    ""path"": ""<Joystick>/stick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch (Team)"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""a7167337-c424-4c9f-aa57-c1202ee1e15e"",
                    ""path"": ""<Joystick>/stick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch (Team)"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""865e53ce-4b0b-4865-8738-5869dab8e62f"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Show Stats"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": []
        },
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": []
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Pickup = m_Player.FindAction("Pick up", throwIfNotFound: true);
        m_Player_Switch = m_Player.FindAction("Switch", throwIfNotFound: true);
        m_Player_Release = m_Player.FindAction("Release", throwIfNotFound: true);
        m_Player_Consume = m_Player.FindAction("Consume", throwIfNotFound: true);
        // Lobby
        m_Lobby = asset.FindActionMap("Lobby", throwIfNotFound: true);
        m_Lobby_SwitchCharacter = m_Lobby.FindAction("Switch (Character)", throwIfNotFound: true);
        m_Lobby_SwitchTeam = m_Lobby.FindAction("Switch (Team)", throwIfNotFound: true);
        m_Lobby_ShowStats = m_Lobby.FindAction("Show Stats", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Pickup;
    private readonly InputAction m_Player_Switch;
    private readonly InputAction m_Player_Release;
    private readonly InputAction m_Player_Consume;
    public struct PlayerActions
    {
        private @InputMaster m_Wrapper;
        public PlayerActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Pickup => m_Wrapper.m_Player_Pickup;
        public InputAction @Switch => m_Wrapper.m_Player_Switch;
        public InputAction @Release => m_Wrapper.m_Player_Release;
        public InputAction @Consume => m_Wrapper.m_Player_Consume;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Pickup.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPickup;
                @Pickup.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPickup;
                @Pickup.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPickup;
                @Switch.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitch;
                @Switch.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitch;
                @Switch.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitch;
                @Release.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRelease;
                @Release.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRelease;
                @Release.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRelease;
                @Consume.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnConsume;
                @Consume.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnConsume;
                @Consume.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnConsume;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Pickup.started += instance.OnPickup;
                @Pickup.performed += instance.OnPickup;
                @Pickup.canceled += instance.OnPickup;
                @Switch.started += instance.OnSwitch;
                @Switch.performed += instance.OnSwitch;
                @Switch.canceled += instance.OnSwitch;
                @Release.started += instance.OnRelease;
                @Release.performed += instance.OnRelease;
                @Release.canceled += instance.OnRelease;
                @Consume.started += instance.OnConsume;
                @Consume.performed += instance.OnConsume;
                @Consume.canceled += instance.OnConsume;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Lobby
    private readonly InputActionMap m_Lobby;
    private ILobbyActions m_LobbyActionsCallbackInterface;
    private readonly InputAction m_Lobby_SwitchCharacter;
    private readonly InputAction m_Lobby_SwitchTeam;
    private readonly InputAction m_Lobby_ShowStats;
    public struct LobbyActions
    {
        private @InputMaster m_Wrapper;
        public LobbyActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @SwitchCharacter => m_Wrapper.m_Lobby_SwitchCharacter;
        public InputAction @SwitchTeam => m_Wrapper.m_Lobby_SwitchTeam;
        public InputAction @ShowStats => m_Wrapper.m_Lobby_ShowStats;
        public InputActionMap Get() { return m_Wrapper.m_Lobby; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(LobbyActions set) { return set.Get(); }
        public void SetCallbacks(ILobbyActions instance)
        {
            if (m_Wrapper.m_LobbyActionsCallbackInterface != null)
            {
                @SwitchCharacter.started -= m_Wrapper.m_LobbyActionsCallbackInterface.OnSwitchCharacter;
                @SwitchCharacter.performed -= m_Wrapper.m_LobbyActionsCallbackInterface.OnSwitchCharacter;
                @SwitchCharacter.canceled -= m_Wrapper.m_LobbyActionsCallbackInterface.OnSwitchCharacter;
                @SwitchTeam.started -= m_Wrapper.m_LobbyActionsCallbackInterface.OnSwitchTeam;
                @SwitchTeam.performed -= m_Wrapper.m_LobbyActionsCallbackInterface.OnSwitchTeam;
                @SwitchTeam.canceled -= m_Wrapper.m_LobbyActionsCallbackInterface.OnSwitchTeam;
                @ShowStats.started -= m_Wrapper.m_LobbyActionsCallbackInterface.OnShowStats;
                @ShowStats.performed -= m_Wrapper.m_LobbyActionsCallbackInterface.OnShowStats;
                @ShowStats.canceled -= m_Wrapper.m_LobbyActionsCallbackInterface.OnShowStats;
            }
            m_Wrapper.m_LobbyActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SwitchCharacter.started += instance.OnSwitchCharacter;
                @SwitchCharacter.performed += instance.OnSwitchCharacter;
                @SwitchCharacter.canceled += instance.OnSwitchCharacter;
                @SwitchTeam.started += instance.OnSwitchTeam;
                @SwitchTeam.performed += instance.OnSwitchTeam;
                @SwitchTeam.canceled += instance.OnSwitchTeam;
                @ShowStats.started += instance.OnShowStats;
                @ShowStats.performed += instance.OnShowStats;
                @ShowStats.canceled += instance.OnShowStats;
            }
        }
    }
    public LobbyActions @Lobby => new LobbyActions(this);
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnPickup(InputAction.CallbackContext context);
        void OnSwitch(InputAction.CallbackContext context);
        void OnRelease(InputAction.CallbackContext context);
        void OnConsume(InputAction.CallbackContext context);
    }
    public interface ILobbyActions
    {
        void OnSwitchCharacter(InputAction.CallbackContext context);
        void OnSwitchTeam(InputAction.CallbackContext context);
        void OnShowStats(InputAction.CallbackContext context);
    }
}

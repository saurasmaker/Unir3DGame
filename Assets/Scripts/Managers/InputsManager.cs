using Patterns.Singletons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputsManager : SingletonMonoBehaviourPersistent<InputsManager>
{

    public static Dictionary<ActionId, string> ActionsNames = new Dictionary<ActionId, string>()
    {
        { ActionId.MOVE, "Move"},
        { ActionId.INTERACT, "Interact"},
        { ActionId.JUMP, "Jump"},
        { ActionId.DASH, "Dash"}
    };


    [SerializeField]
    private PlayerInput _playerInput;

    public PlayerInput ThisPlayerInput
    {
        get
        {
            if (_playerInput == null && !TryGetComponent(out _playerInput))
                Debug.LogWarning("There is no Player input selected in Inputs Manager...");

            return _playerInput;
        }
    }




    #region Overworld Actions
    public InputAction MoveAction
    {
        get { return ThisPlayerInput.actions[ActionsNames[ActionId.MOVE]]; }
    }

    public InputAction InteractAction
    {
        get { return ThisPlayerInput.actions[ActionsNames[ActionId.INTERACT]]; }
    }

    public InputAction JumpAction
    {
        get { return ThisPlayerInput.actions[ActionsNames[ActionId.JUMP]]; }
    }

    public InputAction DashAction
    {
        get { return ThisPlayerInput.actions[ActionsNames[ActionId.DASH]]; }
    }
    #endregion


    #region Battle Actions

    #endregion

    #region Methods
    public static void SetBindingToAction(InputAction action, InputBinding binding)
    {

    }
    #endregion


    #region Monobehaviour Methods
    protected override void OnAwake()
    {
        base.OnAwake();
    }
    #endregion


    public static string InputActionToString(InputAction action)
    {
        string s = ""
            + "toString = " + action.ToString() + ";\n"
            + "name = " + action.name + ";\n"
            + "processors = " + action.processors + ";\n"
            + "interactions = " + action.interactions + ";\n";

        for (int i = 0; i < action.bindings.Count; ++i)
            s += InputBindingToString(action.bindings[i]);

        return s;
    }

    public static string InputBindingToString(InputBinding binding)
    {
        return "\n--- BINDING ---\n"
        + "id = " + binding.id + ";\n"
        + "name = " + binding.name + ";\n"
        + "action = " + binding.action + ";\n"
        + "hasOverrides = " + binding.hasOverrides + ";\n"
        + "isComposite = " + binding.isComposite + ";\n"
        + "isPartOfComposite = " + binding.isPartOfComposite + ";\n"
        + "groups = " + binding.groups + ";\n"
        + "toString = " + binding.ToString() + ";\n"

        + "path = " + binding.path + ";\n"
        + "processors = " + binding.processors + ";\n"
        + "interactions" + binding.interactions + ";\n"

        + "overridePath = " + binding.overridePath + ";\n"
        + "overrideInteractions = " + binding.overrideInteractions + ";\n"
        + "overrideProcessors = " + binding.overrideProcessors + ";\n"

        + "effectivePath = " + binding.effectivePath + ";\n"
        + "effectiveInteractions = " + binding.effectiveInteractions + ";\n"
        + "effectiveProcessors = " + binding.effectiveProcessors + ";\n"
        ;
    }




    public enum ActionId
    {
        MOVE,
        INTERACT,
        JUMP,
        DASH
    }
}
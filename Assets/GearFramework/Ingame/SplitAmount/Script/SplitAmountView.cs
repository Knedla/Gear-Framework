using GearFramework.Runtime;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace GearFramework.Ingame.SplitAmount
{
    #region Reminder
    /*
    
    for example: when the quantityPerUnit is 5, user cannot enter 10 in the InputField => after entering the first digit, the selectedAmount is recalculated to 5 as the minimum amount
    
    this is how it was done in the World of Warcraft => input field is disabled

       (1 Stack)            (4 Stacks)
     <            >       <            >
        5 Total              20 Total
     Okay    Cancel       Okay    Cancel

    */
    #endregion

    public class SplitAmountView : Runtime.PlayerInput.EscapeKeyStack.Item // implement if needed: SplitAmountView need to be closed if its caller closes
    {
        public static bool RequestToOpen { get { return Runtime.PlayerInput.Manager.KeyPressed(Config.RequestToOpenKey_1, Config.RequestToOpenKey_2); } }
        
        public static SplitAmountView Instance { get; private set; }
        
        [SerializeField] protected RectTransform RectTransform;
        [SerializeField] protected InputField InputField;
        [SerializeField] protected Button PreviousButton;
        [SerializeField] protected Button NextButton;
        [SerializeField] protected Button OkButton;
        [SerializeField] protected Button CancelButton;
        
        public bool IsActive { get { return gameObject.activeSelf; } }
        
        enum State
        {
            First,
            Middle,
            Last
        }
        
        State state;
        int quantityPerUnit;
        int maxAmount;
        int selectedAmount;
        Action<int> submit;
        
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            gameObject.SetActive(false);
            SetListeners();
        }
        
        void SetListeners()
        {
            InputField.onValueChanged.AddListener(OnValueChanged);
            PreviousButton.onClick.AddListener(OnClick_Previous);
            NextButton.onClick.AddListener(OnClick_Next);
            OkButton.onClick.AddListener(Submit);
            CancelButton.onClick.AddListener(Cancel);
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(Runtime.PlayerInput.Config.GlobalEnterKey_1) || Input.GetKeyDown(Runtime.PlayerInput.Config.GlobalEnterKey_2))
                Submit();
        }
        
        public void Open(int maxAmount, Action<int> submit, RectTransform alignRelativeToRectTransform, AlignmentOption alignmentOption) => Open(1, maxAmount, submit, alignRelativeToRectTransform, alignmentOption);
        
        public void Open(int quantityPerUnit, int maxAmount, Action<int> submit, RectTransform alignRelativeToRectTransform, AlignmentOption alignmentOption)
        {
            this.quantityPerUnit = quantityPerUnit;
            this.maxAmount = maxAmount;
            this.submit = submit;

            RectTransform.AlignRelativeTo(alignRelativeToRectTransform, alignmentOption, Config.HorizontalEdgeAlignmentOffset, Config.VerticalEdgeAlignmentOffset);
            SetInitialState();
            gameObject.SetActive(true);
            SetInputField();
        }
        
        void SetInitialState()
        {
            selectedAmount = quantityPerUnit;
            SetText(selectedAmount);
            State_First();
        }
       
        void SetInputField()
        {
            if (quantityPerUnit == 1)
            {
                InputField.interactable = true;
                InputField.ActivateInputField();
            }
            else
                InputField.interactable = false;
        }
        
        void OnValueChanged(string value)
        {
            int currentAmount;

            if (!int.TryParse(value, out currentAmount))
                return;

            if (currentAmount < quantityPerUnit)
            {
                SetText(quantityPerUnit);
                return;
            }
            else if (currentAmount > maxAmount)
            {
                SetText(maxAmount);
                return;
            }

            selectedAmount = currentAmount;
            SetButtonStates();
        }
        
        void SetButtonStates()
        {
            if (selectedAmount == quantityPerUnit)
            {
                if (state != State.First)
                    State_First();
            }
            else if (selectedAmount == maxAmount)
            {
                if (state != State.Last)
                    State_Last();
            }
            else
            {
                if (state != State.Middle)
                    State_Midle();
            }
        }
        
        void State_First()
        {
            state = State.First;
            PreviousButton.interactable = false;
            if (!NextButton.interactable) NextButton.interactable = true;
        }
        
        void State_Last()
        {
            state = State.Last;
            if (!PreviousButton.interactable) PreviousButton.interactable = true;
            NextButton.interactable = false;
        }
        
        void State_Midle()
        {
            state = State.Middle;
            if (!PreviousButton.interactable) PreviousButton.interactable = true;
            if (!NextButton.interactable) NextButton.interactable = true;
        }
        
        void OnClick_Previous()
        {
            if (selectedAmount > quantityPerUnit)
                SetText(selectedAmount - quantityPerUnit);
        }
        
        void OnClick_Next()
        {
            if (selectedAmount < maxAmount)
                SetText(selectedAmount + quantityPerUnit);
        }
        
        void SetText(int amount) => InputField.text = amount.ToString();
        
        void Submit()
        {
            gameObject.SetActive(false);
            submit(selectedAmount);
        }
        
        public void Cancel() => gameObject.SetActive(false);
        
        public override void EscapeKeyPressed() => Cancel();
    }
}

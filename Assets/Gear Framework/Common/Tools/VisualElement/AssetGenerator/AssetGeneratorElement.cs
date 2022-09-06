#if UNITY_EDITOR

using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GearFramework.Common.EditorOnly
{
    public abstract class AssetGeneratorElement<T> : SettingsElement where T : ScriptableObject
    {
        static readonly string newTextFieldName = "asset-generator-element-new-text-field";
        static readonly string addButtonName = "asset-generator-element-add-button";
        static readonly string scrollViewName = "asset-generator-element-scroll-view";
        static readonly string updateButtonName = "asset-generator-element-update-button";
        static readonly string deleteButtonName = "asset-generator-element-delete-button";
        static readonly string selectButtonName = "asset-generator-element-select-button";
        static readonly string refreshButtonName = "asset-generator-element-refresh-button";
        static readonly string messageLabelName = "asset-generator-element-message-label";

        // reminder #1: need to be here, otherwise reset the log message when the new element gets focus - OnFocusTextField trigger after button action

        #region Error Message Logic
        enum ErrorCode
        {
            NaN,
            AssetAlreadyExist,
            AssetDoesNotExist,
            UnexpectedValueLength,
            InvalidCharacter
        }
        void ShowErrorMessage(string message = "")
        {
            switch (activeErrorCode)
            {
                case ErrorCode.NaN:
                    Log(message);
                    return;
                case ErrorCode.UnexpectedValueLength:
                    Log($"Length must be between {MinValueLength} and {MaxValueLength}");
                    return;
                case ErrorCode.InvalidCharacter:
                    Log("File name contains invalid characters!");
                    return;
                case ErrorCode.AssetAlreadyExist:
                    Log(GetGenerucPathMessage(message, "already exists"));
                    return;
                case ErrorCode.AssetDoesNotExist:
                    Log(GetGenerucPathMessage(message, "does not exist"));
                    return;
                default:
                    return;
            }
        }
        string GetGenerucPathMessage(string filePath, string message) => $"Asset {Path.GetFileNameWithoutExtension(filePath)} {message}!\n{PathUtil.GetRelativePath(filePath)}\nClick the Refresh button to get the current status of the list.";
        #endregion

        protected override string UxmlRelativeFilePath => PathUtil.GetCallerRelativeFilePath_WithUxmlFileExtension();

        protected virtual string WindowTitle => ObjectNames.NicifyVariableName(typeof(T).Name);
        protected virtual int MinValueLength => 3;
        protected virtual int MaxValueLength => 50;
        protected abstract string NewTextFieldTooltip { get; }
        protected abstract string AssetDirectoryPath { get; }

        TextField newTextField;
        Button addButton;
        Button updateButton;
        Button deleteButton;
        Button selectButton;
        Button refreshButton;
        ScrollView scrollView;
        Label messageLabel;

        Dictionary<TextField, string> textFields;
        TextField selectedTextField;
        ErrorCode activeErrorCode;

        public override void Init()
        {
            SetData();
            PopulateScrollView();
            SetInitialState();
            OnFocusTextField(new FocusEvent() { target = newTextField }); // TODO: for some reason the command newTextField.Focus() does not work when run from the Init method
        }

        void SetData()
        {
            newTextField = this.Q<TextField>(newTextFieldName);
            newTextField.tooltip = NewTextFieldTooltip;
            RegisterTextField(newTextField);

            addButton = this.Q<Button>(addButtonName);
            addButton.RegisterCallback<ClickEvent>(evt => AddButton_OnClick(evt));
            updateButton = this.Q<Button>(updateButtonName);
            updateButton.RegisterCallback<ClickEvent>(evt => UpdateButton_OnClick(evt));
            deleteButton = this.Q<Button>(deleteButtonName);
            deleteButton.RegisterCallback<ClickEvent>(evt => DeleteButton_OnClick(evt));
            selectButton = this.Q<Button>(selectButtonName);
            selectButton.RegisterCallback<ClickEvent>(evt => SelectButton_OnClick(evt));
            refreshButton = this.Q<Button>(refreshButtonName);
            refreshButton.RegisterCallback<ClickEvent>(evt => RefreshButton_OnClick(evt));

            scrollView = this.Q<ScrollView>(scrollViewName);
            messageLabel = this.Q<Label>(messageLabelName);
        }

        void RegisterTextField(TextField textField)
        {
            textField.RegisterValueChangedCallback(ValueChangedCallback);
            textField.RegisterCallback<KeyDownEvent>(OnKeyDownEvent);
            textField.RegisterCallback<FocusEvent>(OnFocusTextField);
        }

        void UnregisterTextField(TextField textField)
        {
            textField.UnregisterValueChangedCallback(ValueChangedCallback);
            textField.UnregisterCallback<KeyDownEvent>(OnKeyDownEvent);
            textField.UnregisterCallback<FocusEvent>(OnFocusTextField);
        }

        void ValueChangedCallback(ChangeEvent<string> changeEvent)
        {
            SetSubmitButton();
            ShowErrorMessage();
        }
        
        void SetSubmitButton()
        {
            bool enabled = CanExecuteActionButton();

            if (selectedTextField == newTextField)
                addButton.SetEnabled(enabled);
            else
                updateButton.SetEnabled(enabled);
        }

        bool CanExecuteActionButton()
        {
            if (selectedTextField.value.Trim() == string.Empty || (selectedTextField != newTextField && selectedTextField.value == Path.GetFileNameWithoutExtension(textFields[selectedTextField])))
            {
                activeErrorCode = ErrorCode.NaN;
                return false;
            }

            if (selectedTextField.value.Length < MinValueLength || selectedTextField.value.Length > MaxValueLength)
            {
                activeErrorCode = ErrorCode.UnexpectedValueLength;
                return false;
            }

            if (!(selectedTextField.value.IndexOfAny(Path.GetInvalidFileNameChars()) < 0))
            {
                activeErrorCode = ErrorCode.InvalidCharacter;
                return false;
            }

            activeErrorCode = ErrorCode.NaN;
            return true;
        }

        void OnKeyDownEvent(KeyDownEvent evt) // thoughts: up down arrows navigation?
        {
            switch (evt.keyCode)
            {
                case KeyCode.Return:
                case KeyCode.KeypadEnter:
                    if (selectedTextField == newTextField)
                        AddButton_OnClick(null);
                    else
                        UpdateButton_OnClick(null);
                    return;
                case KeyCode.Delete:
                    if (selectedTextField != newTextField)
                        DeleteButton_OnClick(null);
                    return;
                //case KeyCode.Home: // note: not happiest solution
                //    newTextField.Focus();
                //    return;
                default:
                    return;
            }
        }

        void OnFocusTextField(FocusEvent evt)
        {
            if (selectedTextField != null && selectedTextField != newTextField)
            {
                string path;
                if (textFields.TryGetValue(selectedTextField, out path))
                    selectedTextField.SetValueWithoutNotify(Path.GetFileNameWithoutExtension(path));
            }

            bool showErrorMessage = selectedTextField != evt.target;
            
            SelectTextField((TextField)evt.target);
            
            if (showErrorMessage)
                ShowErrorMessage();
        }

        void SelectTextField(TextField textField)
        {
            selectedTextField = textField;
            SetButtons();
        }

        void SetButtons()
        {
            SetSubmitButton();

            if (selectedTextField == newTextField)
            {
                updateButton.SetEnabled(false);
                deleteButton.SetEnabled(false);
                selectButton.SetEnabled(false);
            }
            else
            {
                addButton.SetEnabled(false);
                deleteButton.SetEnabled(true);
                selectButton.SetEnabled(true);
            }
        }

        void PopulateScrollView()
        {
            if (textFields == null)
                textFields = new Dictionary<TextField, string>();

            DirectoryUtil.CreateDirectory(AssetDirectoryPath);

            foreach (string item in Directory.GetFiles(AssetDirectoryPath, $"*.asset"))
                AddTextField(item);
        }

        void AddTextField(string filePath)
        {
            TextField textField = new TextField();
            textField.value = Path.GetFileNameWithoutExtension(filePath);
            textField.tooltip = filePath;

            RegisterTextField(textField);

            textFields.Add(textField, filePath);
            scrollView.Add(textField);
        }

        void SetInitialState()
        {
            newTextField.SetValueWithoutNotify(string.Empty);
            newTextField.Focus();
        }

        void AddButton_OnClick(ClickEvent evt)
        {
            if (!CanExecuteActionButtonValidation())
                return;

            string filePath = PathUtil.AddAssetFileExtension(Path.Combine(AssetDirectoryPath, selectedTextField.value));

            if (!AssetAlreadyExistValidation(filePath))
                return;

            ScriptableObjectUtil.CreateInstanceAndSaveToFileSystem<T>(filePath);
            AddTextField(filePath);
            SetInitialState();
            Log($"{Path.GetFileNameWithoutExtension(filePath)} added!");
        }

        void UpdateButton_OnClick(ClickEvent evt)
        {
            if (!CanExecuteActionButtonValidation())
                return;

            string filePath = PathUtil.AddAssetFileExtension(Path.Combine(AssetDirectoryPath, selectedTextField.value));

            if (!AssetAlreadyExistValidation(filePath))
                return;

            if (!AssetDoesNotExistValidation())
                return;

            AssetDatabase.RenameAsset(PathUtil.GetRelativePath(textFields[selectedTextField]), selectedTextField.value);
            //AssetDatabase.Refresh();

            textFields[selectedTextField] = filePath;
            selectedTextField.tooltip = filePath;

            selectedTextField.Focus();
            Log($"{Path.GetFileNameWithoutExtension(filePath)} updated!");
        }
        
        void DeleteButton_OnClick(ClickEvent evt)
        {
            if (!AssetDoesNotExistValidation())
                return;

            TextField textFieldToRemove = selectedTextField;

            FileUtil.DeleteFile(textFields[textFieldToRemove]);
            //AssetDatabase.Refresh();

            FocusConvenienceItem();
            UnregisterTextField(textFieldToRemove);
            textFields.Remove(textFieldToRemove);
            scrollView.Remove(textFieldToRemove);

            Log($"{textFieldToRemove.value} deleted!");
        }

        void FocusConvenienceItem()
        {
            bool brakeOnNext = false;
            VisualElement itemToFocus = null;
            foreach (VisualElement item in scrollView.Children())
            {
                if (brakeOnNext)
                {
                    itemToFocus = item;
                    break;
                }

                if (selectedTextField == item)
                    brakeOnNext = true;
                else
                    itemToFocus = item;
            }

            if (itemToFocus == null)
            {
                selectedTextField = newTextField; // reminder #1
                SetInitialState();
            }
            else
            {
                selectedTextField = (TextField)itemToFocus; // reminder #1
                itemToFocus.Focus();
            }
        }

        void SelectButton_OnClick(ClickEvent evt)
        {
            if (!AssetDoesNotExistValidation())
                return;

            Selection.activeObject = AssetDatabase.LoadAssetAtPath<T>(PathUtil.GetRelativePath(textFields[selectedTextField]));
            selectedTextField.Focus();
            Log($"{selectedTextField.value} selected!");
        }

        void RefreshButton_OnClick(ClickEvent evt)
        {
            if (selectedTextField == null)
                selectedTextField = newTextField;

            string path;
            textFields.TryGetValue(selectedTextField, out path);

            UnregisterTextFields();

            textFields.Clear();
            scrollView.Clear();

            PopulateScrollView();

            KeyValuePair<TextField, string> keyValuePair = textFields.Where(s => s.Value == path).FirstOrDefault();
            if (keyValuePair.Key != null)
            {
                selectedTextField = keyValuePair.Key; // reminder #1
                selectedTextField.Focus();
            }
            else
            {
                selectedTextField = newTextField; // reminder #1
                SetInitialState();
            }
            Log("Refresh done!");
        }

        bool CanExecuteActionButtonValidation()
        {
            if (!CanExecuteActionButton())
            {
                ShowErrorMessage();
                selectedTextField.Focus();
                return false;
            }

            return true;
        }

        bool AssetAlreadyExistValidation(string filePath)
        {
            if (File.Exists(filePath))
            {
                activeErrorCode = ErrorCode.AssetAlreadyExist;
                ShowErrorMessage(filePath);
                selectedTextField.Focus();
                return false;
            }

            return true;
        }

        bool AssetDoesNotExistValidation()
        {
            if (!File.Exists(textFields[selectedTextField]))
            {
                activeErrorCode = ErrorCode.AssetDoesNotExist;
                ShowErrorMessage(textFields[selectedTextField]);
                selectedTextField.Focus();
                return false;
            }

            return true;
        }

        //private void OnDisable()
        //{
        //    UnregisterTextField(newTextField);

        //    addButton.clicked -= AddButton_clicked;
        //    updateButton.clicked -= UpdateButton_clicked;
        //    deleteButton.clicked -= DeleteButton_clicked;
        //    selectButton.clicked -= SelectButton_clicked;
        //    refreshButton.clicked -= RefreshButton_clicked;

        //    UnregisterTextFields();
        //}

        void UnregisterTextFields()
        {
            foreach (KeyValuePair<TextField, string> item in textFields)
                UnregisterTextField(item.Key);
        }

        void Log(string message) => messageLabel.text = message;
    }
}
#endif

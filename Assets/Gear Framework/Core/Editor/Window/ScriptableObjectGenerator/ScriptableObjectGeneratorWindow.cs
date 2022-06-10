#if UNITY_EDITOR

using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityEditor
{
    public abstract class ScriptableObjectGeneratorWindow<T> : EditorWindow where T : ScriptableObject
    {
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
                    Log(string.Format("Length must be between {0} and {1}", MinValueLength, MaxValueLength));
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
        string GetGenerucPathMessage(string filePath, string message) => string.Format("Asset {0} {1}!\n{2}\nClick the Refresh button to get the current status of the list.", Path.GetFileNameWithoutExtension(filePath), message, PathExtension.GetRelativePath(filePath));
        #endregion

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
        TextField messageTextField;

        Dictionary<TextField, string> textFields;
        TextField selectedTextField;
        ErrorCode activeErrorCode;

        private void CreateGUI()
        {
            SetTemplateContainer();
            Initialize();
            PopulateScrollView();
            SetInitialState();
        }

        void SetTemplateContainer()
        {
            VisualTreeAsset visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(PathExtension.ChangeFileExtensionToUxml(PathExtension.GetCallerRelativeFilePath()));
            VisualElement visualElement = visualTreeAsset.Instantiate();
            rootVisualElement.Add(visualElement);
        }

        void Initialize()
        {
            titleContent = new GUIContent(WindowTitle);
            newTextField = rootVisualElement.Query<TextField>("newTextField").First();
            newTextField.tooltip = NewTextFieldTooltip;
            RegisterTextField(newTextField);
            addButton = rootVisualElement.Query<Button>("addButton").First();
            addButton.clicked += AddButton_clicked;
            updateButton = rootVisualElement.Query<Button>("updateButton").First();
            updateButton.clicked += UpdateButton_clicked;
            deleteButton = rootVisualElement.Query<Button>("deleteButton").First();
            deleteButton.clicked += DeleteButton_clicked;
            selectButton = rootVisualElement.Query<Button>("selectButton").First();
            selectButton.clicked += SelectButton_clicked; ;
            refreshButton = rootVisualElement.Query<Button>("refreshButton").First();
            refreshButton.clicked += RefreshButton_clicked;
            scrollView = rootVisualElement.Query<ScrollView>("scrollView").First();
            messageTextField = rootVisualElement.Query<TextField>("messageTextField").First();
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

        private void ValueChangedCallback(ChangeEvent<string> changeEvent)
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

        private void OnKeyDownEvent(KeyDownEvent evt) // thoughts: up down arrows navigation?
        {
            switch (evt.keyCode)
            {
                case KeyCode.Return:
                case KeyCode.KeypadEnter:
                    if (selectedTextField == newTextField)
                        AddButton_clicked();
                    else
                        UpdateButton_clicked();
                    return;
                case KeyCode.Delete:
                    if (selectedTextField != newTextField)
                        DeleteButton_clicked();
                    return;
                //case KeyCode.Home: // note: not happiest solution
                //    newTextField.Focus();
                //    return;
                default:
                    return;
            }
        }

        private void OnFocusTextField(FocusEvent evt)
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

            DirectoryExtension.CreateDirectoryIfDoesNotExists(AssetDirectoryPath);

            foreach (string item in Directory.GetFiles(AssetDirectoryPath, string.Concat(Const.Star, Const.AssetFileExtensionWithDot)))
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

        private void AddButton_clicked()
        {
            if (!CanExecuteActionButtonValidation())
                return;

            string filePath = PathExtension.AddAssetFileExtension(Path.Combine(AssetDirectoryPath, selectedTextField.value));

            if (!AssetAlreadyExistValidation(filePath))
                return;

            CreateAsset(filePath);
            AddTextField(filePath);
            SetInitialState();
            Log(string.Format("{0} added!", Path.GetFileNameWithoutExtension(filePath)));
        }

        void CreateAsset(string filePath)
        {
            T instance = CreateInstance<T>();
            instance.name = selectedTextField.value;

            AssetDatabase.CreateAsset(instance, PathExtension.GetRelativePath(filePath));
        }

        private void UpdateButton_clicked()
        {
            if (!CanExecuteActionButtonValidation())
                return;

            string filePath = PathExtension.AddAssetFileExtension(Path.Combine(AssetDirectoryPath, selectedTextField.value));

            if (!AssetAlreadyExistValidation(filePath))
                return;

            if (!AssetDoesNotExistValidation())
                return;

            AssetDatabase.RenameAsset(PathExtension.GetRelativePath(textFields[selectedTextField]), selectedTextField.value);
            AssetDatabase.Refresh();

            textFields[selectedTextField] = filePath;
            selectedTextField.tooltip = filePath;

            selectedTextField.Focus();
            Log(string.Format("{0} updated!", Path.GetFileNameWithoutExtension(filePath)));
        }
        
        private void DeleteButton_clicked()
        {
            if (!AssetDoesNotExistValidation())
                return;

            TextField textFieldToRemove = selectedTextField;

            FileExtension.DeleteFile(textFields[textFieldToRemove]);
            AssetDatabase.Refresh();

            FocusConvenienceItem();
            UnregisterTextField(textFieldToRemove);
            textFields.Remove(textFieldToRemove);
            scrollView.Remove(textFieldToRemove);

            Log(string.Format("{0} deleted!", textFieldToRemove.value));
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

        private void SelectButton_clicked()
        {
            if (!AssetDoesNotExistValidation())
                return;

            Selection.activeObject = AssetDatabase.LoadAssetAtPath<T>(PathExtension.GetRelativePath(textFields[selectedTextField]));
            selectedTextField.Focus();
            Log(string.Format("{0} selected!", selectedTextField.value));
        }

        private void RefreshButton_clicked()
        {
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

        private void OnDisable()
        {
            UnregisterTextField(newTextField);

            addButton.clicked -= AddButton_clicked;
            updateButton.clicked -= UpdateButton_clicked;
            deleteButton.clicked -= DeleteButton_clicked;
            selectButton.clicked -= SelectButton_clicked;
            refreshButton.clicked -= RefreshButton_clicked;

            UnregisterTextFields();
        }

        void UnregisterTextFields()
        {
            foreach (KeyValuePair<TextField, string> item in textFields)
                UnregisterTextField(item.Key);
        }

        void Log(string message) => messageTextField.value = message;
    }
}
#endif

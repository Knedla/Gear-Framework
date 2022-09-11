using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GearFramework.Runtime.Action
{
    public class ActionHistoryView : MonoBehaviour
    {
        [SerializeField] private Button CommitButton;
        [SerializeField] private Button UndoButton;
        [SerializeField] private Button RedoButton;
        Stack<IAction> undoActions;
        Stack<IAction> redoActions;
        
        private void Awake()
        {
            undoActions = new Stack<IAction>();
            redoActions = new Stack<IAction>();
            CommitButton.onClick.AddListener(Commit);
            UndoButton.onClick.AddListener(Undo);
            RedoButton.onClick.AddListener(Redo);
            DisableButtons();
        }
        
        void DisableButtons()
        {
            CommitButton.interactable = false;
            UndoButton.interactable = false;
            RedoButton.interactable = false;
        }
        
        public void Add(IAction action)
        {
            undoActions.Push(action);
            if (redoActions.Count > 0)
            {
                redoActions.Clear();
                RedoButton.interactable = false;
            }
            if (undoActions.Count == 1)
            {
                CommitButton.interactable = true;
                UndoButton.interactable = true;
            }
        }
        
        void Commit()
        {
            if (undoActions.Count > 0)
                undoActions.Clear();
            if (redoActions.Count > 0)
                redoActions.Clear();
            DisableButtons();
        }
        
        void Undo()
        {
            IAction action = undoActions.Pop();
            action.Rollback();
            redoActions.Push(action);
            if (undoActions.Count == 0)
            {
                CommitButton.interactable = false;
                UndoButton.interactable = false;
            }
            if (!RedoButton.interactable)
                RedoButton.interactable = true;
        }
        
        void Redo()
        {
            IAction action = redoActions.Pop();
            action.Execute();
            undoActions.Push(action);
            if (!UndoButton.interactable)
            {
                CommitButton.interactable = true;
                UndoButton.interactable = true;
            }
            if (redoActions.Count == 0)
                RedoButton.interactable = false;
        }
    }
}

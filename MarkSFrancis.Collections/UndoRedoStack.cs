using System;
using System.Collections.Generic;
using System.Linq;
using MarkSFrancis.Collections.Extensions;

namespace MarkSFrancis.Collections
{
    public class UndoRedoStack<T>
    {
        public T BaseItem { get; set; }

        private List<UndoRedoAction> MyStack { get; set; }

        private int ActiveStackIndex { get; set; }

        public bool UndoAvailable => ActiveStackIndex > 0;

        public bool RedoAvailable => ActiveStackIndex < MyStack.Count;

        public UndoRedoStack(T context)
        {
            BaseItem = context;
            MyStack = new List<UndoRedoAction>();
            ActiveStackIndex = 0;
        }

        public void PerformNewAction(Action<T> actionToPerform, Action<T> undoAction, string taskDescription = "")
        {
            while (MyStack.Count >= ActiveStackIndex)
            {
                MyStack = MyStack.CopyRange(0, ActiveStackIndex).ToList();
            }

            actionToPerform(BaseItem);

            MyStack.Add(new UndoRedoAction(actionToPerform, undoAction, taskDescription, taskDescription));

            ActiveStackIndex++;
        }

        public void PerformNewAction(Action<T> actionToPerform, Action<T> undoAction, string undoDescription, string redoDescription)
        {
            while (MyStack.Count > ActiveStackIndex)
            {
                MyStack = MyStack.CopyRange(0, ActiveStackIndex).ToList();
            }

            actionToPerform(BaseItem);

            MyStack.Add(new UndoRedoAction(actionToPerform, undoAction, undoDescription, redoDescription));

            ActiveStackIndex++;
        }

        public void Undo()
        {
            var actionToUndo = MyStack[ActiveStackIndex];
            actionToUndo.Undo(BaseItem);

            ActiveStackIndex--;
        }

        public void Redo()
        {
            var actionToRedo = MyStack[ActiveStackIndex];
            actionToRedo.Redo(BaseItem);

            ActiveStackIndex++;
        }

        public string[] GetUndoTaskDescriptions()
        {
            string[] stackDescriptions = new string[ActiveStackIndex];
            for (int index = 0; index < ActiveStackIndex; index++)
            {
                stackDescriptions[index] = MyStack[index].UndoDescription;
            }

            return stackDescriptions;
        }

        public string[] GetRedoTaskDescriptions()
        {
            string[] stackDescriptions = new string[MyStack.Count - ActiveStackIndex];
            for (int index = 0; index < MyStack.Count - ActiveStackIndex; index++)
            {
                stackDescriptions[index] = MyStack[ActiveStackIndex + index].RedoDescription;
            }

            return stackDescriptions;
        }

        private class UndoRedoAction
        {
            public Action<T> Redo { get; }

            public Action<T> Undo { get; }

            public string UndoDescription { get; }

            public string RedoDescription { get; }

            public UndoRedoAction(Action<T> redo, Action<T> undo, string undoDescription, string redoDescription)
            {
                this.Redo = redo;
                this.Undo = undo;
                this.UndoDescription = undoDescription;
                this.RedoDescription = redoDescription;
            }
        }
    }
}
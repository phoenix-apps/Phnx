using System;
using System.Collections.Generic;
using System.Linq;

namespace MarkSFrancis.Collections
{
    /// <summary>
    /// Provides a way to manage undoing and redoing actions
    /// </summary>
    public class UndoRedoStack
    {
        private Stack<UndoRedoAction> UndoActions { get; }

        private Stack<UndoRedoAction> RedoActions { get; }

        /// <summary>
        /// Whether there's an undo operation available
        /// </summary>
        public bool UndoAvailable => UndoActions.Count > 0;

        /// <summary>
        /// Whether there's a redo operation available
        /// </summary>
        public bool RedoAvailable => RedoActions.Count > 0;

        /// <summary>
        /// Create a new stack
        /// </summary>
        public UndoRedoStack()
        {
            UndoActions = new Stack<UndoRedoAction>();
            RedoActions = new Stack<UndoRedoAction>();
        }

        /// <summary>
        /// A new action has been performed that should be added to the stack
        /// </summary>
        /// <param name="undoAction">The action to be returned when <see cref="Undo"/> is called for this entry</param>
        /// <param name="redoAction">The action to be returned when <see cref="Redo"/> is called for this entry</param>
        /// <param name="actionDescription">The description of the action</param>
        /// <param name="clearRedo">Whether to clear all awaiting redo tasks</param>
        public void NewAction(Action undoAction, Action redoAction, string actionDescription = "", bool clearRedo = true)
        {
            if (clearRedo)
            {
                RedoActions.Clear();
            }

            UndoActions.Push(new UndoRedoAction(redoAction, undoAction, actionDescription, actionDescription));
        }

        /// <summary>
        /// A new action has been performed that should be added to the stack
        /// </summary>
        /// <param name="undoAction">The action to be returned when <see cref="Undo"/> is called for this entry</param>
        /// <param name="redoAction">The action to be returned when <see cref="Redo"/> is called for this entry</param>
        /// <param name="undoActionDescription">The description of the undo action</param>
        /// <param name="redoActionDescription">The description of the redo action</param>
        /// <param name="clearRedo">Whether to clear all awaiting redo tasks</param>
        public void NewAction(Action undoAction, Action redoAction, string undoActionDescription, string redoActionDescription, bool clearRedo = true)
        {
            if (clearRedo)
            {
                RedoActions.Clear();
            }

            UndoActions.Push(new UndoRedoAction(redoAction, undoAction, undoActionDescription, redoActionDescription));
        }

        /// <summary>
        /// Get the next undo action, and move it to the redo collection
        /// </summary>
        /// <returns>The next undo action</returns>
        public Action Undo()
        {
            var actionToUndo = UndoActions.Pop();
            RedoActions.Push(actionToUndo);
            return actionToUndo.Undo;
        }

        /// <summary>
        /// Get the next redo action, and move it to the undo collection
        /// </summary>
        /// <returns>The next redo action</returns>
        public Action Redo()
        {
            var actionToRedo = RedoActions.Pop();
            UndoActions.Push(actionToRedo);
            return actionToRedo.Redo;
        }

        /// <summary>
        /// Get all the awaiting undo task descriptions
        /// </summary>
        /// <returns>A collection of the undo task descriptions</returns>
        public IEnumerable<string> GetUndoTaskDescriptions()
        {
            return UndoActions.Select(a => a.UndoDescription);
        }

        /// <summary>
        /// Get all the awaiting redo task descriptions
        /// </summary>
        /// <returns>A collection of the redo task descriptions</returns>
        public IEnumerable<string> GetRedoTaskDescriptions()
        {
            return RedoActions.Select(a => a.RedoDescription);
        }

        private class UndoRedoAction
        {
            public Action Redo { get; }

            public Action Undo { get; }

            public string UndoDescription { get; }

            public string RedoDescription { get; }

            public UndoRedoAction(Action redo, Action undo, string undoDescription, string redoDescription)
            {
                Redo = redo;
                Undo = undo;
                UndoDescription = undoDescription;
                RedoDescription = redoDescription;
            }
        }
    }
}
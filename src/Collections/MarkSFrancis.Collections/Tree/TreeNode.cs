using MarkSFrancis.Collections.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace MarkSFrancis.Collections.Tree
{
    /// <summary>
    /// A node of data within a <see cref="Tree{T}"/>
    /// </summary>
    /// <typeparam name="T">The type of data in this node</typeparam>
    public class TreeNode<T>
    {
        /// <summary>
        /// Create a new <see cref="TreeNode{T}"/>
        /// </summary>
        public TreeNode()
        {
            Children = new List<TreeNode<T>>();
        }

        /// <summary>
        /// Create a new <see cref="TreeNode{T}"/> to represent a value
        /// </summary>
        /// <param name="value"></param>
        public TreeNode(T value)
        {
            Value = value;
            Children = new List<TreeNode<T>>();
        }

        /// <summary>
        /// The value contained within this node
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// The immediate children of this node
        /// </summary>
        public List<TreeNode<T>> Children { get; set; }

        /// <summary>
        /// Add a child to this tree node
        /// </summary>
        /// <param name="child">The child to add</param>
        /// <returns>The node after adding to this tree</returns>
        public TreeNode<T> AddChild(TreeNode<T> child)
        {
            Children.Add(child);

            return child;
        }

        /// <summary>
        /// Add a value as a child to this tree node
        /// </summary>
        /// <param name="child">The value to add</param>
        /// <returns>The value as a tree node</returns>
        public TreeNode<T> AddChild(T child)
        {
            var node = new TreeNode<T>(child);

            return AddChild(node);
        }

        /// <summary>
        /// Removes the given child node from this node
        /// </summary>
        /// <param name="childToRemove">The child node to remove</param>
        /// <param name="removeAllOccurences">Whether to remove all occurences, or just the first occurence</param>
        public void RemoveChild(TreeNode<T> childToRemove, bool removeAllOccurences = true)
        {
            Children = Children.Where(
                    c => c != childToRemove)
                .ToList();
        }

        /// <summary>
        /// Removes the given child node from this node
        /// </summary>
        /// <param name="childToRemove">The child node to remove</param>
        /// <param name="removeAllOccurences">Whether to remove all occurences, or just the first occurence</param>
        public void RemoveChild(T childToRemove, bool removeAllOccurences = true)
        {
            Children = Children.Where(
                    c => !c.Value.Equals(childToRemove))
                .ToList();
        }

        /// <summary>
        /// All descendants including this node's children, their children etc. Self-referencing nodes are automatically skipped
        /// </summary>
        public IEnumerable<TreeNode<T>> AllDescendants => GetAllDescendants(out _);

        /// <summary>
        /// Get all descendants, counting the number of self referencing nodes, which are skipped in the returned descendants
        /// </summary>
        /// <param name="totalParentReferencingNodesFound">The total number of self-referencing nodes found</param>
        /// <returns>A collection of all descendants, except for self referencing nodes</returns>
        public List<TreeNode<T>> GetAllDescendants(out int totalParentReferencingNodesFound)
        {
            return GetAllDescendants(new List<TreeNode<T>> { this }, out totalParentReferencingNodesFound);
        }

        /// <summary>
        /// Get all descendants, marking any nodes in the <paramref name="parents"/> as self referencing nodes, which are then skipped in the returned descendants
        /// </summary>
        /// <param name="parents">Any direct parents of this node</param>
        /// <param name="totalParentReferencingNodesFound">The total number of self-referencing nodes found</param>
        /// <returns></returns>
        public List<TreeNode<T>> GetAllDescendants(List<TreeNode<T>> parents, out int totalParentReferencingNodesFound)
        {
            List<TreeNode<T>> childNodes = new List<TreeNode<T>>();
            totalParentReferencingNodesFound = 0;

            foreach (var child in Children)
            {
                if (parents.Contains(child))
                {
                    ++totalParentReferencingNodesFound;
                    continue;
                }

                parents.Add(child);

                childNodes.Add(child);

                int totalParentReferencingChildNodesFound;

                foreach (var subChild in child.GetAllDescendants(parents, out totalParentReferencingChildNodesFound))
                {
                    childNodes.Add(subChild);
                }

                totalParentReferencingNodesFound += totalParentReferencingChildNodesFound;

                parents.RemoveAt(parents.Count - 1);
            }

            return childNodes;
        }
    }
}
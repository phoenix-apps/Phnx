using System.Collections.Generic;

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
        public List<TreeNode<T>> Children { get; }

        /// <summary>
        /// All descendants including this node's children, their children etc.
        /// </summary>
        public IEnumerable<TreeNode<T>> AllDescendants
        {
            get
            {
                foreach (var child in Children)
                {
                    yield return child;

                    foreach (var subChild in child.AllDescendants)
                    {
                        yield return subChild;
                    }
                }
            }
        }
    }
}
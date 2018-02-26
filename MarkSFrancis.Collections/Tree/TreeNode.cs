using System.Collections.Generic;

namespace MarkSFrancis.Collections.Tree
{
    public class TreeNode<T>
    {
        public TreeNode(T value)
        {
            Value = value;
        }

        public T Value { get; set; }

        public List<TreeNode<T>> ImmediateChildren { get; } = new List<TreeNode<T>>();

        public List<TreeNode<T>> AllChildren => ChildrenOf(this);

        private static List<TreeNode<T>> ChildrenOf(TreeNode<T> node)
        {
            List<TreeNode<T>> returnNodes = node.ImmediateChildren;

            foreach (TreeNode<T> nodeChild in node.ImmediateChildren)
            {
                returnNodes.AddRange(ChildrenOf(nodeChild));
            }

            return returnNodes;
        }

        public static explicit operator TreeNode<T>(T value)
        {
            return new TreeNode<T>(value);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkSFrancis.Collections.Tree
{
    public class Tree<T> : ICollection<T>
    {
        private List<TreeNode<T>> AllNodes { get; }

        public T this[int index] => AllNodes[index].Value;

        public TreeNode<T> this[T item] => AllNodes.First(x => x.Value.Equals(item));

        public Tree()
        {
            AllNodes = new List<TreeNode<T>>();
        }

        public void Add(T item)
        {
            TreeNode<T> newNode = new TreeNode<T>(item);
            AllNodes.Add(newNode);
        }

        public TreeNode<T> Add(T item, TreeNode<T> parentNode)
        {
            TreeNode<T> newNode = new TreeNode<T>(item);
            AllNodes.Add(newNode);

            parentNode.ImmediateChildren.Add(newNode);

            return newNode;
        }

        public void Add(TreeNode<T> node, TreeNode<T> parentNode)
        {
            AllNodes.Add(node);

            parentNode.ImmediateChildren.Add(node);
        }

        private void Clean()
        {
            foreach (TreeNode<T> node in TopNodes)
            {
                foreach (TreeNode<T> orphanedNode in node.AllChildren.Where(x => !AllNodes.Contains(x)))
                {
                    // Attached to a parent, but is deleted
                    Remove(orphanedNode);
                }
            }
        }

        public int IndexOf(TreeNode<T> node)
        {
            for (int index = 0; index < AllNodes.Count; index++)
            {
                if (AllNodes[index] == node)
                {
                    return index;
                }
            }

            return -1;
        }

        public void Remove(TreeNode<T> node)
        {
            foreach (TreeNode<T> treeNode in AllNodes)
            {
                treeNode.ImmediateChildren.RemoveAll(x => x == node);
            }

            AllNodes.RemoveAll(x => x == node);
        }

        public void RemoveAt(int index)
        {
            TreeNode<T> node = AllNodes[index];

            foreach (TreeNode<T> treeNode in AllNodes)
            {
                treeNode.ImmediateChildren.RemoveAll(x => x == node);
            }

            AllNodes.RemoveAt(index);
        }

        public IEnumerable<TreeNode<T>> ParentsOf(TreeNode<T> node)
        {
            return AllNodes.Where(x => x.ImmediateChildren.Contains(node));
        }

        public IEnumerable<TreeNode<T>> TopNodes
        {
            get
            {
                List<TreeNode<T>> topNodes = new List<TreeNode<T>>();
                foreach (TreeNode<T> treeNode in AllNodes)
                {
                    if (!AllNodes.Any(x => x.ImmediateChildren.Contains(treeNode)))
                    {
                        topNodes.Add(treeNode);
                    }
                }

                return topNodes;
            }
        }

        public int MaxGenerations
        {
            get
            {
                int maxSoFar = 0;
                foreach (TreeNode<T> treeNode in AllNodes)
                {
                    maxSoFar = Math.Max(NumberOfChildGenerations(treeNode), maxSoFar);
                }

                return maxSoFar;
            }
        }

        public int Count => AllNodes.Count;

        public bool IsReadOnly => false;

        private int NumberOfChildGenerations(TreeNode<T> node)
        {
            int maxSoFar = 0;
            foreach (TreeNode<T> nodeChild in node.ImmediateChildren)
            {
                maxSoFar = Math.Max(maxSoFar, NumberOfChildGenerations(node));
            }

            return maxSoFar + 1;
        }

        public void Clear() => AllNodes.Clear();

        public bool Contains(T item) => AllNodes.Any(x => (object)x.Value == (object)item);

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array.Length + arrayIndex < AllNodes.Count)
            {
                throw new IndexOutOfRangeException(nameof(arrayIndex));
            }

            for (int index = arrayIndex; index - arrayIndex < AllNodes.Count; index++)
            {
                array[index] = AllNodes[index - arrayIndex].Value;
            }
        }

        /// <summary>
        /// Removes the first occurance of an object from the tree
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(T item)
        {
            int count = AllNodes.Count;
            foreach (TreeNode<T> nodeToRemove in AllNodes.Where(x => (object)x.Value == (object)item))
            {
                Remove(nodeToRemove);
                if (count != AllNodes.Count)
                {
                    return true;
                }
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (TreeNode<T> treeNode in AllNodes)
            {
                yield return treeNode.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            StringBuilder returnString = new StringBuilder();
            foreach (TreeNode<T> topNode in TopNodes)
            {
                returnString.AppendLine(topNode.Value.ToString());

                WriteChildrenOf(topNode, 1, returnString);
            }

            return returnString.ToString();
        }

        private void WriteChildrenOf(TreeNode<T> node, int levelsDeep, StringBuilder stringBuilder)
        {
            foreach (TreeNode<T> nodeImmediateChild in node.ImmediateChildren)
            {
                stringBuilder.AppendLine(new string('\t', levelsDeep) + nodeImmediateChild.Value);
                WriteChildrenOf(nodeImmediateChild, levelsDeep + 1, stringBuilder);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Phnx.Collections
{
    /// <summary>
    /// Manages a tree data structure of values, with each node having a many-to-many relationship with other nodes
    /// </summary>
    /// <typeparam name="T">The type of data to store in the tree</typeparam>
    public class Tree<T> : IEnumerable<T>
    {
        private readonly List<TreeNode<T>> _topNodes;

        /// <summary>
        /// Get the top most nodes in the tree
        /// </summary>
        public IEnumerable<TreeNode<T>> TopNodes => _topNodes;

        /// <summary>
        /// Create a new empty <see cref="Tree{T}"/>
        /// </summary>
        public Tree()
        {
            _topNodes = new List<TreeNode<T>>();
        }

        /// <summary>
        /// Create a new <see cref="Tree{T}"/> with values as the top most nodes in the tree
        /// </summary>
        /// <param name="topNodes"></param>
        public Tree(IEnumerable<T> topNodes)
        {
            _topNodes = new List<TreeNode<T>>(topNodes.Select(n => new TreeNode<T>(n)));
        }

        /// <summary>
        /// Get all data nodes in the tree, depth first. Each node is only returned once, so if a node has two parents, it only appears once in the result
        /// </summary>
        public IEnumerable<TreeNode<T>> AllNodes
        {
            get
            {
                var allNodeRelationships = TopNodes.Append(TopNodes.Select(n => n.AllDescendants));
                return allNodeRelationships.DistinctBy(rel => rel);
            }
        }

        /// <summary>
        /// Get the total number of relationships between nodes
        /// </summary>
        public int TotalNodeRelationships => TopNodes.Sum(n =>
        {
            var allDescendants = n.GetAllDescendants(out int totalParentReferencingNodesFound);

            return allDescendants.DistinctBy(node => node).Count() + totalParentReferencingNodesFound;
        });

        /// <summary>
        /// Get the total number of data nodes in the tree
        /// </summary>
        public int TotalNodes => AllNodes.Count();

        /// <summary>
        /// Add a new top node to the tree
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public TreeNode<T> AddTopNode(T value)
        {
            var newNode = new TreeNode<T>(value);
            _topNodes.Add(newNode);

            return newNode;
        }

        /// <summary>
        /// Gets all nodes in the tree, depth first. This can contain duplicate nodes if two parents refer to the same node
        /// </summary>
        /// <returns>All nodes in the tree, depth first. This can contain duplicate nodes if two parents refer to the same node</returns>
        public IEnumerable<T> TraverseDepthFirst()
        {
            return
                TopNodes.DepthFirstFlatten(node => node.Children)
                .Select(n => n.Value);
        }

        /// <summary>
        /// Gets all nodes in the tree, breadth first. This can contain duplicate nodes if two parents refer to the same node
        /// </summary>
        /// <returns>All nodes in the tree, breadth first. This can contain duplicate nodes if two parents refer to the same node</returns>
        public IEnumerable<T> TraverseBreadthFirst()
        {
            return
                TopNodes.BreadthFirstFlatten(node => node.Children)
                .Select(n => n.Value);
        }

        /// <summary>
        /// Returns an enumerator for the <see cref="TopNodes"/> values. To traverse the entire tree, use <see cref="AllNodes"/>, <see cref="TraverseDepthFirst"/> or <see cref="TraverseBreadthFirst"/>
        /// </summary>
        /// <returns>An enumerator for the <see cref="TopNodes"/> values</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return TopNodes.Select(n => n.Value).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
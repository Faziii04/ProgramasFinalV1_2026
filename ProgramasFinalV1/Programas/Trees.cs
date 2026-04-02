using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramasFinalV1.Programas
{
    // 1) TREE NODE CLASS (represents each node in the tree)
    internal class TreeNode
    {
        internal int Value;
        internal TreeNode Left;
        internal TreeNode Right;

        internal TreeNode(int value)
        {
            Value = value;
        }
    }

    // 1) TREE CLASS (Binary Search Tree) + BASIC OPERATIONS
    internal class Trees
    {
        private TreeNode _root;
        public int Count { get; private set; }

        // Basic operation: check if tree is empty
        public bool IsEmpty()
        {
            return _root == null;
        }

        // Basic operation: clear entire tree
        public void Clear()
        {
            _root = null;
            Count = 0;
        }

        // Basic operation: insert one value
        public void Insert(int value)
        {
            _root = InsertRecursive(_root, value);
        }

        private TreeNode InsertRecursive(TreeNode node, int value)
        {
            if (node == null)
            {
                Count++;
                return new TreeNode(value);
            }

            if (value < node.Value)
            {
                node.Left = InsertRecursive(node.Left, value);
            }
            else if (value > node.Value)
            {
                node.Right = InsertRecursive(node.Right, value);
            }

            return node;
        }

        // 3) FUNCTION TO CREATE AN ORDERED TREE (BST) BY PASSING NUMBERS ONE BY ONE
        public void InsertOneByOne(IEnumerable<int> values)
        {
            if (values == null) return;

            foreach (int value in values)
            {
                Insert(value);
            }
        }

        // Basic operation: search value in tree
        public bool Contains(int value)
        {
            TreeNode current = _root;

            while (current != null)
            {
                if (value == current.Value) return true;
                current = value < current.Value ? current.Left : current.Right;
            }

            return false;
        }

        // Basic operation: delete value from tree
        public bool Delete(int value)
        {
            bool removed;
            _root = DeleteRecursive(_root, value, out removed);
            if (removed) Count--;
            return removed;
        }

        private TreeNode DeleteRecursive(TreeNode node, int value, out bool removed)
        {
            if (node == null)
            {
                removed = false;
                return null;
            }

            if (value < node.Value)
            {
                node.Left = DeleteRecursive(node.Left, value, out removed);
                return node;
            }

            if (value > node.Value)
            {
                node.Right = DeleteRecursive(node.Right, value, out removed);
                return node;
            }

            removed = true;

            if (node.Left == null) return node.Right;
            if (node.Right == null) return node.Left;

            TreeNode successor = GetMinNode(node.Right);
            node.Value = successor.Value;
            bool dummy;
            node.Right = DeleteRecursive(node.Right, successor.Value, out dummy);
            return node;
        }

        private TreeNode GetMinNode(TreeNode node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }
            return node;
        }

        // 2) TRAVERSAL #1: IN-ORDER (Left, Root, Right)
        public List<int> InOrder()
        {
            List<int> result = new List<int>();
            InOrderRecursive(_root, result);
            return result;
        }

        private void InOrderRecursive(TreeNode node, List<int> result)
        {
            if (node == null) return;

            InOrderRecursive(node.Left, result);
            result.Add(node.Value);
            InOrderRecursive(node.Right, result);
        }

        // 2) TRAVERSAL #2: PRE-ORDER (Root, Left, Right)
        public List<int> PreOrder()
        {
            List<int> result = new List<int>();
            PreOrderRecursive(_root, result);
            return result;
        }

        private void PreOrderRecursive(TreeNode node, List<int> result)
        {
            if (node == null) return;

            result.Add(node.Value);
            PreOrderRecursive(node.Left, result);
            PreOrderRecursive(node.Right, result);
        }

        // 2) TRAVERSAL #3: POST-ORDER (Left, Right, Root)
        public List<int> PostOrder()
        {
            List<int> result = new List<int>();
            PostOrderRecursive(_root, result);
            return result;
        }

        private void PostOrderRecursive(TreeNode node, List<int> result)
        {
            if (node == null) return;

            PostOrderRecursive(node.Left, result);
            PostOrderRecursive(node.Right, result);
            result.Add(node.Value);
        }

        // 2) TRAVERSAL #4: LEVEL-ORDER (Breadth-First)
        public List<int> LevelOrder()
        {
            List<int> result = new List<int>();
            if (_root == null) return result;

            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(_root);

            while (queue.Count > 0)
            {
                TreeNode current = queue.Dequeue();
                result.Add(current.Value);

                if (current.Left != null) queue.Enqueue(current.Left);
                if (current.Right != null) queue.Enqueue(current.Right);
            }

            return result;
        }
    }
}

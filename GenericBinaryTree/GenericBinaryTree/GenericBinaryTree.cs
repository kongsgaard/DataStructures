using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericBinaryTree
{
    public class GenericBinaryTree<T> where T : class
    {
        private Node<T> root;
        private Comparison<T> comparer;

        public GenericBinaryTree(T initial, Comparison<T> compare) 
        {
            root = new Node<T>(initial);
            comparer = compare;
        }

        /// <summary>
        /// Returns true if inserted,
        /// Returns false if key already exists
        /// </summary>
        /// <param name="data"></param>
        public bool Insert(T data) 
        {
            return Insert_Internal(data, root);
        }
        private bool Insert_Internal(T insertData, Node<T> currentNode)
        {
            int compareResult = comparer.Invoke(insertData, currentNode.data);

            if (compareResult == 0)
            {
                return false;
            }
            else if (compareResult == 1)
            {//Go left
                if (currentNode.leftChild == null) 
                {
                    currentNode.leftChild = new Node<T>(insertData);
                    currentNode.leftChild.parrent = currentNode;
                    return true;
                }
                return Insert_Internal(insertData, currentNode.leftChild);
            }
            else
            {//Go right
                if (currentNode.rightChild == null)
                {
                    currentNode.rightChild = new Node<T>(insertData);
                    currentNode.rightChild.parrent = currentNode;
                    return true;
                }
                return Insert_Internal(insertData, currentNode.rightChild);
            }
        }

        /// <summary>
        /// Returns null if no fitting instance was found
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public T Search(T data) 
        {
            Node<T> result = Search_Internal(data, root);

            if (result == null) 
            {
                return null;
            }

            return result.data;
        }
        private Node<T> Search_Internal(T searchData, Node<T> currentNode) 
        {
            int compareResult = comparer.Invoke(searchData, currentNode.data);

            if (compareResult == 0) 
            {
                return currentNode;
            }
            else if (compareResult == 1)
            {//Go left 
                if (currentNode.leftChild == null)
                {
                    return null;
                }
                return Search_Internal(searchData, currentNode.leftChild);
            }
            else 
            {//Go right
                if (currentNode.rightChild == null)
                {
                    return null;
                }
                return Search_Internal(searchData, currentNode.rightChild);
            }
        }

        /// <summary>
        /// Delete a node that fits the data,
        /// return true if the node was deleted
        /// return false if the node was not deleted (didn't exist)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Delte(T data) 
        {
            Node<T> returnNode = DeleteInternal(data, root);

            if (returnNode != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private Node<T> DeleteInternal(T deleteData, Node<T> currentNode) 
        {
            int compareResult = comparer.Invoke(deleteData, currentNode.data);

            if (compareResult == 0)
            {//delete this node

                if (currentNode.leftChild == null && currentNode.rightChild == null) 
                {
                    Node<T> returnNode = new Node<T>(currentNode.data);
                    currentNode = null;
                    return returnNode;
                }
                else if(currentNode.leftChild == null && currentNode.rightChild != null)
                {//Replace with right node
                    Node<T> returnNode = new Node<T>(currentNode.data);
                    if (currentNode.parrent.leftChild == currentNode)
                    {
                        currentNode.parrent.leftChild = currentNode.leftChild;
                    }
                    else if (currentNode.parrent.rightChild == currentNode)
                    {
                        currentNode.parrent.rightChild = currentNode.rightChild;
                    }
                    return returnNode;
                }
                else if (currentNode.rightChild == null && currentNode.leftChild != null)
                {//Replace with left node
                    Node<T> returnNode = new Node<T>(currentNode.data);

                    if (currentNode.parrent.leftChild == currentNode) 
                    {
                        currentNode.parrent.leftChild = currentNode.leftChild;
                    }
                    else if (currentNode.parrent.rightChild == currentNode) 
                    {
                        currentNode.parrent.rightChild = currentNode.rightChild;
                    }
                    return returnNode;
                }
                else 
                {//None of children are null,
                    Random ran = new Random();
                    int leftORright = ran.Next(0,2);
                    Console.WriteLine(leftORright);

                    if (leftORright == 0)
                    {//Replace from left side
                        Node<T> predecessor = FindPredecessorInternal(currentNode);
                        DeleteInternal(predecessor.data, predecessor);
                        
                        predecessor.leftChild = currentNode.leftChild;
                        predecessor.leftChild.parrent = predecessor;

                        predecessor.rightChild = currentNode.rightChild;
                        predecessor.rightChild.parrent = predecessor;

                        predecessor.parrent = currentNode.parrent;
                        
                        if (currentNode.parrent.leftChild == currentNode) 
                        {
                            currentNode.parrent.leftChild = predecessor;
                        }
                        else if (currentNode.parrent.rightChild == currentNode) 
                        {
                            currentNode.parrent.rightChild = predecessor;
                        }
                    }
                    else 
                    {//Replace from right side
                        Node<T> successor = FindSuccessorInternal(currentNode);
                        DeleteInternal(successor.data, successor);

                        successor.leftChild = currentNode.leftChild;
                        successor.rightChild = currentNode.rightChild;
                        successor.parrent = currentNode.parrent;

                        if (currentNode.parrent.leftChild == currentNode)
                        {
                            currentNode.parrent.leftChild = successor;
                        }
                        else if (currentNode.parrent.rightChild == currentNode)
                        {
                            currentNode.parrent.rightChild = successor;
                        }
                    }
                    
                    return currentNode;
                }
            }
            else if (compareResult == 1)
            {//Go left 
                if (currentNode.leftChild == null)
                {
                    return null;
                }
                return DeleteInternal(deleteData, currentNode.leftChild);
            }
            else
            {//Go right
                if (currentNode.rightChild == null)
                {
                    return null;
                }
                return DeleteInternal(deleteData, currentNode.rightChild);
            }
        }

        /// <summary>
        /// Changing the compare value of the returned value will ruin the binary search tree property of the tree
        /// If you wish to alter the value, delete the node from the tree
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public T FindPredecessor(T data) 
        {
            Node<T> dataNode = Search_Internal(data, root);
            return FindPredecessorInternal(dataNode).data;
        }

        private Node<T> FindPredecessorInternal(Node<T> node)
        {
            Node<T> searchNode = node.leftChild;
            while(true) 
            {
                if (searchNode.rightChild == null)
                {
                    return searchNode;
                }
                else
                {
                    searchNode = searchNode.rightChild;
                }
            }
        }

        /// <summary>
        /// Changing the compare value of the returned value will ruin the binary search tree property of the tree
        /// If you wish to alter the value, delete the node from the tree
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public T FindSuccessor(T data)
        {
            Node<T> dataNode = Search_Internal(data, root);
            return FindSuccessorInternal(dataNode).data;
        }

        private Node<T> FindSuccessorInternal(Node<T> node)
        {
            Node<T> searchNode = node.rightChild;
            while (true)
            {
                if (searchNode.leftChild == null)
                {
                    return searchNode;
                }
                else
                {
                    searchNode = searchNode.leftChild;
                }
            }
        }
    }


    public class Node<T> 
    {
        public T data;
        public Node<T> leftChild;
        public Node<T> rightChild;
        public Node<T> parrent;

        public Node(T dat) 
        {
            data = dat;
            leftChild = null;
            rightChild = null;
        }
    }
}

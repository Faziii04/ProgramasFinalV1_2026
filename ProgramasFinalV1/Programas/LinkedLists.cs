using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramasFinalV1.Programas
{
    internal class Node
    {
        public int Data { get; set; }
        public Node Next { get; set; }
        public Node(int data)
        {
            Data = data;
            Next = null;
        }
    }

    internal class DoubleNode
    {
        public int Data { get; set; }
        public DoubleNode Next { get; set; }
        public DoubleNode Prev { get; set; }
        public DoubleNode(int data)
        {
            Data = data;
            Next = null;
            Prev = null;
        }
    }

    internal class SinglyLinkedList
    {
        private Node head;
        private int count;

        public SinglyLinkedList() 
        { 
            head = null; 
            count = 0; 
        }

        public int GetSize() => count;
        public bool IsEmpty() => head == null;
        public Node GetHead() => head;

        public void Add(int data)
        {
            Node newNode = new Node(data);
            if (head == null) { head = newNode; }
            else
            {
                Node current = head;
                while (current.Next != null) current = current.Next;
                current.Next = newNode;
            }
            count++;
        }

        public Node GetAt(int index)
        {
            if (index < 0 || index >= count) throw new IndexOutOfRangeException("Invalid index.");
            Node current = head;
            for (int i = 0; i < index; i++) current = current.Next;
            return current;
        }

        public void Update(int index, int newData)
        {
            Node node = GetAt(index);
            if (node != null) node.Data = newData;
        }

        public bool RemoveAt(int index)
        {
            if (index < 0 || index >= count) return false;
            if (index == 0)
            {
                head = head.Next;
            }
            else
            {
                Node prev = GetAt(index - 1);
                prev.Next = prev.Next?.Next;
            }
            count--;
            return true;
        }
    }

    internal class DoublyLinkedList
    {
        private DoubleNode head;
        private DoubleNode tail;
        private int count;

        public DoublyLinkedList() 
        { 
            head = null; 
            tail = null; 
            count = 0; 
        }

        public int GetSize() => count;
        public bool IsEmpty() => head == null;
        public DoubleNode GetHead() => head;

        public void Add(int data)
        {
            DoubleNode newNode = new DoubleNode(data);
            if (head == null)
            {
                head = tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                newNode.Prev = tail;
                tail = newNode;
            }
            count++;
        }

        public DoubleNode GetAt(int index)
        {
            if (index < 0 || index >= count) throw new IndexOutOfRangeException("Invalid index.");
            DoubleNode current = head;
            for (int i = 0; i < index; i++) current = current.Next;
            return current;
        }

        public void Update(int index, int newData)
        {
            DoubleNode node = GetAt(index);
            if (node != null) node.Data = newData;
        }

        public bool RemoveAt(int index)
        {
            if (index < 0 || index >= count) return false;
            DoubleNode current = GetAt(index);

            if (current.Prev != null) current.Prev.Next = current.Next;
            else head = current.Next;

            if (current.Next != null) current.Next.Prev = current.Prev;
            else tail = current.Prev;

            count--;
            return true;
        }
    }

    internal class CircularLinkedList
    {
        private DoubleNode head;
        private DoubleNode tail;
        private int count;

        public CircularLinkedList() 
        { 
            head = null; 
            tail = null; 
            count = 0; 
        }

        public int GetSize() => count;
        public bool IsEmpty() => head == null;
        public DoubleNode GetHead() => head;

        public void Add(int data)
        {
            DoubleNode newNode = new DoubleNode(data);
            if (head == null)
            {
                head = tail = newNode;
                head.Next = head;
                head.Prev = head;
            }
            else
            {
                tail.Next = newNode;
                newNode.Prev = tail;
                newNode.Next = head;
                head.Prev = newNode;
                tail = newNode;
            }
            count++;
        }

        public DoubleNode GetAt(int index)
        {
            if (index < 0 || index >= count) throw new IndexOutOfRangeException("Invalid index.");
            DoubleNode current = head;
            for (int i = 0; i < index; i++) current = current.Next;
            return current;
        }

        public void Update(int index, int newData)
        {
            DoubleNode node = GetAt(index);
            if (node != null) node.Data = newData;
        }

        public bool RemoveAt(int index)
        {
            if (index < 0 || index >= count) return false;

            if (count == 1)
            {
                head = tail = null;
            }
            else
            {
                DoubleNode current = GetAt(index);
                current.Prev.Next = current.Next;
                current.Next.Prev = current.Prev;
                if (current == head) head = current.Next;
                if (current == tail) tail = current.Prev;
            }
            count--;
            return true;
        }
    }
}

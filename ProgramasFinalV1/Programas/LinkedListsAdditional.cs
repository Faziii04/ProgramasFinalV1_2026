using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramasFinalV1.Programas
{
    internal class Node<T>
    {
        public T Data { get; set; }
        public Node<T> Next { get; set; }

        public Node(T data)
        {
            Data = data;
            Next = null;
        }
    }

    internal class CustomVector<T>
    {
        private Node<T> head;
        private Node<T> tail;
        private int count;

        public CustomVector(int capacity = 100)
        {
            head = null;
            tail = null;
            count = 0;
        }

        public void Add(T item)
        {
            Node<T> newNode = new Node<T>(item);
            if (head == null)
            {
                head = tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                tail = newNode;
            }
            count++;
        }

        public T Get(int index)
        {
            if (index < 0 || index >= count) throw new IndexOutOfRangeException("Invalid index.");

            Node<T> current = head;
            for (int i = 0; i < index; i++) current = current.Next;
            return current.Data;
        }

        public int Size() => count;
    }

    internal class CustomStack<T>
    {
        private Node<T> top;

        public CustomStack(int capacity = 100)
        {
            top = null;
        }

        public void Push(T item)
        {
            Node<T> newNode = new Node<T>(item);
            newNode.Next = top;
            top = newNode;
        }

        public T Pop()
        {
            if (IsEmpty()) throw new InvalidOperationException("Stack is empty.");

            T value = top.Data;
            top = top.Next;
            return value;
        }

        public bool IsEmpty() => top == null;
    }

    internal class CustomQueue<T>
    {
        private Node<T> front;
        private Node<T> rear;

        public CustomQueue(int capacity = 100)
        {
            front = null;
            rear = null;
        }

        public void Enqueue(T item)
        {
            Node<T> newNode = new Node<T>(item);
            if (rear == null)
            {
                front = rear = newNode;
            }
            else
            {
                rear.Next = newNode;
                rear = newNode;
            }
        }

        public T Dequeue()
        {
            if (IsEmpty()) throw new InvalidOperationException("Queue is empty.");

            T value = front.Data;
            front = front.Next;
            if (front == null) rear = null;
            return value;
        }

        public bool IsEmpty() => front == null;
    }

    internal class LinkedListsAdditional
    {
        // 1) Invert numbers and return the inverted sequence
        public CustomVector<int> InvertNumbers(CustomVector<int> numbers)
        {
            CustomVector<int> result = new CustomVector<int>();
            if (numbers == null) return result;

            CustomQueue<int> queue = new CustomQueue<int>(numbers.Size());
            CustomStack<int> stack = new CustomStack<int>(numbers.Size());

            for (int i = 0; i < numbers.Size(); i++) queue.Enqueue(numbers.Get(i));
            while (!queue.IsEmpty()) stack.Push(queue.Dequeue());
            while (!stack.IsEmpty()) result.Add(stack.Pop());

            return result;
        }

        // 2) Traverse doubly linked list in both directions and print while traversing
        public void TraverseDoubly(DoublyLinkedList list)
        {
            if (list == null || list.IsEmpty())
            {
                Console.WriteLine("Doubly linked list is empty.");
                return;
            }

            int size = list.GetSize();

            Console.WriteLine("Forward:");
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine(list.GetAt(i).Data);
            }

            Console.WriteLine("Reverse:");
            for (int i = size - 1; i >= 0; i--)
            {
                Console.WriteLine(list.GetAt(i).Data);
            }
        }

        // 3) Create and return a queue of stacks
        public CustomQueue<CustomStack<int>> CreateQueueOfStacks(CustomVector<CustomVector<int>> stacksData)
        {
            CustomQueue<CustomStack<int>> queueOfStacks = new CustomQueue<CustomStack<int>>();
            if (stacksData == null) return queueOfStacks;

            for (int i = 0; i < stacksData.Size(); i++)
            {
                CustomStack<int> stack = new CustomStack<int>();
                CustomVector<int> currentStackData = stacksData.Get(i);

                if (currentStackData != null)
                {
                    for (int j = 0; j < currentStackData.Size(); j++)
                    {
                        stack.Push(currentStackData.Get(j));
                    }
                }

                queueOfStacks.Enqueue(stack);
            }

            return queueOfStacks;
        }

        /*
         * =============================
         * 1) PART THAT INVERTS NUMBERS
         * =============================
         * Main function:
         * - InvertNumbers(CustomVector<int> numbers)
         *
         * Return type:
         * - CustomVector<int> (inverted values)
         *
         * Involved methods:
         * - CustomVector<T>.Get(int index)
         * - CustomVector<T>.Size()
         * - CustomVector<T>.Add(T item)
         * - CustomQueue<T>.Enqueue(T item)
         * - CustomQueue<T>.Dequeue()
         * - CustomQueue<T>.IsEmpty()
         * - CustomStack<T>.Push(T item)
         * - CustomStack<T>.Pop()
         * - CustomStack<T>.IsEmpty()
         */

        /*
         * =========================================
         * 2) DOUBLY LINKED LIST TRAVERSAL (OUTPUT)
         * =========================================
         * Main function:
         * - TraverseDoubly(DoublyLinkedList list)
         *
         * Behavior:
         * - Prints while traversing (no extra storage structure)
         *
         * Involved methods:
         * - DoublyLinkedList.IsEmpty()
         * - DoublyLinkedList.GetSize()
         * - DoublyLinkedList.GetAt(int index)
         */

        /*
         * ==================
         * 3) QUEUE OF STACKS
         * ==================
         * Main function:
         * - CreateQueueOfStacks(CustomVector<CustomVector<int>> stacksData)
         *
         * Involved methods:
         * - CustomVector<T>.Get(int index)
         * - CustomVector<T>.Size()
         * - CustomStack<T>.Push(T item)
         * - CustomQueue<T>.Enqueue(T item)
         */
    }
}

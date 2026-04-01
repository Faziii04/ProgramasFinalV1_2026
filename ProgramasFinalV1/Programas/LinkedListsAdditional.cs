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
        // 1. Invert a series of numbers using vectors, queues, and stacks
        public void InvertNumbersDemonstration(int[] numbers)
        {
            CustomVector<int> vector = new CustomVector<int>(numbers.Length);
            CustomQueue<int> queue = new CustomQueue<int>(numbers.Length);
            CustomStack<int> stack = new CustomStack<int>(numbers.Length);

            // Store in Vector
            foreach (var num in numbers) vector.Add(num);

            // Transfer from Vector to Queue
            for (int i = 0; i < vector.Size(); i++) queue.Enqueue(vector.Get(i));

            // Transfer from Queue to Stack (this prepares inversion)
            while (!queue.IsEmpty()) stack.Push(queue.Dequeue());

            // Pop from Stack to print inverted numbers
            Console.WriteLine("Inverted numbers:");
            while (!stack.IsEmpty()) Console.WriteLine(stack.Pop());
        }

        // 2. Iterate over the doubly linked list forward and reverse
        public void IterateDoublyLinkedList(DoublyLinkedList list)
        {
            if (list == null || list.IsEmpty()) return;

            DoubleNode current = list.GetHead();

            Console.WriteLine("Forward:");
            while (current != null)
            {
                Console.WriteLine(current.Data);
                if (current.Next == null) break; // keep reference to tail
                current = current.Next;
            }

            Console.WriteLine("Reverse:");
            // Since we break when current.Next == null, 'current' is locally at the tail
            while (current != null)
            {
                Console.WriteLine(current.Data);
                current = current.Prev;
            }
        }

        // 3. Create a queue of stacks or a stack of queues
        public void ComplexStructuresDemonstration()
        {
            // Queue of Stacks
            CustomQueue<CustomStack<int>> queueOfStacks = new CustomQueue<CustomStack<int>>(5);
            CustomStack<int> myStack = new CustomStack<int>(5);
            myStack.Push(10);
            myStack.Push(20);
            queueOfStacks.Enqueue(myStack);

            // Stack of Queues
            CustomStack<CustomQueue<int>> stackOfQueues = new CustomStack<CustomQueue<int>>(5);
            CustomQueue<int> myQueue = new CustomQueue<int>(5);
            myQueue.Enqueue(1); // Since CustomQueue now takes int
            myQueue.Enqueue(2);
            stackOfQueues.Push(myQueue);

            Console.WriteLine("Added a stack to the queue and a queue to the stack successfully.");
        }
    }
}

using System.Collections;

namespace Lab3_2_Variant8;

/// <summary>Узагальнене бінарне дерево. T має бути класом, а не структурою.</summary>
public class BinaryTree<T> : IEnumerable<T> where T : class
{
    private sealed class Node
    {
        public T Data { get; }
        public Node? Left { get; set; }
        public Node? Right { get; set; }
        public Node(T data) => Data = data;
    }

    private Node? _root;
    private readonly IComparer<T> _comparer;

    public BinaryTree(IComparer<T>? comparer = null)
    {
        _comparer = comparer ?? Comparer<T>.Default;
    }

    public void Add(T item)
    {
        ArgumentNullException.ThrowIfNull(item);
        var newNode = new Node(item);

        if (_root is null)
        {
            _root = newNode;
            return;
        }

        Node current = _root;
        while (true)
        {
            if (_comparer.Compare(item, current.Data) < 0)
            {
                if (current.Left is null)
                {
                    current.Left = newNode;
                    return;
                }
                current = current.Left;
            }
            else
            {
                if (current.Right is null)
                {
                    current.Right = newNode;
                    return;
                }
                current = current.Right;
            }
        }
    }

    // Власний ітератор виконує обхід у зворотному порядку: ліве, праве, корінь.
    public IEnumerator<T> GetEnumerator() => new PostOrderEnumerator(_root);
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private sealed class PostOrderEnumerator : IEnumerator<T>
    {
        private readonly List<T> _items = new();
        private int _position = -1;

        public PostOrderEnumerator(Node? root) => BuildPostOrder(root);

        private void BuildPostOrder(Node? node)
        {
            if (node is null) return;
            BuildPostOrder(node.Left);
            BuildPostOrder(node.Right);
            _items.Add(node.Data);
        }

        public T Current => _position >= 0 && _position < _items.Count
            ? _items[_position]
            : throw new InvalidOperationException("Ітератор не встановлено на елемент.");

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (_position + 1 >= _items.Count)
                return false;
            _position++;
            return true;
        }

        public void Reset() => _position = -1;
        public void Dispose() { }
    }
}

using System.Runtime.CompilerServices;

namespace CollectionExpressionArguments;

// CollectionBuilder tells the compiler which method to call to create
// this collection when a collection expression is used.
//
// The builder method is MyCollectionBuilder.Create.
[CollectionBuilder(typeof(MyCollectionBuilder), nameof(MyCollectionBuilder.Create))]
class MyCollection<T> : IEnumerable<T>
{
    // Queue preserves insertion order and allows removing the oldest item.
    private readonly Queue<T> _items;

    // Initialize the underlying queue with the requested capacity.
    public MyCollection(int capacity) => _items = new Queue<T>(capacity);

    // Store the requested capacity so it can be inspected later.
    public int Capacity { get; init; }

    // Add an item while enforcing the maximum number of retained elements.
    public void Add(T item, int capacity)
    {
        // If the collection is full, remove the oldest element first.
        if (_items.Count == capacity)
        {
            _items.Dequeue();
        }

        // Append the new element to the end of the queue.
        _items.Enqueue(item);
    }

    // Allow foreach and string.Join to enumerate the collection.
    public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

    // Non-generic IEnumerable implementation required by the interface.
    System.Collections.IEnumerator
    System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
}

static class MyCollectionBuilder
{
    // Collection builder method used by the compiler to construct MyCollection<T>.
    //
    // `capacity` comes from `with(capacity: 4)`.
    // `items` contains the elements specified in the collection expression.
    //
    // ReadOnlySpan<T> lets the builder process the input elements efficiently.
    public static MyCollection<T> Create<T>(int capacity, ReadOnlySpan<T> items)
    {
        // Create the collection with the requested capacity and record it.
        var buffer = new MyCollection<T>(capacity)
        {
            Capacity = capacity
        };

        // Add every element, applying the collection's capacity limit.
        foreach (var item in items)
            buffer.Add(item, capacity);

        // Return the fully constructed collection.
        return buffer;
    }
}
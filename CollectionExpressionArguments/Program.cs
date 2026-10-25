using CollectionExpressionArguments;

// C# 15 Collection Expression Arguments:
// `with(capacity: 4)` passes the capacity argument to the collection
// creation process instead of relying on the default capacity.

// Create a List<string> with an initial capacity of 4.
List<string> names = [with(capacity: 4), "Alice", "Bob", "Charlie"];

// Capacity is the allocated storage size, while Count is the number
// of elements currently stored in the list.
Console.WriteLine($"names.Capacity = {names.Capacity} (Count = {names.Count})");

// Create a HashSet<string> with an initial capacity of 4.
// HashSet stores unique elements, so duplicate values are ignored.
// String comparisons are case-sensitive by default, meaning "Hi",
// "HI", and "hi" are treated as different values.
HashSet<string> greetings = [with(capacity: 4), "Hi", "HI", "HI", "hi", "Bye"];

Console.WriteLine(string.Join(", ", greetings));

// Create a custom collection using a collection expression.
// The capacity argument is passed to the custom collection builder.
// This collection retains at most 4 elements, removing the oldest
// element whenever another element is added after reaching capacity.
MyCollection<int> ring = [with(capacity: 4), 1, 2, 3, 4, 5];

Console.WriteLine(string.Join(", ", ring));
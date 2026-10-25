// Create a list containing five integers.
var numbers = new List<int> { 10, 20, 30, 40, 50 };

// Arrays and spans support range indexing, for example array[1..3].
// List<T> does not provide a built-in Range indexer.
// The extension indexer below adds this functionality.
//
// Range 1..3 means start at index 1 and stop BEFORE index 3.
// Therefore, the result contains 20 and 30.
Console.WriteLine(string.Join(", ", numbers[1..3])); // 20, 30


// Create a dictionary with two key-value pairs.
var scores = new Dictionary<string, int>
{
    ["alice"] = 10,
    ["bob"] = 20
};

// Use the custom two-parameter extension indexer.
// Its getter adds the values associated with both keys:
// scores["alice"] + scores["bob"] = 10 + 20 = 30.
Console.WriteLine(scores["alice", "bob"]); // 30


// Use the setter of the extension indexer.
// The assigned value (5) is written to BOTH dictionary keys.
scores["alice", "bob"] = 5;

// Enumerate the dictionary and print each key-value pair.
// Both Alice and Bob now have a score of 5.
Console.WriteLine(
    string.Join(", ", scores.Select(kv => $"{kv.Key}={kv.Value}")));
// Output: alice=5, bob=5


public static class Extensions
{
    // Define an extension block for List<T>.
    // `source` is the list instance on which the extension indexer is used.
    extension<T>(List<T> source)
    {
        // Add an indexer that accepts a Range, such as 1..3 or ^2..^0.
        // The indexer returns a new List<T> containing the selected elements.
        public List<T> this[Range range]
        {
            get
            {
                // Convert the range into a starting offset and element count.
                // GetOffsetAndLength handles indexes from the end, such as ^1.
                var (offset, length) =
                    range.GetOffsetAndLength(source.Count);

                // GetRange takes (startIndex, count), not a Range.
                // It creates a new list containing the selected elements.
                return source.GetRange(offset, length);
            }
        }
    }

    // Define an extension block for Dictionary<string, int>.
    // This adds an indexer that accepts TWO string keys.
    extension(Dictionary<string, int> source)
    {
        // The getter combines the values associated with keyA and keyB.
        // Both keys must already exist in the dictionary.
        public int this[string keyA, string keyB]
        {
            get => source[keyA] + source[keyB];

            // The setter assigns the supplied value to both keys.
            // In an indexer setter, `value` is the assigned value.
            set
            {
                source[keyA] = value;
                source[keyB] = value;
            }
        }
    }
}
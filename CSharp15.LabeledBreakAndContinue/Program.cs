// C# 15 preview feature: labeled break and continue.
//
// A label identifies a loop so that a nested loop can target it directly.
// This can simplify nested-loop control flow without using a boolean
// flag or extracting the logic into a separate method.
//
// Requires a compiler that supports this preview feature and:
// <LangVersion>preview</LangVersion>

// Create a 3x3 two-dimensional array.
int[,] grid =
{
    { 1, 2, 3 },
    { 4, 5, 6 },
    { 7, 8, 9 },
}
;

// ------------------------------------------------------------
// BREAK <LABEL>: EXIT THE LABELED OUTER LOOP
// ------------------------------------------------------------

// Define the label `searchGrid` for the outer loop.
searchGrid:
for (int row = 0; row < grid.GetLength(0); row++)
{
    // Iterate through every column in the current row.
    for (int col = 0; col < grid.GetLength(1); col++)
    {
        // Display the coordinates and value being checked.
        Console.WriteLine($"checking [{row},{col}] = {grid[row, col]}");

        // Stop searching when the value 5 is found.
        if (grid[row, col] == 5)
        {
            Console.WriteLine(
                $"found 5 at [{row},{col}], stopping the outer loop");

            // Exit the entire labeled outer loop immediately.
            // The inner loop and outer loop both stop.
            break searchGrid;
        }
    }
}

// Execution continues here after break searchGrid.
Console.WriteLine();


// ------------------------------------------------------------
// CONTINUE <LABEL>: SKIP TO THE NEXT OUTER LOOP ITERATION
// ------------------------------------------------------------

// Define the label `rows` for the outer loop.
rows:
for (int row = 0; row < grid.GetLength(0); row++)
{
    // Search through the columns of the current row.
    for (int col = 0; col < grid.GetLength(1); col++)
    {
        // Check whether the current value is divisible by 4.
        if (grid[row, col] % 4 == 0)
        {
            Console.WriteLine(
                $"row {row} contains a multiple of 4 ({grid[row, col]}) " +
                "- skipping the rest of this row");

            // Stop processing this row and immediately continue
            // with the next iteration of the OUTER loop.
            // The remaining columns in this row are skipped.
            continue rows;
        }
    }

    // This statement runs only if the inner loop finishes normally,
    // meaning the row contains no multiples of 4.
    Console.WriteLine($"row {row} has no multiples of 4");
}
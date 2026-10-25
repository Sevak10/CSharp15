using System.Globalization;

// Create an array of shapes using a collection expression.
// The compiler infers the array elements as the base type Shape.
Shape[] shapes =
[
    new Circle(2.0),
    new Rectangle(3.0, 4.0),
    new Triangle(3.0, 4.0, 5.0),
];

// Process every shape using the same base type.
// Pattern matching determines the actual shape at runtime.
foreach (Shape shape in shapes)
{
    // Describe() returns a readable shape name.
    // -28 aligns the description in a field 28 characters wide.
    // F3 formats the area to exactly three decimal places.
    // InvariantCulture ensures a consistent decimal separator (.)
    // regardless of the machine's regional settings.
    Console.WriteLine(
        $"{Describe(shape),-28} area = " +
        $"{Area(shape).ToString("F3", CultureInfo.InvariantCulture)}");
}

Console.WriteLine();
Console.WriteLine("Every arm is covered, so no default/discard case is required.");

// Calculate the area using a switch expression.
// Because Shape is a closed hierarchy, the compiler can determine
// which derived types are possible and check that every type is covered.
//
// No `_` discard arm is necessary.
static double Area(Shape shape) => shape switch
{
    // Circle area = π × radius²
    Circle c => Math.PI * c.Radius * c.Radius,

    // Rectangle area = width × height
    Rectangle r => r.Width * r.Height,

    // Triangle area is calculated using Heron's formula.
    Triangle t => HeronArea(t),
};

// Return a readable description of each shape.
// This switch expression is also exhaustive because Shape is closed.
static string Describe(Shape shape) => shape switch
{
    Circle c => $"Circle(r={c.Radius})",
    Rectangle r => $"Rectangle({r.Width}x{r.Height})",
    Triangle t => $"Triangle({t.A},{t.B},{t.C})",
};

// Calculate a triangle's area using Heron's formula.
//
// s is the semi-perimeter: half the sum of the three side lengths.
// This formula works for valid triangles without requiring a height.
static double HeronArea(Triangle t)
{
    double s = (t.A + t.B + t.C) / 2.0;

    // Area = √(s × (s - a) × (s - b) × (s - c))
    return Math.Sqrt(s * (s - t.A) * (s - t.B) * (s - t.C));
}

// C# 15 closed type hierarchy:
//
// `closed` restricts which types can derive from Shape.
// Only permitted derived types declared in this source file
// can extend this hierarchy.
//
// `closed` also implies abstract, so do not add the `abstract`
// modifier explicitly.
internal closed record Shape;

// A circle is a permitted concrete shape.
// The positional record automatically defines the Radius property.
internal sealed record Circle(double Radius) : Shape;

// A rectangle stores its width and height.
internal sealed record Rectangle(double Width, double Height) : Shape;

// A triangle stores its three side lengths.
internal sealed record Triangle(double A, double B, double C) : Shape;
byte myByte = 255;
Console.WriteLine($"A byte: {myByte}\n");

sbyte mySignedByte = 127;
Console.WriteLine($"A signed byte: {mySignedByte}\n");

short myShort = 32767;
Console.WriteLine($"A short: {myShort}\n");

ushort myUnsignedShort = 65535;
Console.WriteLine($"An unsigned short: {myUnsignedShort}\n");

int myInt = 2_000_000;
Console.WriteLine($"An int: {myInt}\n");

uint myUnsignedInt = 4_000_000U;
Console.WriteLine($"An unsigned int: {myUnsignedInt}");

long myLong = 9_000_000_000_000_000_000L;
Console.WriteLine($"A long: {myLong}\n");

ulong myULong = 18_000_000_000_000_000_000UL;
Console.WriteLine($"An unsigned long: {myULong}\n");

char myChar = '@';
Console.WriteLine($"A char: {myChar}\n");

string myString = "Alice";
Console.WriteLine($"A string: {myString}\n");

float myFloat = 3.4e37f;
Console.WriteLine($"A float: {myFloat}\n");

double myDouble = 3.4e307;
Console.WriteLine($"A double: {myDouble}\n");

decimal myDecimal = 1_000_000_000_000_000_000_000_000.01M;
Console.WriteLine($"A decimal: {myDecimal}\n");

bool myBool = false;
Console.WriteLine($"A bool: {myBool}\n");

// Begin Returns portion

myByte = 1;
mySignedByte = -128;
myShort = -32_000;
myUnsignedShort = 2;
myInt = -2_000_000;
myUnsignedInt = 4;
myLong = -9_000_000_000_000_000_000L;
myULong = 8UL;
myChar = '$';
myString = "The United States of America";
myFloat = -4e-44f;
myDouble = -3.4e-322;
myDecimal = -1_000_000_000_000_000_000_000_000.01M;
myBool = true;

Console.WriteLine("New values assigned!:\n" +
    $"Byte: {myByte}\n" +
    $"Signed byte: {mySignedByte}\n" +
    $"Short: {myShort}\n" +
    $"Unsigned short: {myUnsignedShort}\n" +
    $"Int: {myInt}\n" +
    $"Unsigned int: {myUnsignedInt}\n" +
    $"Long: {myLong}\n" +
    $"Unsigned long: {myULong}\n" +
    $"Char: {myChar}\n" +
    $"String: {myString}\n" +
    $"Float: {myFloat}\n" +
    $"Double: {myDouble}\n" +
    $"Decimal: {myDecimal}\n" +
    $"Bool: {myBool}");
dynamic intA = 2;
dynamic intB = 3;
Console.WriteLine($"{intA} plus {intB} equals {Add(intA, intB)}");

dynamic doubleA = 2.3;
dynamic doubleB = 3.4;
Console.WriteLine($"{doubleA:0.0} plus {doubleB:0.0} equals {Add(doubleA, doubleB):0.0}");

dynamic stringA = "fire";
dynamic stringB = "truck";
Console.WriteLine($"{stringA} plus {stringB} equals {Add(stringA, stringB)}");

dynamic dateTimeA = new DateTime(1941, 12, 7);
dynamic timeSpanB = new TimeSpan(days: (int)(365.25001 * 100), 0, 0, 0);
Console.WriteLine($"{dateTimeA.ToShortDateString()} plus {timeSpanB.TotalDays / 365.21:0} years equals {Add(dateTimeA, timeSpanB).ToShortDateString()}");

dynamic Add(dynamic a, dynamic b) => a + b;


// The obvious downside to using dynamic like this is that the compiler 
// will not detect errors before the program is run.
// If the error is rare or situational the error may not be fixed before 
// the program is put into actual use, which could be very bad if the 
// program does something important and the cost of an error is high.
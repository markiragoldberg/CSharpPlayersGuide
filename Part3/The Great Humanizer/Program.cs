using Humanizer;

(DateOnly dateNow, TimeOnly timeNow) = DateTime.Now;
TimeOnly feastTime = new(18, 00);
DateOnly dailyFeastDate = timeNow <= feastTime ? dateNow : dateNow.AddDays(1);
DateTime nextDailyFeast = new(dailyFeastDate, feastTime, DateTimeKind.Local);
Console.WriteLine($"When is the daily feast? {nextDailyFeast.Humanize()}");

DateTime newYearFeast = new(dateNow.Year, 12, 31, 11, 59, 59, 999, 999, DateTimeKind.Local);
Console.WriteLine($"When is the New Year feast? {newYearFeast.Humanize()}");

Console.WriteLine($"When is the feast fifty hours from now? {DateTime.Now.AddHours(50).Humanize()}");
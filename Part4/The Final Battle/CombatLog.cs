//using ConsoleIO;

//namespace The_Final_Battle
//{
//    public class CombatLog(int maxMessages = 15)
//    {
//        public Queue<LogMessage> log = new();
//        public int MaxMessages { get => maxMessages; }

//        public void AddMessage(string message, ConsoleColor color = ConsoleColor.Gray)
//        {
//            log.Enqueue(new LogMessage(message, color));
//            while (log.Count > maxMessages)
//                log.Dequeue();
//        }

//        public void WriteLog()
//        {
//            foreach(LogMessage message in log)
//            {
//                ColoredConsole.WriteLine(message.Text, message.Color);
//            }
//        }
//    }

//    public class LogMessage(string text, ConsoleColor color)
//    {
//        public string Text { get => text; }
//        public ConsoleColor Color { get => color; }
//    }
//}

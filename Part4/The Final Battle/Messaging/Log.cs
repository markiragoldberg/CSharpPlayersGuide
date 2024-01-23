namespace The_Final_Battle.Messaging
{
	public class Log
	{
		private List<Message> _messages = new();
		private List<Message> _temporaryMessages = new();

		public IEnumerable<Message> MostRecent(int recentCount)
		{
			if (_messages.Count + _temporaryMessages.Count <= recentCount)
			{
				return _messages.Concat(_temporaryMessages);
			}
			else if (_temporaryMessages.Count < recentCount)
			{
				return _messages.TakeLast(recentCount - _temporaryMessages.Count)
					            .Concat(_temporaryMessages);
			}
			else
			{
				return _temporaryMessages.TakeLast(recentCount);
			}
		}
		public void AddMessage(string message, MessageCategory category)
		{
			_temporaryMessages.Clear();
			_messages.Add(new Message(message, category));
		}
		public void AddTemporaryMessage(string message, MessageCategory category) 
			=> _temporaryMessages.Add(new Message(message, category));
		public void ClearTemporaryMessages() => _temporaryMessages.Clear();
	}
}

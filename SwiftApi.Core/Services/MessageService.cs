using Microsoft.VisualBasic;
using SwiftApi.Core.Services.Interfaces;
using SwiftApi.Data.GlobalConstants;
using SwiftApi.Data.Models;
using SwiftApi.Data.Repositories;
using System.Text;
using System.Text.RegularExpressions;

namespace SwiftApi.Core.Services
{
	public class MessageService : IMessageService
	{
		private readonly IMessageRepository messageRepository;
		private readonly string[] blocks = { "{1:", "{2:", "{3:", "{4:", "{5:" };
		private readonly string blockEnd = "}";
		private readonly string blockEndDouble = "}}";
		private const int num = 3;

		Dictionary<string, string> swiftMessageParced = new Dictionary<string, string>();

		public MessageService(IMessageRepository messageRepository)
		{
			this.messageRepository = messageRepository;
		}

		public Message InsertSwiftMessage(string swiftMessage)
		{
			Message message = ParseSwiftMessage(swiftMessage);
			if (message != null)
			{
			    messageRepository.InsertSwiftMessageAsync(message);
			}
			return message;
		}
		public Message ParseSwiftMessage(string swiftMessage)
		{
			for (int i = 0; i < blocks.Length; i++)
			{
				string block = blocks[i];
				if (swiftMessage.Contains(block))
				{
					string keyText = $"Block{i + 1}";
					BlockSplit(swiftMessage, block, keyText);
				}
			}

			Message parsedMessage = ParseMessageElements(swiftMessageParced);
			return parsedMessage;
		}

		private Dictionary<string, string> BlockSplit(string message, string start, string keyText)
		{
			string endPoint = blockEnd;
			string key = keyText;
			int cuts = num;
			if (start.Contains("5:"))
			{
				endPoint = blockEndDouble;
			}
			if (start.Contains("3:"))
			{
				cuts = num + 1;
			}

			int startIndex = message.IndexOf(start) + cuts;
			int endIndex = message.IndexOf(endPoint, startIndex);
			string result = message.Substring(startIndex, endIndex - startIndex);

			if (start.Contains("5:"))
			{
				Block5Split(result);
			}
			else if (start.Contains("4:"))
			{
				Block4Split(result);
			}
			else
			{
				swiftMessageParced.Add(key, result);
			}

			return swiftMessageParced;
		}

		private void Block5Split(string result)
		{
			string key = "TrailersBlock";
			string input = $"{result}}}";
			int counter = 1;
			string pattern = @"\{(.*?)\}";
			MatchCollection matches = Regex.Matches(input, pattern);

			foreach (Match match in matches)
			{
				string extractedValue = match.Groups[1].Value;
				swiftMessageParced.Add($"{key}{counter}", extractedValue);
				counter++;
			}
		}
		private void Block4Split(string result)
		{
			string key = "TextBlock";
			string input = result.Replace("\r", "").Replace("\n", "");
			if (input.Contains("\\r\\n"))
			{
				input = result.Replace("\\r\\n", "");
			}
			string[] fieldTags = { ":20:", ":21:", ":79:" };

			string[] messageParts = input.Split(fieldTags, StringSplitOptions.RemoveEmptyEntries);
			messageParts = messageParts.Where(part => !string.IsNullOrEmpty(part)).ToArray();

			for (int i = 0; i < messageParts.Length; i++)
			{
				string extractedValue = messageParts.Length > 0 ? fieldTags[i] + messageParts[i].Trim() : "";
				swiftMessageParced.Add($"{key}{i + 1}", extractedValue);
			}
		}


		private Message ParseMessageElements(Dictionary<string, string> elements)
		{
			Message message = new Message
			{
				SenderBIC = elements.GetValueOrDefault("Block1"),
				MessageType = elements.GetValueOrDefault("Block2"),
				TransactionReferenceNumber = elements.GetValueOrDefault("TextBlock1"),
				RelatedReference = elements.GetValueOrDefault("TextBlock2"),
				NarrativeText = elements.GetValueOrDefault("TextBlock3"),
				MAC = elements.GetValueOrDefault("TrailersBlock1"),
				CHK = elements.GetValueOrDefault("TrailersBlock2")
			};
			if (string.IsNullOrEmpty(message.SenderBIC) || (string.IsNullOrEmpty(message.MessageType) && string.IsNullOrEmpty(message.TransactionReferenceNumber)))
			{
				return null;
			}
			return message;
		}

	}
}

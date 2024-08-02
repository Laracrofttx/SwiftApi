using Microsoft.VisualBasic;
using SwiftApi.Core.Services.Interfaces;
using SwiftApi.Data.GlobalConstants;
using SwiftApi.Data.Models;
using SwiftApi.Data.Repositories;
using System.Text;

namespace SwiftApi.Core.Services
{
	public class MessageService : IMessageService
	{
		private readonly IMessageRepository messageRepository;

		public MessageService(IMessageRepository messageRepository)
		{
			this.messageRepository = messageRepository;
		}

		public async Task<ResponseMessage<string>> InsertAllSwiftMessagesAsync(Message message)
		{
			var response = new ResponseMessage<string>();

			try
			{
				await this.messageRepository.InsertSwiftMessageAsync(message);
				response.Data = GlobalConstants.SuccessfulInsert;
			}
			catch (Exception ex)
			{
				response.IsSuccessful = false;
				throw new InvalidDataException(ex.Message);
			}
			return response;
		}

		public async Task<ResponseMessage<Message>> PargeSwiftMessageAsync(string messages)
		{
			var resultResponse = new ResponseMessage<Message>();

			try
			{
				var message = new Message();
				var lines = messages
				   .Split(new[]
				   { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

				message.SenderBIC = TakeSenderBIC(lines[0]);
				message.MessageType = TakeMessageType(lines[0]);
				message.TransactionReferenceNumber = TakeTransactionRefferenceNumber(lines[1]);
				message.RelatedReference = TakeRelatedRefference(lines[2]);
				message.NarrativeText = TakeNarrativeInformation(lines);
				message.MAC = TakeMAC(lines[22]);
				message.CHK = TakeCHK(lines[22]);

				resultResponse.Data = message;
			}
			catch (Exception ex)
			{
				resultResponse.IsSuccessful = false;
				throw new InvalidCastException(ex.Message);
			}

			return await Task.FromResult(resultResponse);
		}

		public string TakeNarrativeInformation(string[] lines)
		{
			var startLine = 3;
			var endLine = 21;

			var additionalInfoLines = lines[startLine..(endLine + 1)];
			var stringBuilder = new StringBuilder();

			foreach (var line in additionalInfoLines)
			{
				if (line.StartsWith(":79:"))
				{
					stringBuilder.AppendLine(line.Substring(4));
				}
				else
				{
					stringBuilder.AppendLine(line);
				}
			}
			return stringBuilder.ToString();
		}

		private string TakeSenderBIC(string line)
		{
			var start = line.IndexOf("F01") + 3;
			var end = line.IndexOf("}");
			return line.Substring(start, end - start); 
		}

		private string TakeMessageType(string line)
		{
			var start = line.IndexOf("2:") + 2;
			var end = line.IndexOf("ABGR") - 1;
			return line.Substring(start, end - start + 1);
		}

		private string TakeTransactionRefferenceNumber(string line)
		{
			return line.Substring(line.IndexOf(":20:") + 5);
		}

		private string TakeRelatedRefference(string line)
		{
			return line.Substring(line.IndexOf(":21:") + 5);
		}

		private string TakeMAC(string line)
		{
			var startIndex = line.IndexOf("MAC:") + 4;
			var endIndex = line.IndexOf("}", startIndex);
			return line.Substring(startIndex, endIndex - startIndex);
		}

		private string TakeCHK(string line)
		{
			var startIndex = line.IndexOf("CHK:") + 4;
			var endIndex = line.IndexOf("}", startIndex);
			return line.Substring(startIndex, endIndex - startIndex);
		}
	}
}

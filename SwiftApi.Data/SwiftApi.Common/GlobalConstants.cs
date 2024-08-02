using SwiftApi.Data.Models;

namespace SwiftApi.Data.GlobalConstants
{
	public class GlobalConstants
	{
		public const string SuccessfulInsert = "Message inserted successfully.";
		public const string ErrorDuringDatabaseOperations = "An error occurred during the Database operations: ";
		public const string AnotherUnexpectedError = "An unexpected error occurred: ";
		public const string SuccessfulTableCreation = "Table Message was created successfully";

		public const string CreateSqlTable = @"
        CREATE TABLE IF NOT EXISTS Message (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            MessageType Text,
            SenderBIC Text,
            TransactionReferenceNumber Text,
            RelatedReference Text,
            NarrativeText Text,
            MAC Text,
            CHK Text
        );";

	    public const string Insert = @"
            INSERT INTO Message (SenderBIC, MessageType, TransactionReferenceNumber, RelatedReference, NarrativeText, MAC, CHK)
            VALUES (@SenderBIC, @MessageType, @TransactionReferenceNumber, @RelatedReference, @NarrativeText, @MAC, @CHK)";


		//Constants for Database Actions

		public const string SuccessfulDataInsertion = "SwiftData inserted successfully.";
		public const string ErrorDuringInsertion = "An error occurred during the Database operations: ";
		public const string ErrorDuringParsing = "An error occurred while trying to parse the content: ";

		//Constants for Logging

		public const string ControllerCalled = "SwiftMessageApiController called.";
		public const string ImportProcessStarted = "Action Import starting.";
		public const string ParseProcessStarted = "Parsing content starting.";
		public const string InsertDataProcessStarted = "Inserting swift data starting.";

		//Constants for Exception Messages

		public const string ArgumentNullMessage = "File is empty.";
		public const string IOExceptionMessage = "An I/O error occurred while reading the file. Please check the file's availability and permissions.";
		public const string OutOfMemoryMessage = "Out of memory. File content is too large.";
		public const string AnotherOperationMessage = "Another unexpected error occurred during the operation.";

	}

}



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
	}

}



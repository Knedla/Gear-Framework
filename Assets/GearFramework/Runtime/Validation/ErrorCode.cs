namespace GearFramework.Runtime.Validation
{
    public partial class ErrorCode
    {
        private ErrorCode() { }

        public const string Common_StopProcessExecution = "common_stopprocessexecution"; // if for any reason there is a need to interrupt the process but not to trigger the error (example: the result of the execution of the process is equal to the beginning; real world example: swap of two identical items in the inventory)
    }
}

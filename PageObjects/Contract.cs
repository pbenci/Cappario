namespace Cappario
{
    public class Contract
    {
        public string Code { get; private set; }
        public string RightFiscalBranch { get; private set; }
        public string Status { get; private set; }
        public string CustomerCode { get; private set; }
        public string CustomerName { get; private set; }
        public string CurrentBranch { get; private set; }

        public Contract(string Code, string RightFiscalBranch, string Status, string CustomerCode, string CustomerName, string CurrentBranch)
        {
            this.Code = Code;
            this.RightFiscalBranch = RightFiscalBranch;
            this.Status = Status;
            this.CustomerCode = CustomerCode;
            this.CustomerName = CustomerName;
            this.CurrentBranch = CurrentBranch;
        }
    }
}
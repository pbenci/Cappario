namespace Cappario
{
    public class Contract
    {
        public string Code { get; private set; }
        public string RightFiscalBranch { get; private set; }
        public string CustomerCode { get; private set; }
        public string CustomerName { get; private set; }
        public string CurrentBranch { get; private set; }

        public Contract(string Code, string RightFiscalBranch, string CustomerCode, string CustomerName, string CurrentBranch)
        {
            this.Code = Code;
            this.RightFiscalBranch = RightFiscalBranch;
            this.CustomerCode = CustomerCode;
            this.CustomerName = CustomerName;
            this.CurrentBranch = CurrentBranch;
        }
    }
}
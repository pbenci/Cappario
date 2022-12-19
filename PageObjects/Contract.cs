namespace Cappario
{
    public class Contract
    {
        public string Code { get; private set; }
        public string RightFiscalBranch { get; private set; }
        public string Status { get; private set; }
        public Contract(string Code, string RightFiscalBranch, string Status)
        {
            this.Code = Code;
            this.RightFiscalBranch = RightFiscalBranch;
            this.Status = Status;
        }
    }
}
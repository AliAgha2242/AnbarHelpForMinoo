namespace DifffMalIAndAnbar.Dtos
{
    public class DetailProps
    {
        public string sanadNo { get; set; }

        public DetailProps(string sanadNo, string sanadDate)
        {
            this.sanadNo = sanadNo;
            this.sanadDate = sanadDate;
        }

        public string sanadDate { get; set; }
    }
}

namespace NetMed.ApiConsummer.Core.Base
{
    public class BaseModel
    {
        public BaseModel()
        {
            ChangeDate = DateTime.Now;
        }
        public DateTime? ChangeDate { get; set; }
    }
}
